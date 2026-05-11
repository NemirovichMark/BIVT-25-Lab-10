using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Lab10.Blue
{
    public class BlueJsonFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
    {
        public BlueJsonFileManager(string name) : base(name) { _fileExtension = "json"; }
        public BlueJsonFileManager(string name, string fileName, string folderPath, string fileExtension = "") : base(name, fileName, folderPath, fileExtension) { _fileExtension = "json"; }

        public override void EditFile(string text)
        {
            if (IOE(text) || !File.Exists(FullPath)) return;
            var obj = Deserialize();
            obj.ChangeText(text);
            Serialize(obj);
        }
        public override void ChangeFileExtension(string text)
        {
            if (IOE(text) || !File.Exists(FullPath)) return;
            ChangeFileFormat("json");
        }

        public override void Serialize(T elem)
        {
            if (elem == null) return;
            string json = JsonSerializer.Serialize(elem, options);
            Directory.CreateDirectory(FolderPath);
            File.WriteAllText(FullPath, json);
        }
        public override T Deserialize()
        {
            if(!File.Exists(FullPath)) return null;
            string json = File.ReadAllText(FullPath);
            T? obj = JsonSerializer.Deserialize<T>(json, options);
            obj.Review();
            return obj;
        }
    }
}
