using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Lab10.Blue
{
    public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueJsonFileManager(string name) : base(name) { }
        public BlueJsonFileManager(string name, string folderPath, string fileName, string extension = "txt")
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
            ChangeFileFormat("json");
            base.ChangeFileExtension("json");
        }

        public override void Serialize(T obj)
        {
            if (obj == null) return;
            CreateFile();

            var data = new Dictionary<string, string>
            {
                ["Type"] = obj.GetType().AssemblyQualifiedName,
                ["Input"] = obj.Input
            };

            string seq = GetSequence(obj);
            if (seq != null) data["Sequence"] = seq;

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize()
        {
            try
            {
                if (string.IsNullOrEmpty(FullPath) || !File.Exists(FullPath)) return null;
                string json = File.ReadAllText(FullPath);

                
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                string typeStr = root.TryGetProperty("Type", out var t) ? t.GetString() : null;
                string inputStr = root.TryGetProperty("Input", out var i) ? i.GetString() : null;
                string seqStr = root.TryGetProperty("Sequence", out var s) ? s.GetString() : null;

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
