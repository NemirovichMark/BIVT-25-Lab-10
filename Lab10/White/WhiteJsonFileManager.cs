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
            if (!File.Exists(path)) return System.Array.Empty<White>();

            var json = File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<List<JsonItem>>(json);
            if (items == null) return System.Array.Empty<White>();

            var result = new List<White>();
            foreach (var item in items)
            {
                switch (item.Type)
                {
                    case "Task1":
                        result.Add(new Task1(item.Input));
                        break;
                    case "Task2":
                        result.Add(new Task2(item.Input));
                        break;
                    case "Task3":
                        result.Add(new Task3(item.Input, new string[0, 0]));
                        break;
                    case "Task4":
                        result.Add(new Task4(item.Input));
                        break;
                }
            }
            return result.ToArray();
        }

        // Одиночная сериализация
        public override void Serialize(White obj)
        {
            if (obj == null) return;
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
