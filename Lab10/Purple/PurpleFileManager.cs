using Lab10.Interfaces;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : global::Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name) { }

        public PurpleFileManager(string name, string folder_path, string file_name, string file_extension = "")
            : base(name, folder_path, file_name, file_extension) { }

        public override void EditFile(string text)
        {
            if (!string.IsNullOrEmpty(this.FullPath) && File.Exists(this.FullPath))
                File.WriteAllText(this.FullPath, text ?? string.Empty);
        }

        public override void ChangeFileExtension(string new_extension)
        {
            if (string.IsNullOrEmpty(this.FullPath))
                return;
            string old_path = this.FullPath;
            this.ChangeFileFormat(new_extension);
            string new_path = this.FullPath;
            if (File.Exists(old_path) && old_path != new_path)
            {
                if (File.Exists(new_path))
                    File.Delete(new_path);
                File.Move(old_path, new_path);
            }
        }

        public abstract void Serialize(T obj);
        public abstract T Deserialize();
    }
}