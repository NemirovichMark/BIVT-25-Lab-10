using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private PurpleFileManager<T>? _manager;
        private T[] _tasks;

        public PurpleFileManager<T>? Manager => _manager;
        public T[] Tasks => _tasks;

        public Purple(T[]? tasks = null)
        {
            if (tasks == null)
                _tasks = new T[0];
            else
                _tasks = tasks;
        }

        public Purple(PurpleFileManager<T> manager, T[]? tasks = null)
        {
            _manager = manager;
            if (tasks != null)
                _tasks = tasks;
        }
        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            if (tasks == null)
                _tasks = new T[0];
            else
                _tasks = tasks;
            _manager = manager;
        }

        public void Add(T task)
        {
            if(task != null)
            {
                Array.Resize(ref _tasks, _tasks.Length + 1);
                _tasks[_tasks.Length - 1] = task;
            }
        }
        public void Add(T[] tasks)
        {
            if(tasks != null)
            {
                foreach (T el in tasks)
                    Add(el);
            }
        }
        public void Remove(T task)
        {
            if(task != null && _tasks.Length > 0 && Array.IndexOf(_tasks, task) != -1)
            {
                int i = Array.IndexOf(_tasks, task);
                T[] result = new T[_tasks.Length - 1];
                int x = 0, y = 0;
                while(x < result.Length)
                {
                    if (y == i)
                    {
                        y++;
                        continue;
                    }
                    result[x++] = _tasks[y++];
                }
                _tasks = result;
            }
        }
        public void Clear()
        {
            _tasks = Array.Empty<T>();
            if (Directory.Exists(_manager.FolderPath) && !string.IsNullOrEmpty(_manager.FolderPath) && _manager != null)
            {
                Directory.Delete(_manager.FolderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (_manager != null)
            {
                for (int x = 0; x < _tasks.Length; x++)
                {
                    _manager.ChangeFileName($"task{x}");
                    _manager.Serialize(_tasks[x]);
                }
            }
        }

        public void LoadTasks()
        {
            if(_manager != null)
            {
                for(int x = 0; x < _tasks.Length; x++)
                {
                    _manager.ChangeFileName($"task{x}");
                    _tasks[x] = _manager.Deserialize();
                }
            }
        }
        public void ChangeManager(PurpleFileManager<T> manager)
        {
            if(_manager != null && !string.IsNullOrWhiteSpace(_manager.FolderPath))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), manager.Name);
                Directory.CreateDirectory(path);
                _manager.SelectFolder(path);
                _manager = manager;
            }
        }
    }
}
