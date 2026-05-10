using Lab9.Blue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private T[] _tasks;
        private BlueFileManager<T> _manager;
        public BlueFileManager<T> Manager => _manager;
        public T[] Tasks => (T[])_tasks.Clone();
        public Blue()
        {
            _tasks = Array.Empty<T>();
        }
        public Blue(T[] array)
        {
            _tasks = array != null ? array : Array.Empty<T>();
        }
        public Blue(BlueFileManager<T> manager,T[] tasks=null)
        {
            _tasks = tasks != null ? tasks : Array.Empty<T>();
            _manager = manager;
        }
        public Blue(T[] tasks,BlueFileManager<T> manager)
        {
            _tasks = tasks != null ? tasks : Array.Empty<T>();
            _manager = manager;
        }
        public void Add(T item)
        {
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[^1] = item;
        }
        public void Add(T[] items)
        {
            foreach(var item in items)
            {
                Add(item);
            }
        }
        public void Remove(T item)
        {
            T[] newArray= new T[_tasks.Length - 1];
            int j = 0;
            for(int i=0;i< _tasks.Length;i++)
            {
                if (!_tasks[i].Equals(item))
                {
                    newArray[j] = _tasks[i];
                    j++;
                }
            }
            _tasks = newArray;
        }
        public void Clear()
        {
            _tasks = Array.Empty<T>();
            Directory.Delete(_manager.FolderPath);
        }
        public void SaveTasks()
        {
            if (_manager == null || _tasks == null || _tasks.Length == 0) return;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] is Lab9.Blue.Task1)
                    _manager.ChangeFileName("Task1");
                if (_tasks[i] is Lab9.Blue.Task2)
                    _manager.ChangeFileName("Task2");
                if (_tasks[i] is Lab9.Blue.Task3)
                    _manager.ChangeFileName("Task3");
                if (_tasks[i] is Lab9.Blue.Task4)
                    _manager.ChangeFileName("Task4");

                _manager.Serialize(_tasks[i]);
            }
        }
        public void LoadTasks()
        {
            for (int i = 0; i < _tasks.Length; i++)
            {
                _tasks[i] = _manager.Deserialize();
            }
        }
        public void ChangeManager(BlueFileManager<T> newManager)
        {
            var folderPath = Path.Combine(_manager.FolderPath ?? Directory.GetCurrentDirectory(), _manager.Name);
            Directory.CreateDirectory(folderPath);
            newManager.SelectFolder(folderPath);
            _manager = newManager;

        }
    }
}
