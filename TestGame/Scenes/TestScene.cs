using Core.Scenes;
using TestGame.Objects;

namespace TestGame.Scenes;

public class TestScene : Scene
{
    public TestScene() : base("TestScene")
    {
        
    }
    public override void Initialize()
    {
        base.Initialize();  //Scene 기본 로직 호출
        //추가 로직 구현
        TestObj testObj = new TestObj(10, 10, 'A');
        AddObject(testObj);
    }
    public override void Update()
    {
        base.Update();  //Scene 기본 로직 호출
        //추가 로직 구현
    }

    public override void Render()
    {
        base.Render();  //Scene 기본 로직 호출
        //추가 로직 구현
    }
}