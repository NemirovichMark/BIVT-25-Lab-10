using System.Reflection;
using System.Text;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        private const string InputMarker = "\nInput:\n";
        private const string WindowsInputMarker = "\r\nInput:\r\n";

        public BlueTxtFileManager(string name) : base(name)
        {
            ChangeFileFormat("txt");
        }

        public BlueTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension)
        {
            ChangeFileFormat("txt");
        }

        public override void EditFile(string content)
        {
            T? task = Deserialize();
            if (task is null)
            {
                return;
            }

            task.ChangeText(content ?? string.Empty);
            Serialize(task);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            base.ChangeFileExtension("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj is null || string.IsNullOrWhiteSpace(FullPath))
            {
                return;
            }

            string? sequence = TryGetTask2Sequence(obj);
            string sequenceBase64 = sequence is null
                ? string.Empty
                : Convert.ToBase64String(Encoding.UTF8.GetBytes(sequence));

            StringBuilder builder = new StringBuilder();
            builder.Append("Type:").Append(obj.GetType().FullName ?? obj.GetType().Name).Append('\n');
            builder.Append("SequenceIsNull:").Append(sequence is null ? "true" : "false").Append('\n');
            builder.Append("SequenceBase64:").Append(sequenceBase64).Append('\n');
            builder.Append("InputIsNull:").Append(obj.Input is null ? "true" : "false").Append('\n');
            builder.Append("Input:").Append('\n');
            if (obj.Input is not null)
            {
                builder.Append(obj.Input);
            }

            EnsureDirectory();
            File.WriteAllText(FullPath, builder.ToString());
        }

        public override T? Deserialize()
        {
            string path = FullPath;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return default;
            }

            try
            {
                string content = File.ReadAllText(path);
                int markerIndex = content.IndexOf(InputMarker, StringComparison.Ordinal);
                int markerLength = InputMarker.Length;

                if (markerIndex < 0)
                {
                    markerIndex = content.IndexOf(WindowsInputMarker, StringComparison.Ordinal);
                    markerLength = WindowsInputMarker.Length;
                }

                if (markerIndex < 0)
                {
                    return default;
                }

                string header = content[..markerIndex];
                string input = content[(markerIndex + markerLength)..];
                Dictionary<string, string> fields = ParseHeader(header);

                fields.TryGetValue("Type", out string? typeName);
                fields.TryGetValue("SequenceIsNull", out string? sequenceIsNullText);
                fields.TryGetValue("SequenceBase64", out string? sequenceBase64);
                fields.TryGetValue("InputIsNull", out string? inputIsNullText);

                string? restoredInput = string.Equals(inputIsNullText, "true", StringComparison.OrdinalIgnoreCase)
                    ? null
                    : input;

                string? restoredSequence = null;
                if (!string.Equals(sequenceIsNullText, "true", StringComparison.OrdinalIgnoreCase))
                {
                    restoredSequence = DecodeBase64(sequenceBase64);
                }

                Lab9.Blue.Blue? task = CreateTask(typeName, restoredInput, restoredSequence);
                if (task is null)
                {
                    return default;
                }

                task.Review();
                return task is T typedTask ? typedTask : default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
        }

        private static Dictionary<string, string> ParseHeader(string header)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>(StringComparer.Ordinal);
            string[] lines = header.Replace("\r", string.Empty, StringComparison.Ordinal).Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                int separatorIndex = lines[i].IndexOf(':');
                if (separatorIndex < 0)
                {
                    continue;
                }

                string key = lines[i][..separatorIndex];
                string value = lines[i][(separatorIndex + 1)..];
                fields[key] = value;
            }

            return fields;
        }

        private static string? DecodeBase64(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            byte[] bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }

        private static Lab9.Blue.Blue? CreateTask(string? typeName, string? input, string? sequence)
        {
            if (Matches(typeName, typeof(Lab9.Blue.Task1)))
            {
                return new Lab9.Blue.Task1(input!);
            }

            if (Matches(typeName, typeof(Lab9.Blue.Task2)))
            {
                return new Lab9.Blue.Task2(input!, sequence!);
            }

            if (Matches(typeName, typeof(Lab9.Blue.Task3)))
            {
                return new Lab9.Blue.Task3(input!);
            }

            if (Matches(typeName, typeof(Lab9.Blue.Task4)))
            {
                return new Lab9.Blue.Task4(input!);
            }

            return null;
        }

        private static bool Matches(string? storedTypeName, Type expectedType)
        {
            if (string.IsNullOrWhiteSpace(storedTypeName))
            {
                return false;
            }

            return string.Equals(storedTypeName, expectedType.FullName, StringComparison.Ordinal)
                || string.Equals(storedTypeName, expectedType.Name, StringComparison.Ordinal)
                || storedTypeName.EndsWith($".{expectedType.Name}", StringComparison.Ordinal);
        }

        private static string? TryGetTask2Sequence(Lab9.Blue.Blue task)
        {
            if (task is not Lab9.Blue.Task2)
            {
                return null;
            }

            FieldInfo? field = typeof(Lab9.Blue.Task2).GetField("_sequence", BindingFlags.Instance | BindingFlags.NonPublic);
            return field?.GetValue(task) as string;
        }

        private void EnsureDirectory()
        {
            string? directory = Path.GetDirectoryName(FullPath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
