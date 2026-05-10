using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private BlueFileManager<T> _manager;
        private T[] _tasks;

        public BlueFileManager<T> Manager => _manager;
        public T[] Tasks => _tasks;

        public Blue()
        {
            _manager = null;
            _tasks = Array.Empty<T>();
        }

        
        public Blue(T[] tasks)
        {
            _manager = null;
            _tasks = CopyArray(tasks);
        }

        
        public Blue(BlueFileManager<T> manager, T[] tasks = null)
        {
            _manager = manager;
            _tasks = CopyArray(tasks);
        }

        
        public Blue(T[] tasks, BlueFileManager<T> manager)
        {
            _manager = manager;
            _tasks = CopyArray(tasks);
        }
        private T[] CopyArray(T[] source)
        {
            if (source == null || source.Length == 0) return Array.Empty<T>();
            T[] copy = new T[source.Length];
            Array.Copy(source, copy, source.Length);
            return copy;
        }

        public void Add(T item)
        {
            if (item == null) return;
            T[] newArr = new T[_tasks.Length + 1];
            Array.Copy(_tasks, newArr, _tasks.Length);
            newArr[_tasks.Length] = item;
            _tasks = newArr;
        }

        public void Add(T[] items)
        {
            if (items == null) return;
            foreach (var item in items) Add(item);
        }

        public void Remove(T item)
        {
            if (item == null || _tasks.Length == 0) return;
            int idx = Array.IndexOf(_tasks, item);
            if (idx == -1) return;
            T[] newArr = new T[_tasks.Length - 1];
            Array.Copy(_tasks, 0, newArr, 0, idx);
            if (idx < _tasks.Length - 1)
                Array.Copy(_tasks, idx + 1, newArr, idx, _tasks.Length - idx - 1);
            _tasks = newArr;
        }
        public void Clear()
        {
            _tasks = Array.Empty<T>();
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
                _manager.ChangeFileName($"task_{i}");
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null || _tasks == null) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task_{i}");
               
                _tasks[i] = _manager.Deserialize();
            }
        }
        public void ChangeManager(BlueFileManager<T> newManager)
        {
            if (newManager == null) return;
            if (!string.IsNullOrEmpty(newManager.Name) && !Directory.Exists(newManager.Name))
            {
                Directory.CreateDirectory(newManager.Name);
            }
            newManager.SelectFolder(newManager.Name ?? "");
            _manager = newManager;
        }
    }

}
