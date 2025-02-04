using Core.Components;
using Core.MyMath;
using Core.Scenes;

namespace Core.Objects
{
    public static class Object
    {
        /// <summary>
        /// 기존 GameObject를 주어진 위치에 추가합니다.
        /// </summary>
        /// <param name="original">추가할 GameObject</param>
        /// <param name="position">새롭게 설정할 위치</param>
        /// <returns>추가된 GameObject</returns>
        public static GameObject Instantiate(GameObject original, Vector2<int> position)
        {
            // Scene에 추가
            SceneManager.CurrentScene.AddObject(original);

            // 위치 설정
            SetPosition(original, position);

            return original;
        }

        /// <summary>
        /// 기존 GameObject를 부모 객체와 함께 추가합니다.
        /// </summary>
        /// <param name="original">추가할 GameObject</param>
        /// <param name="parent">부모로 설정할 GameObject</param>
        /// <returns>부모가 설정된 GameObject</returns>
        public static GameObject Instantiate(GameObject original, GameObject parent)
        {
            // 부모 설정
            original.Parent = parent;

            // Scene에 추가
            SceneManager.CurrentScene.AddObject(original);

            // 부모의 위치 기반으로 객체 위치 설정
            SetPositionFromParent(original, parent);

            return original;
        }

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
            SceneManager.CurrentScene.AddObject(gameObject);

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

            // 부모 설정
            gameObject.Parent = parent;

            // Scene에 추가
            SceneManager.CurrentScene.AddObject(gameObject);

            // 부모의 위치 기반으로 객체 위치 설정
            SetPositionFromParent(gameObject, parent);

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
    }
}