using Lab9.White;
using System.Text;

namespace Lab10.White
{
    public class WhiteTxtFileManager : WhiteFileManager
    {
        public WhiteTxtFileManager(string name) : base(name)
        {
            ChangeFileName("tasks");
            ChangeFileFormat("txt");
        }

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
                {
                    string codesStr = SerializeCodes(t3.Codes);
                    sb.AppendLine($"Task3|{t3.Input}|{codesStr}");
                }
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
                        if (parts.Length >= 3)
                        {
                            var codes = DeserializeCodes(parts[2]);
                            tasks.Add(new Task3(input, codes));
                        }
                        break;
                    case "Task4":
                        tasks.Add(new Task4(input));
                        break;
                }
            }
            return tasks.ToArray();
        }

        private string SerializeCodes(string[,] codes)
        {
            if (codes == null) return "";
            var list = new List<string>();
            for (int i = 0; i < codes.GetLength(0); i++)
                list.Add($"{codes[i,0]},{codes[i,1]}");
            return string.Join(";", list);
        }

        private string[,] DeserializeCodes(string data)
        {
            if (string.IsNullOrEmpty(data)) return new string[0, 0];
            var pairs = data.Split(';');
            var codes = new string[pairs.Length, 2];
            for (int i = 0; i < pairs.Length; i++)
            {
                var pair = pairs[i].Split(',');
                codes[i, 0] = pair[0];
                codes[i, 1] = pair[1];
            }
            return codes;
        }
    }
}
