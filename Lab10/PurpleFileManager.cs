using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name)
        {
        }

        public PurpleFileManager(string name, string foldername, string filename, string extension ="txt")
            : base(name, foldername, filename, extension)
        {
        }

        public override void EditFile(string str)
        {
            if (!File.Exists(FullPath)) return;
            base.EditFile(str);
        }

        public override void ChangeFileExtension(string change_extension)
        {
            if (!File.Exists(FullPath)) return;
            base.ChangeFileExtension(change_extension);
        }

        public abstract T Deserialize();
        public abstract void Serialize(T obj);
    }
}