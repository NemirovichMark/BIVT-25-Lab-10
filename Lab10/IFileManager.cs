namespace Lab10
{
    public interface IFileManager
    {
        //parameters
        public string FolderPath { get;}
        public string FileName { get; }
        public string FileExtension { get; }
        public string FullPath { get; }

        //methods
        void SelectFolder(string Folder);
        void ChangeFileName(string File);
        void ChangeFileFormat(string File);
    }
}