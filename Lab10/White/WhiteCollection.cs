using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab9.White
{
    public class WhiteCollection
    {
        private WhiteFileManager _manager;
        private White[] _tasks;

        public WhiteFileManager Manager => _manager;
        public White[] Tasks => _tasks;

        // Конструктор 1: только массив задач
        public WhiteCollection(White[] tasks = null)
        {
            _tasks = tasks != null ? (White[])tasks.Clone() : new White[0];
            _manager = new WhiteJsonFileManager("DefaultManager");
        }

        // Конструктор 2: менеджер + массив
        public WhiteCollection(WhiteFileManager manager, White[] tasks = null)
        {
            _manager = manager ?? new WhiteJsonFileManager("DefaultManager");
            _tasks = tasks != null ? (White[])tasks.Clone() : new White[0];
        }

        // Конструктор 3: массив + менеджер
        public WhiteCollection(White[] tasks, WhiteFileManager manager)
        {
            _manager = manager ?? new WhiteJsonFileManager("DefaultManager");
            _tasks = tasks != null ? (White[])tasks.Clone() : new White[0];
        }

        public void Add(White task)
        {
            if (task == null) return;
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = task;
        }

        public void Add(White[] tasks)
        {
            if (tasks == null) return;
            foreach (var task in tasks)
            {
                Add(task);
            }
        }

        public void Remove(White task)
        {
            if (task == null) return;
            int index = Array.IndexOf(_tasks, task);
            if (index >= 0)
            {
                for (int i = index; i < _tasks.Length - 1; i++)
                    _tasks[i] = _tasks[i + 1];
                Array.Resize(ref _tasks, _tasks.Length - 1);
            }
        }

        public void Clear()
        {
            _tasks = new White[0];
            if (_manager != null && Directory.Exists(_manager.FolderPath))
            {
                Directory.Delete(_manager.FolderPath, true);
            }
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
            if (_manager == null) return;

            var loadedTasks = new System.Collections.Generic.List<White>();
            int index = 0;

            while (true)
            {
                _manager.ChangeFileName($"task_{index}");
                White task = _manager.Deserialize();
                if (task == null) break;
                loadedTasks.Add(task);
                index++;
            }

            _tasks = loadedTasks.ToArray();
        }

        public void ChangeManager(WhiteFileManager newManager)
        {
            if (newManager == null) return;
            _manager = newManager;

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), _manager.Name);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            _manager.SelectFolder(folderPath);
        }
    }
}
