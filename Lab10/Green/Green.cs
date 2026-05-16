using System.IO;

namespace Lab10.Green
{
    public class Green
    {
        private GreenFileManager _manager;
        private Lab9.Green.Green[] _tasks;

        public GreenFileManager Manager => _manager;
        public Lab9.Green.Green[] Tasks => CopyTasks();

        public Green()
        {
            _tasks = new Lab9.Green.Green[0];
        }

        public Green(Lab9.Green.Green[] tasks)
        {
            _tasks = CopyTasks(tasks);
        }

        public Green(GreenFileManager manager, Lab9.Green.Green[] tasks = null)
        {
            _manager = manager;
            _tasks = CopyTasks(tasks);
        }

        public Green(Lab9.Green.Green[] tasks, GreenFileManager manager)
        {
            _manager = manager;
            _tasks = CopyTasks(tasks);
        }

        public void Add(Lab9.Green.Green task)
        {
            if (task == null)
                return;

            System.Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = task;
        }

        public void Add(Lab9.Green.Green[] tasks)
        {
            if (tasks == null)
                return;

            foreach (var task in tasks)
                Add(task);
        }

        public void Remove(Lab9.Green.Green task)
        {
            if (task == null || _tasks.Length == 0)
                return;

            int index = -1;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == task)
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
                return;

            var result = new Lab9.Green.Green[_tasks.Length - 1];
            int offset = 0;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (i == index)
                {
                    offset = 1;
                    continue;
                }

                result[i - offset] = _tasks[i];
            }

            _tasks = result;
        }

        public void Clear()
        {
            _tasks = new Lab9.Green.Green[0];
            if (_manager != null && Directory.Exists(_manager.FolderPath))
                Directory.Delete(_manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            if (_manager == null)
                return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null)
                return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                _tasks[i] = _manager.Deserialize<Lab9.Green.Green>();
            }
        }

        public void ChangeManager(GreenFileManager manager)
        {
            if (manager == null)
                return;

            string folder = manager.Name;
            if (!string.IsNullOrEmpty(folder))
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                manager.SelectFolder(folder);
            }

            _manager = manager;
        }

        private Lab9.Green.Green[] CopyTasks()
        {
            return CopyTasks(_tasks);
        }

        private Lab9.Green.Green[] CopyTasks(Lab9.Green.Green[] tasks)
        {
            if (tasks == null)
                return new Lab9.Green.Green[0];

            var copy = new Lab9.Green.Green[tasks.Length];
            for (int i = 0; i < tasks.Length; i++)
                copy[i] = tasks[i];

            return copy;
        }
    }
}
