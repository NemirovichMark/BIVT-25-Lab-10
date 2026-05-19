using System.Text.Json;
using Lab9.White;

namespace Lab10.White
{
    public class WhiteJsonFileManager : WhiteFileManager
    {
        public WhiteJsonFileManager(string name) : base(name)
        {
            ChangeFileName("tasks");
            ChangeFileFormat("json");
        }

        // Массовая сериализация
        public override void SaveTasks(White[] tasks)
        {
            var items = tasks.Select(t => new { Type = t.GetType().Name, Input = t.Input });
            var json = JsonSerializer.Serialize(items);
            EditFile(json);
        }

        public override White[] LoadTasks()
        {
            var path = FullPath;
            if (!File.Exists(path)) return Array.Empty<White>();

            var json = File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<List<JsonItem>>(json);
            if (items == null) return Array.Empty<White>();

            return items.Select(item => item.Type switch
            {
                "Task1" => new Task1(item.Input),
                "Task2" => new Task2(item.Input),
                "Task3" => new Task3(item.Input, new string[0, 0]),
                "Task4" => new Task4(item.Input),
                _ => null
            }).Where(t => t != null).ToArray()!;
        }

        // Одиночная сериализация
        public override void Serialize(White obj)
        {
            SaveTasks(new[] { obj });
        }

        public override White Deserialize()
        {
            var tasks = LoadTasks();
            return tasks.Length > 0 ? tasks[0] : null;
        }

        private class JsonItem
        {
            public string Type { get; set; } = "";
            public string Input { get; set; } = "";
        }
    }
}
