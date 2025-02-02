using Core.Scenes;

using TestGame.Scenes;

namespace TestGame;

public static class LoadManager
{
    public static void LoadScenes()
    {
        SceneManager.CreateScene<TestScene>();
    }
}
