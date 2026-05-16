using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public class Green
    {
        private Lab9.Green.Green[] _tasks;
        private GreenFileManager _manager;

        public Lab9.Green.Green[] Tasks => _tasks.ToArray();
        public GreenFileManager Manager => _manager;


        public Green(Lab9.Green.Green[] tasks = null)
        {
            if (tasks == null) _tasks = Array.Empty<Lab9.Green.Green>();
            else _tasks = tasks.ToArray();
            _manager = null;
        }
        public Green(GreenFileManager manager, Lab9.Green.Green[] tasks = null)
        {
            _manager = manager;
            if (tasks == null) _tasks = Array.Empty<Lab9.Green.Green>();
            else _tasks = tasks.ToArray();
        }
        public Green(Lab9.Green.Green[] tasks, GreenFileManager manager)
        {
            _manager = manager;
            if (tasks == null) _tasks = Array.Empty<Lab9.Green.Green>();
            else _tasks = tasks.ToArray();
        }

        public void Add(Lab9.Green.Green obj)
        {
            if (obj == null) return;
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = obj;
        }
        public void Add(Lab9.Green.Green[] objs)
        {
            for (int i  = 0; i < objs.Length; i++)
            {
                Add(objs[i]);
            }
        }
        public void Remove(Lab9.Green.Green obj)
        {
            if (obj == null) return;
            int index = -1;
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == obj) index = i;
            }

            int j = 0;
            Lab9.Green.Green[] copy = new Lab9.Green.Green[_tasks.Length - 1];
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (i == index) i++;
                copy[j] = _tasks[i];
                j++;
            }
            _tasks = copy;
        }
        public void Clear()
        {
            _tasks = Array.Empty<Lab9.Green.Green>();
            Directory.Delete(_manager.FolderPath);
        }
        public void SaveTasks()
        {
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == null)
                    continue;
                _manager.ChangeFileName($"task{i}");
                _manager.Serialize(_tasks[i]);
            }

        }
        public void LoadTasks()
        {
            Lab9.Green.Green[] tasks = new Lab9.Green.Green[_tasks.Length];
            for (int i = 0; i < tasks.Length; i++)
            {
                _manager.ChangeFileName($"task{i}");
                Lab9.Green.Green loaded = _manager.Deserialize<Lab9.Green.Green>();
                tasks[i] = loaded;
            }

            _tasks = tasks;
        }
        public void ChangeManager(GreenFileManager newManager)
        {
            if (newManager == null) return;
            _manager = newManager;
            string newFolderPath = Path.Combine(newManager.FolderPath, newManager.Name);
            if(!Directory.Exists(newFolderPath))
                Directory.CreateDirectory(newFolderPath);
            newManager.SelectFolder(newFolderPath);
            _manager = newManager;
        }
    }
}
