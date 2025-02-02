namespace Core.Managers
{
    public static class InputConfig
    {
        private static readonly Dictionary<string, ConsoleKey> DefaultMappings = new()
        {
            { "MoveUp", ConsoleKey.W },
            { "MoveDown", ConsoleKey.S },
            { "MoveLeft", ConsoleKey.A },
            { "MoveRight", ConsoleKey.D },
            { "Jump", ConsoleKey.Spacebar },
            { "Attack", ConsoleKey.J }
        };

        public static Dictionary<string, ConsoleKey> KeyMappings { get; private set; } = new(DefaultMappings);

        public static void RebindKey(string action, ConsoleKey newKey)
        {
            if (KeyMappings.ContainsKey(action))
                KeyMappings[action] = newKey;
        }

        public static void AddCustomKeyMapping(string action, ConsoleKey key)
        {
            if (!KeyMappings.ContainsKey(action))
                KeyMappings.Add(action, key);
        }

        public static ConsoleKey GetKey(string action)
        {
            return KeyMappings.TryGetValue(action, out var key) ? key : ConsoleKey.NoName;
        }

        public static void SetCustomMappings(Dictionary<string, ConsoleKey> customMappings)
        {
            KeyMappings = new Dictionary<string, ConsoleKey>(customMappings);
        }
    }
}