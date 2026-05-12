using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private PurpleFileManager<T> _manager;
        private T[] _tasks;
        
        public PurpleFileManager<T> Manager => _manager;
        public T[] Tasks => _tasks;

        public Purple()
        {
            _tasks = new T[0];
        }
        public Purple(T[] tasks)
        {
            if (tasks == null)
                _tasks = new T[0];
            else
                _tasks = tasks;
        }
        public Purple(PurpleFileManager<T> manager, T[] tasks = null)
        {
            _manager = manager;
            if (tasks == null)
                _tasks = new T[0];
            else
                _tasks = tasks;
        }

        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            _tasks = tasks;
            _manager = manager;
        }

        public void Add(T item) { 
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[^1] = item;
        }

        public void Add(T[] items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public void Remove(T item) 
        {
            int index = Array.IndexOf(Tasks, item);
            if (index < 0) return;

            T[] array = new T[Tasks.Length - 1];
            int newIndex = 0;
            for (int i = 0; i < Tasks.Length; i++)
            {
                if (i != index)
                    array[newIndex++] = Tasks[i];
            }
            _tasks = array;
        }

        public void Clear()
        {
            _tasks = new T[0];
            Directory.Delete(_manager.FolderPath);

        }

        public void SaveTasks()
        {
            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                _manager.Serialize(_tasks[i]);
            }

        }

        public void LoadTasks()
        {
            for (int i = 0; i < _tasks.Length; i++)
            {
                string nameFile = $"task{i}";
                _manager.ChangeFileName(nameFile);
                _tasks[i] = _manager.Deserialize();
            }
        }

        public void ChangeManager(PurpleFileManager<T> newManager)
        {
            string folderPathPreviusManager = _manager.FolderPath;
            _manager = newManager;
            var newPath = Path.Combine(folderPathPreviusManager, _manager.Name);
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);
            _manager.SelectFolder(newPath);

        }

    }   

}
