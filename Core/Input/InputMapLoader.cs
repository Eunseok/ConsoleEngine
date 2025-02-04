using Newtonsoft.Json;

namespace Core.Input;

public static class InputMapLoader
{
    private static readonly string DatPath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input/InputMap", "InputMap.json");

    private static readonly Dictionary<string, List<string>> MappingKeys = new()
    {
        { "Escape", new List<string> { "Escape" } },
        { "MoveUp", new List<string> { "W" } },
        { "MoveDown", new List<string> { "S" } },
        { "MoveLeft", new List<string> { "A" } },
        { "MoveRight", new List<string> { "D" } },
        { "Jump", new List<string> { "Spacebar" } },
        { "Attack", new List<string> { "J" } }
    };

    // 데이터 로드 (역직렬화)
    public static Dictionary<string, List<string>> LoadData()
    {
        if (!File.Exists(DatPath))
        {
            Console.WriteLine("InputMap.json 파일이 없습니다. 기본 데이터를 반환합니다.");
            return MappingKeys;
        }

        try
        {
            string json = File.ReadAllText(DatPath);

            // JSON 파일을 Dictionary<string, List<string>>로 역직렬화
            var loadedData = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            if (loadedData != null)
            {
                return loadedData;
            }

            Console.WriteLine("JSON 데이터를 로드하지 못했습니다. 기본 데이터로 초기화합니다.");
            return MappingKeys;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"데이터 로드 중 오류 발생: {ex.Message}");
            return MappingKeys;
        }
    }

    // 데이터 저장 (직렬화)
    public static void SaveToJson()
    {
        try
        {
            // 들여쓰기 설정 추가
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented // 읽기 쉽게 출력
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