using System.Reflection;
using System.Text.Json;

namespace Lab10.Blue
{
    public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        private sealed class BlueJsonData
        {
            public string? Type { get; set; }
            public string? Input { get; set; }
            public string? Sequence { get; set; }
        }

        public BlueJsonFileManager(string name) : base(name)
        {
            ChangeFileFormat("json");
        }

        public BlueJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json")
            : base(name, folderPath, fileName, fileExtension)
        {
            ChangeFileFormat("json");
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
            base.ChangeFileExtension("json");
        }

        public override void Serialize(T obj)
        {
            if (obj is null || string.IsNullOrWhiteSpace(FullPath))
            {
                return;
            }

            BlueJsonData data = new BlueJsonData
            {
                Type = obj.GetType().FullName ?? obj.GetType().Name,
                Input = obj.Input,
                Sequence = TryGetTask2Sequence(obj)
            };

            EnsureDirectory();
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FullPath, json);
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
                string json = File.ReadAllText(path);
                BlueJsonData? data = JsonSerializer.Deserialize<BlueJsonData>(json);
                if (data is null)
                {
                    return default;
                }

                Lab9.Blue.Blue? task = CreateTask(data.Type, data.Input, data.Sequence);
                if (task is null)
                {
                    return default;
                }

                task.Review();
                return task is T typedTask ? typedTask : default;
            }
            catch (JsonException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
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
