using System.Text;
using Lab9.White;

namespace Lab10.White
{
    public class WhiteTxtFileManager : WhiteFileManager
    {
        public WhiteTxtFileManager(string name) : base(name)
        {
            ChangeFileName("tasks");
            ChangeFileFormat("txt");
        }

        // Массовая сериализация (для WhiteManagerTest)
        public override void SaveTasks(White[] tasks)
        {
            if (tasks == null) return;
            var sb = new StringBuilder();
            foreach (var t in tasks)
            {
                if (t is Task1 t1)
                    sb.AppendLine($"Task1|{t1.Input}");
                else if (t is Task2 t2)
                    sb.AppendLine($"Task2|{t2.Input}");
                else if (t is Task3 t3)
                    sb.AppendLine($"Task3|{t3.Input}");
                else if (t is Task4 t4)
                    sb.AppendLine($"Task4|{t4.Input}");
            }
            EditFile(sb.ToString());
        }

        public override White[] LoadTasks()
        {
            var path = FullPath;
            if (!File.Exists(path)) return Array.Empty<White>();

            var lines = File.ReadAllLines(path);
            var tasks = new List<White>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (parts.Length < 2) continue;

                string type = parts[0];
                string input = parts[1];
                switch (type)
                {
                    case "Task1":
                        tasks.Add(new Task1(input));
                        break;
                    case "Task2":
                        tasks.Add(new Task2(input));
                        break;
                    case "Task3":
                        tasks.Add(new Task3(input, new string[0, 0]));
                        break;
                    case "Task4":
                        tasks.Add(new Task4(input));
                        break;
                }
            }
            return tasks.ToArray();
        }

        // Одиночная сериализация (для GeneralTest)
        public override void Serialize(White obj)
        {
            SaveTasks(new[] { obj });
        }

        public override White Deserialize()
        {
            var tasks = LoadTasks();
            return tasks.Length > 0 ? tasks[0] : null;
        }
    }
}
