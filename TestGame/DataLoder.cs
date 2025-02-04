using Newtonsoft.Json;
using TestGame.Scripts;

namespace TextRPG;

public static class DataLoader
{
    private static string itemFilePath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "items.json");
    private static string playerFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "playerData.json");
    public static List<ItemScript> Items { get; private set; } = new List<ItemScript>();

    public static void LoadItems()
    {
        // Console.WriteLine($"현재 작업 디렉터리: {Directory.GetCurrentDirectory()}");
        //Console.WriteLine($"JSON 파일 예상 경로: {Path.GetFullPath(filePath)}");
        if (!File.Exists(itemFilePath))
        {
            Console.WriteLine("아이템 데이터 파일이 없습니다.");
            Console.ReadLine();
            return;
        }
        
        string json = File.ReadAllText(itemFilePath);
        Items = JsonConvert.DeserializeObject<List<ItemScript>>(json);
        Console.WriteLine("아이템 데이터 로드 완료!");
        
    }
    
    public static void SavePlayerData(PlayerScript player)
    {
        
        string json = JsonConvert.SerializeObject(player, Formatting.Indented);
        File.WriteAllText(playerFilePath, json);
        Console.WriteLine("플레이어 데이터가 저장되었습니다.");
    }
    
    public static PlayerScript LoadPlayerData()
    {
        if (!File.Exists(playerFilePath))
        {
            Console.WriteLine("저장된 데이터가 없습니다.");
            return null;
        }

        string json = File.ReadAllText(playerFilePath);

        return JsonConvert.DeserializeObject<PlayerScript>(json);;
    }
}