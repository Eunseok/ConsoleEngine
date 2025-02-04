using Core;
using Core.Components;
using Core.Input;
using Core.MyMath;
using Core.Objects;
using Core.Scenes;
using TestGame.Scripts;
using static Core.Objects.Object;

namespace TestGame.Singletons;

public class GameManager : Script
{
    // 싱글톤 인스턴스
    public static GameManager? Instance { get; private set; }

 
    public override void Initialize()
    {
        if (Instance == null)
            Instance = this;
    }

    protected override void OnUpdate(float deltaTime)
    {

    }
}