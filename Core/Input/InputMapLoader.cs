using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Input;

public static class InputMapLoader
{
    private static readonly string DatPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input/InputMap", "InputMap.json");

    private static readonly Dictionary<string, ConsoleKey> MappingKeys = new()
    {
        { "Escape", ConsoleKey.Escape },
        { "MoveUp", ConsoleKey.W },
        { "MoveDown", ConsoleKey.S },
        { "MoveLeft", ConsoleKey.A },
        { "MoveRight", ConsoleKey.D },
        { "Jump", ConsoleKey.Spacebar },
        { "Attack", ConsoleKey.J }
    };

    // 데이터 로드 (역직렬화)
    public static Dictionary<string, ConsoleKey> LoadData()
    {
        if (!File.Exists(DatPath))
        {
            Console.WriteLine("InputMap.json 파일이 없습니다. 기본 데이터를 반환합니다.");
            return new Dictionary<string, ConsoleKey>();
        }

        try
        {
            string json = File.ReadAllText(DatPath);
            return JsonConvert.DeserializeObject<Dictionary<string, ConsoleKey>>(json)?? new Dictionary<string, ConsoleKey>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"데이터 로드 중 오류 발생: {ex.Message}");
            return new Dictionary<string, ConsoleKey>();
        }
    }

    // 데이터 저장 (직렬화)
    public static void SaveToJson()
    {
        try
        {
            // JSON 변환 설정: StringEnumConverter 추가
            var settings = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() }, // ConsoleKey 값을 문자열로 변환
                Formatting = Formatting.Indented // 들여쓰기 설정
            };

            // MappingKeys 데이터를 JSON으로 직렬화
            string json = JsonConvert.SerializeObject(MappingKeys, settings);

            // 파일 저장 경로가 없으면 디렉터리 생성
            string? directory = Path.GetDirectoryName(DatPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // JSON 데이터를 파일에 저장
            File.WriteAllText(DatPath, json);
            Console.WriteLine($"InputMap.json이 {DatPath} 경로에 저장되었습니다.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"데이터 저장 중 오류 발생: {ex.Message}");
        }
    }
}