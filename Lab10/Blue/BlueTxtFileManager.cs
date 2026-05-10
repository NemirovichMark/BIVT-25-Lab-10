using System;
using System.IO;
namespace Lab10.Blue
{
    public class BlueTxtFileManager<T> : BlueFileManager<T> where T : Blue
    {
        public BlueTxtFileManager(string name) : base(name) { }
        public BlueTxtFileManager(string name, string folder, string file, string ext = "txt")
            : base(name, folder, file, ext) { }

        public override void Serialize(T obj)
        {
            if (obj == null) return;
            File.WriteAllText(FullPath, $"Input:{obj.Input}");
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;
            var content = File.ReadAllText(FullPath);
            string input = content.Contains(":") ? content.Split(':')[1] : string.Empty;
            return (T)Activator.CreateInstance(typeof(T), input);
        }
    }
}
