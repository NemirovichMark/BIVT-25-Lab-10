using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class WhiteTxtFileManager : WhiteFileManager
    {
        public WhiteTxtFileManager(string name) : base(name) { }

        public WhiteTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string content)
        {
            White obj = Deserialize();
            obj?.ChangeText(content);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            if (newExtension?.ToLower() != "txt")
                ChangeFileFormat("txt");
            else
                base.ChangeFileExtension(newExtension);
        }

        public override void Serialize(White obj)
        {
            if (obj == null) return;

            var sb = new StringBuilder();
            sb.AppendLine($"TypeName:{obj.GetType().AssemblyQualifiedName}");

            // Сохраняем Input (основные данные)
            if (obj is Task1 t1)
                sb.AppendLine($"Input:{EscapeString(t1.Input)}");
            else if (obj is Task2 t2)
                sb.AppendLine($"Input:{EscapeString(t2.Input)}");
            else if (obj is Task3 t3)
            {
                sb.AppendLine($"Input:{EscapeString(t3.Input)}");
                // Для Task3 нужно сохранить таблицу кодов
                var field = typeof(Task3).GetField("_codeTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var codeTable = field?.GetValue(t3) as string[,];
                if (codeTable != null)
                {
                    sb.AppendLine($"CodeTableRows:{codeTable.GetLength(0)}");
                    for (int i = 0; i < codeTable.GetLength(0); i++)
                    {
                        sb.AppendLine($"CodeTable:{EscapeString(codeTable[i, 0])}:{EscapeString(codeTable[i, 1])}");
                    }
                }
            }
            else if (obj is Task4 t4)
                sb.AppendLine($"Input:{EscapeString(t4.Input)}");

            EditFile(sb.ToString());
        }

        public override White Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            try
            {
                string[] lines = File.ReadAllLines(FullPath);
                string typeName = null;
                string input = null;
                string[,] codeTable = null;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    if (line.StartsWith("TypeName:"))
                        typeName = line.Substring(9);
                    else if (line.StartsWith("Input:"))
                        input = UnescapeString(line.Substring(6));
                    else if (line.StartsWith("CodeTableRows:"))
                    {
                        int rows = int.Parse(line.Substring(14));
                        codeTable = new string[rows, 2];
                    }
                    else if (line.StartsWith("CodeTable:") && codeTable != null)
                    {
                        string parts = line.Substring(10);
                        int colonIndex = parts.IndexOf(':');
                        if (colonIndex > 0)
                        {
                            string key = UnescapeString(parts.Substring(0, colonIndex));
                            string value = UnescapeString(parts.Substring(colonIndex + 1));
                            for (int i = 0; i < codeTable.GetLength(0); i++)
                            {
                                if (codeTable[i, 0] == null)
                                {
                                    codeTable[i, 0] = key;
                                    codeTable[i, 1] = value;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(input))
                    return null;

                Type type = Type.GetType(typeName);
                if (type == null) return null;

                if (type == typeof(Task1))
                    return new Task1(input);
                else if (type == typeof(Task2))
                    return new Task2(input);
                else if (type == typeof(Task3) && codeTable != null)
                    return new Task3(input, codeTable);
                else if (type == typeof(Task4))
                    return new Task4(input);

                return null;
            }
            catch
            {
                return null;
            }
        }

        private string EscapeString(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private string UnescapeString(string s)
        {
            if (s == null) return "";
            return s.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\\\", "\\");
        }
    }
}
