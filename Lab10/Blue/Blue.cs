using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private T[] _t;
        private BlueFileManager<T> _manager;

        public T[] Tasks => _t;
        public BlueFileManager<T> Manager => _manager;

        public Blue(T[] t = null)
        {
            if (t is null) _t = new T[0];
            else _t = t.ToArray();
        }
        public Blue(BlueFileManager<T> manager, T[] t = null) : this(t)
        {
            _manager = manager;
        }
        public Blue(T[] t, BlueFileManager<T> manager) : this(manager, t) { }

        public void Add(T item)
        {
            if (_t == null) return;
            Array.Resize(ref _t, _t.Length + 1);
            _t[_t.Length - 1] = item;
        }
        public void Add(T[] elems) 
        {
            for (int i = 0; i < elems.Length; i++) Add(elems[i]);
        }
        public void Remove(T item)
        {
            if (_t == null) return;
            int index = Array.IndexOf(_t, item);
            if (index < 0) return;
            for (int i = index; i < _t.Length - 1; i++) _t[i] = _t[i + 1];
            Array.Resize(ref _t, _t.Length - 1);
        }
        public void Clear()
        {
            if (_t == null) return;
            _t = new T[0];

            if(Directory.Exists(_manager.FolderPath)) Directory.Delete(_manager.FolderPath, true);
        }
        public void SaveTasks()
        {
            if (_manager == null) return;
            if (_t == null || _t.Length == 0) return;

            foreach(var i in _t)
            {
                if (i is Lab9.Blue.Task1) _manager.ChangeFileName("task0");
                if (i is Lab9.Blue.Task2) _manager.ChangeFileName("task1");
                if (i is Lab9.Blue.Task3) _manager.ChangeFileName("task2");
                if (i is Lab9.Blue.Task4) _manager.ChangeFileName("task3");

                _manager.Serialize(i);
            }
        }
        public void LoadTasks()
        {
            if (_manager == null) return;
            if (_t == null) _t = new T[0];
            for(int i = 0; File.Exists(Path.Combine(_manager.FolderPath, $"task{i}.{_manager.FileExtension}")); i++)
            {
                _manager.ChangeFileName($"task{i}");
                var obj = _manager.Deserialize();
                Add(obj);
            }
        }
        public void ChangeManager(BlueFileManager<T> manager)
        {
            if (_manager == null) return;
            _manager = manager;
            _t = new T[0];
            if (!Directory.Exists(_manager.FolderPath))
            {
                Directory.CreateDirectory(_manager.Name);
                _manager.SelectFolder(_manager.Name);
            }
        }
    }
}
