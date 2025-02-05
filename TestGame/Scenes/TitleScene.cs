using Core;
using Core.Scenes;
using Core.Components;
using Core.MyMath;
using Core.Objects;
using TestGame.Scripts;
using TestGame.Singletons;

using static Core.Objects.Object;

namespace TestGame.Scenes;

public class TitleScene : Scene
{
    public TitleScene() : base("TitleScene")
    {
    }

    public override void Initialize()
    {
        GameObject gameManager = Instantiate<EmptyObject>(Vector2<int>.Zero());
        gameManager.AddComponent<GameManager>();
        
        var box = Instantiate<BoxObject>(Game.ConsoleCenter);
        box.SetSize(30, 20);
        box.SetOrder(10);
        box.AddComponent<CursurScript>();
        
        var titleBox = Instantiate<BoxObject>(new Vector2<int>(0, -6), box);
        titleBox.SetSize(20,5);
        titleBox.SetOrder(1);
        Instantiate<LabelObject>(Vector2<int>.Zero(), titleBox).SetText("스파르타 던전", ConsoleColor.Blue);
        string[] str;
        if (LoadManager.HasPlayData())
        {
            str = new string[]
            {
                "이어하기",
                "새로하기",
                "나가기"
            };
        }
        else
        {
            str = new string[]
            {
                "새로하기",
                "나가기"
            };
        }

        for (int i = 0; i < str.Length; i++)
        {
            var label = str[i];
            var btn = Instantiate<ButtonObject>(new Vector2<int>(0, 1+i*2), box);
            btn.RegisterEventHandler("OnClick", _ =>
            {
                Destroy(box);
                GameManager.Instance.Owner?.BroadcastEvent("Title", label);
            });
            btn.SetLabel(label);
        }
    }


    public override void Update(float deltaTime)
    {
        base.Update(deltaTime); // Scene 기본 로직 호출
        // 추가 로직 구현
        
    }
}