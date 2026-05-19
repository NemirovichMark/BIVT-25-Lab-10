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

        public override void SaveTasks(Lab9.White.White[] tasks)
        {
            var items = tasks.Select(t => new
            {
                Type = t.GetType().Name,
                Input = t.Input
            });
            var json = JsonSerializer.Serialize(items);
            EditFile(json);
        }

        public override Lab9.White.White[] LoadTasks()
        {
            var path = FullPath;
            if (!File.Exists(path)) return Array.Empty<Lab9.White.White>();

            var json = File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<List<JsonItem>>(json);
            if (items == null) return Array.Empty<Lab9.White.White>();

            var tasks = new List<Lab9.White.White>();
            foreach (var item in items)
            {
                switch (item.Type)
                {
                    case "Task1":
                        tasks.Add(new Task1(item.Input));
                        break;
                    case "Task2":
                        tasks.Add(new Task2(item.Input));
                        break;
                    case "Task3":
                        tasks.Add(new Task3(item.Input, new string[0, 0]));
                        break;
                    case "Task4":
                        tasks.Add(new Task4(item.Input));
                        break;
                }
            }
            return tasks.ToArray();
        }

        private class JsonItem
        {
            public string Type { get; set; }
            public string Input { get; set; }
        }
    }
}
