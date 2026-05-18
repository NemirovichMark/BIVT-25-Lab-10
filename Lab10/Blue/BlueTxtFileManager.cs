using System;
using System.IO;
using System.Reflection;

namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueTxtFileManager(string name) : base(name) { }
        public BlueTxtFileManager(string name, string folderPath, string fileName, string extension = "txt")
            : base(name, folderPath, fileName, extension) { }

        public override void EditFile(string content)
        {
            if (string.IsNullOrEmpty(FullPath)) return;
            T obj = Deserialize();
            if (obj != null)
            {
                obj.ChangeText(content);
                Serialize(obj);
            }
        }

        public override void ChangeFileExtension(string newExtension)
        {
            if (string.IsNullOrEmpty(FullPath)) return;
            ChangeFileFormat("txt");
            base.ChangeFileExtension("txt");
        }

        public override void Serialize(T obj)
        {
            if (obj == null) return;
            CreateFile();

            string seq = GetSequence(obj);
            string content = $"Type: {obj.GetType().AssemblyQualifiedName}\nInput: {obj.Input}";
            if (seq != null) content += $"\nSequence: {seq}";

            File.WriteAllText(FullPath, content);
        }

        public override T Deserialize()
        {
            try
            {
                if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return null;
                string[] lines = File.ReadAllLines(FullPath);

                string typeStr = null, inputStr = null, seqStr = null;
                foreach (var line in lines)
                {
                    if (line.StartsWith("Type: ")) typeStr = line.Substring(6);
                    else if (line.StartsWith("Input: ")) inputStr = line.Substring(7);
                    else if (line.StartsWith("Sequence: ")) seqStr = line.Substring(10);
                }

                if (typeStr == null || inputStr == null) return null;
                Type type = Type.GetType(typeStr);
                if (type == null) return null;

                return CreateInstance(type, inputStr, seqStr);
            }
            catch
            {
                return null;
            }
        }

        private T CreateInstance(Type type, string input, string sequence)
        {
            var ctor2 = type.GetConstructor(new[] { typeof(string), typeof(string) });
            if (ctor2 != null && sequence != null)
            {
                var obj = (T)ctor2.Invoke(new object[] { input, sequence });
                obj.Review();
                return obj;
            }

            var ctor1 = type.GetConstructor(new[] { typeof(string) });
            if (ctor1 != null)
            {
                var obj = (T)ctor1.Invoke(new object[] { input });
                obj.Review();
                return obj;
            }

            return null;
        }

        private string GetSequence(T obj)
        {
            var field = obj.GetType().GetField("sequence", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(obj) as string;
        }
    }
}
