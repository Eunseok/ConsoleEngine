using System;
using System.Collections.Generic;
using Core.Components;
using Core.MyMath; // `Vector2<T>` 사용을 위한 네임스페이스

namespace Core.Objects
{
    public class GameObject : Entity, ICloneable

    {
        private readonly List<Component> _components = new(); //갖고있는 컴포넌트
        private readonly List<GameObject> _children = new(); //갖고있는 자식 오브젝트
        public float LfeTime { get; set; } = 0.0f;
        private float _lifeDeltaTime = 0.0f;

        private bool _isActive { get; set; } = true;
        public bool IsActive() => Parent?.IsActive() ?? _isActive;
        public bool IsDestroyed { get; set; } = false;

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }
        public int Order { get; set; } = 0; // 렌더링 순서를 위한 필드

        public void DestroyedTimer(float deltaTime)
        {
            _lifeDeltaTime += deltaTime;
            if (_lifeDeltaTime >= LfeTime)
            {
                IsDestroyed = true;
            }
        }


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

        public GameObject? Parent { get; set; } = null;// 부모 객체


        public GameObject() : base("GameObject")
        {
            Awake();
        }

        public GameObject(string name) : base(name)
        {
            Awake();
        }


        public void AddChild(GameObject child)
        {
            _children.Add(child);
            child.Parent = this;
        }
        public List<GameObject> GetChild()
        {
            return _children;
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
            component.Initialize();

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
            component.Initialize();

            return component;
        }


        // 특정 타입의 컴포넌트를 가져오기
        public T? GetComponent<T>() where T : Component
        {
            return _components.Find(c => c is T) as T;
        }


        //초기 컴포넌트 생성 
        public virtual void Awake()
        {
            AddComponent<Transform>();
            AddComponent<Renderer>();
        }
        
        public virtual void Initialize()
        {
            foreach (var component in _components)
                component.Initialize();
        }

        // 모든 컴포넌트 업데이트 진행
        public virtual void Update(float deltaTime)
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
                handler.Invoke(data!);
            }

            foreach (var component in _components)
            {
                component.OnMessageReceived(eventKey, data!); // 모든 컴포넌트로 메시지 전달
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

        public object Clone()
        {
            // 여기서 깊은 복사를 수행 (필요에 따라 컴포넌트 복사 등 추가 로직 작성)
            return MemberwiseClone();
        }
    }
}