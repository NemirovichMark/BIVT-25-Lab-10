using System;
using System.IO;
using System.Linq;

namespace Lab10.White
{
    public class White
    {
        private WhiteFileManager _manager;
        private Lab9.White.White[] _tasks;

        public WhiteFileManager Manager => _manager;
        public Lab9.White.White[] Tasks => _tasks;

        public White(Lab9.White.White[] tasks)
        {
            _tasks = tasks?.ToArray() ?? Array.Empty<Lab9.White.White>();
        }

        public White(WhiteFileManager manager, Lab9.White.White[] tasks = null)
        {
            _manager = manager;
            _tasks = tasks?.ToArray() ?? Array.Empty<Lab9.White.White>();
        }

        public White(Lab9.White.White[] tasks, WhiteFileManager manager)
        {
            _manager = manager;
            _tasks = tasks?.ToArray() ?? Array.Empty<Lab9.White.White>();
        }

        public void Add(Lab9.White.White task)
        {
            if (task == null) return;
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[^1] = task;
        }

        public void Add(Lab9.White.White[] tasks)
        {
            if (tasks == null) return;
            foreach (var t in tasks) Add(t);
        }

        public void Remove(Lab9.White.White task)
        {
            if (task == null) return;
            _tasks = _tasks.Where(t => t != task).ToArray();
        }

        public void Clear()
        {
            _tasks = Array.Empty<Lab9.White.White>();
            if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath) && Directory.Exists(_manager.FolderPath))
                Directory.Delete(_manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            if (_manager == null) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task_{i}");
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null || !Directory.Exists(_manager.FolderPath)) return;
            var files = Directory.GetFiles(_manager.FolderPath);
            _tasks = new Lab9.White.White[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                _manager.ChangeFileName(Path.GetFileNameWithoutExtension(files[i]));
                _tasks[i] = _manager.Deserialize();
            }
        }

        public void ChangeManager(WhiteFileManager newManager)
        {
            _manager = newManager;
            if (_manager != null)
            {
                _manager.SelectFolder(_manager.Name);
                if (!Directory.Exists(_manager.FolderPath))
                    Directory.CreateDirectory(_manager.FolderPath);
            }
        }
    }
}
