using System;
using System.IO;

namespace Lab10.White
{
    public class White : Lab9.White.White
    {
        private Lab10.White.WhiteFileManager _manager;
        private Lab10.White.White[] _tasks;
        private string _text;

        public Lab10.White.WhiteFileManager Manager => _manager;
        public Lab10.White.White[] Tasks => _tasks;
        
        public new string Text
        {
            get => _text;
            set => _text = value;
        }

        public White() : base("")
        {
            _text = "";
            _tasks = new Lab10.White.White[0];
        }

        public White(string text) : base(text)
        {
            _text = text;
            _tasks = new Lab10.White.White[0];
        }

        public White(Lab10.White.White[] tasks) : base("")
        {
            _text = "";
            _tasks = tasks ?? new Lab10.White.White[0];
        }

        public White(Lab10.White.WhiteFileManager manager, Lab10.White.White[] tasks = null) : base("")
        {
            _manager = manager;
            _tasks = tasks ?? new Lab10.White.White[0];
            _text = "";
        }

        public White(Lab10.White.White[] tasks, Lab10.White.WhiteFileManager manager) : base("")
        {
            _manager = manager;
            _tasks = tasks ?? new Lab10.White.White[0];
            _text = "";
        }

        public new void ChangeText(string newText)
        {
            _text = newText;
        }

        public void Add(Lab10.White.White task)
        {
            if (task == null) return;
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = task;
        }

        public void Add(Lab10.White.White[] tasks)
        {
            if (tasks == null) return;
            foreach (var t in tasks) Add(t);
        }

        public void Remove(Lab10.White.White task)
        {
            if (task == null || _tasks == null || _tasks.Length == 0) return;
            int index = Array.IndexOf(_tasks, task);
            if (index == -1) return;
            Lab10.White.White[] newTasks = new Lab10.White.White[_tasks.Length - 1];
            Array.Copy(_tasks, 0, newTasks, 0, index);
            Array.Copy(_tasks, index + 1, newTasks, index, _tasks.Length - index - 1);
            _tasks = newTasks;
        }

        public void Clear()
        {
            _tasks = new Lab10.White.White[0];
            if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath) && Directory.Exists(_manager.FolderPath))
            {
                Directory.Delete(_manager.FolderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (_manager == null || _tasks == null) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == null) continue;
                _manager.ChangeFileName("Task_" + i);
                _manager.Serialize((Lab9.White.White)_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null || string.IsNullOrEmpty(_manager.FolderPath) || !Directory.Exists(_manager.FolderPath)) return;
            string[] files = Directory.GetFiles(_manager.FolderPath);
            _tasks = new Lab10.White.White[0];
            foreach (string file in files)
            {
                _manager.ChangeFileName(Path.GetFileNameWithoutExtension(file));
                Lab9.White.White baseObj = _manager.Deserialize();
                if (baseObj is Lab10.White.White task)
                {
                    Add(task);
                }
            }
        }

        public override void Review() { }

        public void ChangeManager(Lab10.White.WhiteFileManager newManager)
        {
            _manager = newManager;
            if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath))
            {
                Directory.CreateDirectory(_manager.FolderPath);
            }
        }
    }
}
