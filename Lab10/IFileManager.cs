namespace Lab10
{
    public interface IFileManager
    {
        public string FolderPath { get; }
        public string FileName { get; }
        public string FileExtension { get; }
        public string FullPath { get; }

        public void SelectFolder(string folderPath);
        public void SelectFile(string fileName);
        public void ChangeFileName(string fileName);
        public void ChangeFormat(string fileExtension);
        public void ChangeFileFormat(string fileExtension);
    }
}
