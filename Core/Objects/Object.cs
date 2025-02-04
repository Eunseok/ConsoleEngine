using Core.Components;
using Core.MyMath;
using Core.Scenes;

namespace Core.Objects
{
    public static class Object
    {
        /// <summary>
        /// 새로운 GameObject를 주어진 위치에 생성합니다.
        /// </summary>
        /// <typeparam name="T">GameObject 유형</typeparam>
        /// <param name="position">새롭게 설정할 위치</param>
        /// <returns>생성된 GameObject</returns>
        public static T Instantiate<T>(Vector2<int> position) where T : GameObject, new()
        {
            // 새로운 GameObject 생성
            T gameObject = new T();

            // Scene에 추가
            SceneManager.CurrentScene?.AddObject(gameObject);

            // 위치 설정
            SetPosition(gameObject, position);

            return gameObject;
        }

        /// <summary>
        /// 새로운 GameObject를 부모 객체와 함께 생성합니다.
        /// </summary>
        /// <typeparam name="T">GameObject 유형</typeparam>
        /// <param name="parent">부모로 설정할 GameObject</param>
        /// <returns>부모가 설정된 GameObject</returns>
        public static T Instantiate<T>(GameObject parent) where T : GameObject, new()
        {
            // 새로운 GameObject 생성
            T gameObject = new T();

            // 자식 추가 
            parent.AddChild(gameObject);

            // Scene에 추가
            SceneManager.CurrentScene.AddObject(gameObject);

            return gameObject;
        }
        public static T Instantiate<T>(Vector2<int> position,GameObject parent) where T : GameObject, new()
        {
            // 새로운 GameObject 생성
            T gameObject = new T();

            // 자식 추가 
            parent.AddChild(gameObject);

            // Scene에 추가
            SceneManager.CurrentScene?.AddObject(gameObject);

            // 위치 설정
            SetPosition(gameObject, position);

            return gameObject;
        }



        
        /// <summary>
        /// GameObject의 Transform 컴포넌트를 사용하여 위치를 설정합니다.
        /// </summary>
        /// <param name="gameObject">위치를 설정할 GameObject</param>
        /// <param name="position">설정할 위치</param>
        private static void SetPosition(GameObject gameObject, Vector2<int> position)
        {
            gameObject.GetComponent<Transform>()?.SetPosition(position);
        }

        /// <summary>
        /// 부모 객체의 Transform 컴포넌트를 기반으로 GameObject 위치를 설정합니다.
        /// </summary>
        /// <param name="gameObject">위치를 설정할 GameObject</param>
        /// <param name="parent">부모로 사용할 GameObject</param>
        private static void SetPositionFromParent(GameObject gameObject, GameObject parent)
        {
            Vector2<int> parentPosition = parent.GetComponent<Transform>()?.Position ?? Vector2<int>.Zero();
            SetPosition(gameObject, parentPosition);
        }
        
        /// <summary>
        /// 지정된 GameObject를 삭제합니다.
        /// </summary>
        /// <param name="gameObject">삭제할 GameObject</param>
        /// <param name="delay">삭제 Delay</param>
        public static void Destroy(GameObject gameObject,float delay = 0.0f)
        {
            
            // Scene에서 제거할 GameObject 리스트에 추가
            SceneManager.CurrentScene.DestroyedObject(gameObject, delay);
            
        }
        
        public static void Destroy(string name,float delay = 0.0f)
        {
            
            // Scene에서 제거할 GameObject 리스트에 추가
            SceneManager.CurrentScene.DestroyedObject(name, delay);
            
        }

        public static void ObjectSort()
        {
            SceneManager.CurrentScene.ObjectSort();
        }
        
    }
}