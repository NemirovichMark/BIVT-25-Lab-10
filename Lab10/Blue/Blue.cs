using System.IO;

namespace Lab10.Blue
{
    public class Blue<T> where T : Lab9.Blue.Blue
    {
        private BlueFileManager<T> _manager;

        private T[] _tasks;

        public BlueFileManager<T> Manager => _manager;

        public T[] Tasks
        {
            get
            {
                return (T[])_tasks.Clone();
            }
        }

        public Blue(T[] tasks)
        {
            _manager = null;

            if (tasks == null)
            {
                _tasks = new T[0];
            }
            else
            {
                _tasks = (T[])tasks.Clone();
            }
        }

        public Blue(BlueFileManager<T> manager, T[] tasks = null)
        {
            _manager = manager;

            if (tasks == null)
            {
                _tasks = new T[0];
            }
            else
            {
                _tasks = (T[])tasks.Clone();
            }
        }

        public Blue(T[] tasks, BlueFileManager<T> manager)
        {
            _manager = manager;

            if (tasks == null)
            {
                _tasks = new T[0];
            }
            else
            {
                _tasks = (T[])tasks.Clone();
            }
        }

        public void Add(T task)
        {
            if (task == null)
            {
                return;
            }

            T[] newTasks = new T[_tasks.Length + 1];

            for (int i = 0; i < _tasks.Length; i++)
            {
                newTasks[i] = _tasks[i];
            }

            newTasks[newTasks.Length - 1] = task;

            _tasks = newTasks;
        }

        public void Add(T[] tasks)
        {
            if (tasks == null)
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
            if (task == null)
            {
                return;
            }

            int index = -1;

            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == task)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return;
            }

            T[] newTasks = new T[_tasks.Length - 1];

            int newIndex = 0;

            for (int i = 0; i < _tasks.Length; i++)
            {
                if (i != index)
                {
                    newTasks[newIndex] = _tasks[i];
                    newIndex++;
                }
            }

            _tasks = newTasks;
        }

        public void Clear()
        {
            _tasks = new T[0];

            if (_manager != null &&
                _manager.FolderPath != null &&
                Directory.Exists(_manager.FolderPath))
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

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName("task" + i);

                _tasks[i] = _manager.Deserialize();
            }
        }

        public void ChangeManager(BlueFileManager<T> manager)
        {
            if (manager == null)
            {
                return;
            }

            _manager = manager;

            if (_manager.Name != null &&
                _manager.Name != string.Empty)
            {
                if (!Directory.Exists(_manager.Name))
                {
                    Directory.CreateDirectory(_manager.Name);
                }

                _manager.SelectFolder(_manager.Name);
            }
        }
    }
}
