using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10.Purple;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name) { }
        public PurpleFileManager(string name, string folderPath, string filename, string fileExtension = "") : base(name, folderPath, filename, fileExtension) { }
        public override void EditFile(string a)
        {
            if (!File.Exists(FullPath)) return;
            File.WriteAllText(FullPath, a);
        }
        public override void ChangeFileExtension(string e)
        {
            if (!File.Exists(FullPath)) return;
            string Path1=FullPath; // где файл лежал ДО изменения расширения
            base.ChangeFileFormat(e); // Меняем расширение в памяти
            string Path2=FullPath; // ПОСЛЕ изменения
            File.Move(Path1, Path2); //Перемещает/переименовывает файл на диске (физ)
        }
        public abstract T Deserialize();
        public abstract void Serialize(T obj);
    }
}
