using System;
using System.IO;
using System.Linq;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        //parameters
        private T?[] _tasks = new T?[0];
        private PurpleFileManager<T>? _manager;

        //constructors
        public Purple()
        {
            _tasks = new T[0];
        }

        public Purple(PurpleFileManager<T>? manager, T[]? tasks = null)
        {
            _manager = manager;
            _tasks = tasks == null ? new T?[0] : tasks.ToArray();
        }

        public Purple(T[]? tasks, PurpleFileManager<T>? manager)
        {
            _tasks = tasks == null ? new T?[0] : tasks.ToArray();
            _manager = manager;
        }

        //properties
        public PurpleFileManager<T>? Manager { get { return _manager; } }
        public T?[] Tasks { get { return _tasks; } }

        //methods
        public void Add(T item)
        {
            var list = _tasks.ToList();
            list.Add(item);
            _tasks = list.ToArray();
        }

        public void Add(T[]? items)
        {
            if (items == null) return;
            var list = _tasks.ToList();
            list.AddRange(items);
            _tasks = list.ToArray();
        }

        public void Remove(T item)
        {
            _tasks = _tasks.Where(t => !object.ReferenceEquals(t, item)).ToArray();
        }

        public void Clear()
        {
            _tasks = new T[0];
            try
            {
                    if (_manager != null && Directory.Exists(_manager.FolderPath))
                        Directory.Delete(_manager.FolderPath, true);
            }
            catch { }
        }

        public void SaveTasks()
        {
            if (_manager == null) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                var item = _tasks[i];
                if (item != null)
                    _manager.Serialize(item);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                try
                {
                    var obj = _manager.Deserialize();
                    _tasks[i] = obj;
                }
                catch
                {
                    _tasks[i] = null;
                }
            }
        }

        public void ChangeManager(PurpleFileManager<T> newManager)
        {
            _manager = newManager;
            var name = newManager?.Name ?? "";
            var folder = Path.Combine(Directory.GetCurrentDirectory(), name);
            Directory.CreateDirectory(folder);
            newManager.SelectFolder(folder);
        }
    }
}
