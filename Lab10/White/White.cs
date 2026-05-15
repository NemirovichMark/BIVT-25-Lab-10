using System;
using System.IO;

namespace Lab10.White
{
    public class White : Lab9.White.White
    {
        private Lab10.White.WhiteFileManager _manager;
        private Lab10.White.White[] _tasks = Array.Empty<Lab10.White.White>();
        private string _text = "";

        public Lab10.White.WhiteFileManager Manager => _manager;
        public Lab10.White.White[] Tasks => _tasks;

        public new string Text  
        {  
            get => _text;  
            set => _text = value;  
        }

        public White() : base("") { }
        
        public White(string text) : base(text)  
        {  
            _text = text;  
        }

        public White(Lab10.White.White[] tasks) : base("")  
        {  
            _tasks = tasks ?? Array.Empty<Lab10.White.White>();  
        }

        public White(Lab10.White.WhiteFileManager manager, Lab10.White.White[] tasks = null) : base("")
        {
            _manager = manager;
            _tasks = tasks ?? Array.Empty<Lab10.White.White>();
        }

        public new void ChangeText(string newText) => _text = newText;

        public void Add(Lab10.White.White task)
        {
            if (task == null) return;
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[^1] = task;
        }

        public void SaveTasks()
        {
            if (_manager == null) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"Task_{i}");
                _manager.Serialize((Lab9.White.White)_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null || !Directory.Exists(_manager.FolderPath)) return;
            _tasks = Array.Empty<Lab10.White.White>();
            foreach (var file in Directory.GetFiles(_manager.FolderPath))
            {
                _manager.ChangeFileName(Path.GetFileNameWithoutExtension(file));
                if (_manager.Deserialize() is Lab10.White.White loadedTask)
                    Add(loadedTask);
            }
        }

        public override void Review() { }

        public void ChangeManager(Lab10.White.WhiteFileManager newManager) => _manager = newManager;

        public void Clear()
        {
            _tasks = Array.Empty<Lab10.White.White>();
            if (_manager != null && Directory.Exists(_manager.FolderPath))
                Directory.Delete(_manager.FolderPath, true);
        }
    }
}
