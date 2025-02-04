using System;
using System.Collections.Generic;
using Core.Components;
using Core.MyMath; // `Vector2<T>` 사용을 위한 네임스페이스

namespace Core.Objects
{
    public class GameObject : Entity
    {
        private readonly List<Component> _components = new(); //갖고있는 컴포넌트
        

        // 이벤트 시스템
        private Dictionary<string, Action<object>> _eventHandlers = new();

        // 로컬 좌표 
        public Vector2<int> LocalPosition
        {
            get
            {
                // Transform에서 현재 Position을 매번 가져옴
                return GetComponent<Transform>()?.Position ?? Vector2<int>.Zero();
            }
        }

        // 전역 좌표 계산 (getter)
        public Vector2<int> GlobalPosition
        {
            get
            {
                // 부모 객체가 있으면 부모의 GlobalPosition + 자신의 LocalPosition
                if (Parent != null)
                {
                    return Parent.GlobalPosition + LocalPosition;
                }

                // 부모 객체가 없으면 LocalPosition이 곧 GlobalPosition
                return LocalPosition;
            }
        }
        public GameObject? Parent { get; set; } // 부모 객체

        public GameObject(string name) : base(name)
        {
            
        }

        

        // 컴포넌트를 추가
        public T AddComponent<T>() where T : Component, new()
        {
            // GetComponent<T>() 호출
            T? existingComponent = GetComponent<T>();
    
            // 이미 존재하면 반환
            if (existingComponent != null) 
                return existingComponent;
            
            T component = new T();
            component.OnAttach(this);
            _components.Add(component);
            return component;
        }
        public T AddComponent<T>(params object[] args) where T : Component
        {
            // GetComponent<T>() 호출
            T? existingComponent = GetComponent<T>();
    
            // 이미 존재하면 반환
            if (existingComponent != null) 
                return existingComponent;

            T component = (T)Activator.CreateInstance(typeof(T), args)!;
            component.OnAttach(this);
            _components.Add(component);
            return component;
        }
        

        // 특정 타입의 컴포넌트를 가져오기
        public T? GetComponent<T>() where T : Component
        {
            return _components.Find(c => c is T) as T;
        }


        //초기 컴포넌트 생성 
        public virtual void Initialize()
        {
            AddComponent<Transform>();
            AddComponent<Renderer>();
        }

        // 모든 컴포넌트 업데이트 진행
        public void Update(float deltaTime)
        {
            foreach (var component in _components)
            {
                if (component.IsActive)
                {
                    component.Update(deltaTime);
                }
            }
        }

        // 메시지 브로드캐스트 (모든 컴포넌트로 전달)
        public void BroadcastEvent(string eventKey, object? data = null)
        {
            if (_eventHandlers.TryGetValue(eventKey, out var handler))
            {
                if (data != null)
                    handler.Invoke(data);
            }

            foreach (var component in _components)
            {
                if (data != null)
                component.OnMessageReceived(eventKey, data); // 모든 컴포넌트로 메시지 전달
            }
        }

        // 메시지 핸들러 추가
        public void RegisterEventHandler(string eventKey, Action<object> handler)
        {
            if (!_eventHandlers.ContainsKey(eventKey))
            {
                _eventHandlers[eventKey] = handler;
            }
        }

        // 메시지 핸들러 제거
        public void UnregisterEventHandler(string eventKey)
        {
            if (_eventHandlers.ContainsKey(eventKey))
            {
                _eventHandlers.Remove(eventKey);
            }
        }
    }
}