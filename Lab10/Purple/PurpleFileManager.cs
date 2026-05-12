using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name) { }
        public PurpleFileManager(string name, string folderPath, string filename, string fileExtension = "") : base(name, folderPath, filename, fileExtension) { }
        public override void EditFile(string a)
        {
            if (!File.Exists(FullPath) || string.IsNullOrEmpty(FullPath)) return;
            File.WriteAllText(FullPath, a);
        }
        public override void ChangeFileExtension(string e)
        {
             if (!File.Exists(FullPath)) return;
            string Path1 = FullPath; // Запоминаем старый путь
            string b = File.ReadAllText(Path1); // Запоминаем  содержимое файла 
            string ext = e;
            base.ChangeFileFormat(ext); // меняет расширение в памяти
            string Path2 = FullPath; // Запоминаем новый полный путь
            if (File.Exists(Path2))  File.Delete(Path2);
            File.WriteAllText(Path2, b);
            if (File.Exists(Path1) && Path1 != Path2) File.Delete(Path1);
        }
        public abstract T Deserialize();
        public abstract void Serialize(T obj);
    }
}
