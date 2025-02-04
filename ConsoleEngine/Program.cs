using Core;
using Core.Input;
using TestGame;
using TextRPG;

namespace ConsoleEngine;

class Program
{
    static void Main(string[] args)
    {
        
        Console.CursorVisible = false;
        // 아이템 Data 로드
        DataLoader.LoadItems();
        // 씬 로드
        LoadManager.LoadScenes();
        //InputMap 로드
        InputManager.MappingKeys = InputMapLoader.LoadData(); 

        // 게임 초기화
        Game game = new Game();
        game.Run(); // 게임 실행

        // 게임 종료
        Console.WriteLine("게임 종료!");
    }
}

