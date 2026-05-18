using System;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : global::Lab10.MyFileManager, global::Lab10.ISerializer<T>
    {
        public PurpleFileManager(string name) : base(name) { }

        public PurpleFileManager(string name, string folder, string fileName, string ext = "txt")
            : base(name, folder, fileName, ext) { }

        public abstract void Serialize(T obj);
        public abstract T? Deserialize();

        public override void EditFile(string content)
        {
            base.EditFile(content);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            base.ChangeFileExtension(newExtension);
        }
    }
}
