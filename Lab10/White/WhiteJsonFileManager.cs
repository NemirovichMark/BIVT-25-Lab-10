using Lab9.White;
using System.Text.Json;

namespace Lab10.White
{
    public class WhiteJsonFileManager : WhiteFileManager
    {
        public WhiteJsonFileManager(string name) : base(name)
        {
            ChangeFileName("tasks");
            ChangeFileFormat("json");
        }

        public override void SaveTasks(White[] tasks)
        {
            var items = tasks.Select(t => new
            {
                Type = t.GetType().Name,
                Data = t
            });
            var json = JsonSerializer.Serialize(items);
            EditFile(json);
        }

        public override White[] LoadTasks()
        {
            var path = FullPath;
            if (!File.Exists(path)) return Array.Empty<White>();

            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var tasks = new List<White>();

            foreach (var item in root.EnumerateArray())
            {
                string typeName = item.GetProperty("Type").GetString();
                JsonElement data = item.GetProperty("Data");

                White task = typeName switch
                {
                    "Task1" => JsonSerializer.Deserialize<Task1>(data.GetRawText()),
                    "Task2" => JsonSerializer.Deserialize<Task2>(data.GetRawText()),
                    "Task3" => JsonSerializer.Deserialize<Task3>(data.GetRawText()),
                    "Task4" => JsonSerializer.Deserialize<Task4>(data.GetRawText()),
                    _ => null
                };
                if (task != null) tasks.Add(task);
            }
            return tasks.ToArray();
        }
    }
}
