using System;
using System.IO;

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
            _manager = null;
            _tasks = new T[0];
        }
        public Purple(T[] tasks)
        {
            _manager = null;
            _tasks = CopyTasks(tasks);
        }

        public Purple(PurpleFileManager<T> manager, T[] tasks = null)
        {
            _manager = manager;
            _tasks = CopyTasks(tasks);
        }

        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            _manager = manager;
            _tasks = CopyTasks(tasks);
        }

        public void Add(T task)
        {
            if (task == null)
            {
                return;
            }

            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = task;
        }

        public void Add(T[] tasks)
        {
            if (tasks == null || tasks.Length == 0)
            {
                return;
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                Add(tasks[i]);
            }
        }

        public void Remove(T task)
        {
            if (task == null || _tasks == null || _tasks.Length == 0)
            {
                return;
            }

            int index = Array.IndexOf(_tasks, task);

            if (index >= 0)
            {
                for (int i = index; i < _tasks.Length - 1; i++)
                {
                    _tasks[i] = _tasks[i + 1];
                }

                Array.Resize(ref _tasks, _tasks.Length - 1);
            }
        }

        public void Clear()
        {
            _tasks = new T[0];

            if (_manager == null)
            {
                return;
            }

            if (Directory.Exists(_manager.FolderPath))
            {
                Directory.Delete(_manager.FolderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (_manager == null)
            {
                return;
            }

            if (_tasks == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName("task" + i);
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null)
            {
                return;
            }

            if (!Directory.Exists(_manager.FolderPath))
            {
                _tasks = new T[0];
                return;
            }

            string[] files = Directory.GetFiles(_manager.FolderPath, "*." + _manager.FileExtension);
            Array.Sort(files);

            T[] result = new T[0];

            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(files[i]);

                _manager.ChangeFileName(fileName);

                T task = _manager.Deserialize();

                if (task != null)
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = task;
                }
            }

            _tasks = result;
        }

        public void ChangeManager(PurpleFileManager<T> newManager)
        {
            if (newManager == null)
            {
                return;
            }

            newManager.SelectFolder(newManager.Name);
            _manager = newManager;
        }

        private T[] CopyTasks(T[] tasks)
        {
            if (tasks == null || tasks.Length == 0)
            {
                return new T[0];
            }

            T[] result = new T[tasks.Length];

            for (int i = 0; i < tasks.Length; i++)
            {
                result[i] = tasks[i];
            }

            return result;
        }
    }
}