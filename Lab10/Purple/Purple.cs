using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private List<T> _tasks;
        public PurpleFileManager<T> Manager { get; private set; }
        public T[] Tasks => _tasks.ToArray();

        public Purple(T[] tasks = null)
        {
            if (tasks == null)
                _tasks = new List<T> { };
            else
                _tasks = tasks.ToList();
        }
        public Purple(PurpleFileManager<T> manager, T[] tasks = null)
        {
            Manager = manager;
            if (tasks == null)
                _tasks = new List<T> { };
            else
                _tasks = tasks.ToList();
        }
        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            Manager = manager;
            if (tasks == null)
                _tasks = new List<T> { };
            else
                _tasks = tasks.ToList();
        }
        public void Add(T item)
        {
            _tasks.Add(item);
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
            _tasks.Remove(item);
        }
        public void Clear()
        {
            _tasks.Clear();
            Directory.Delete(Manager.FolderPath, true);
        }
        public void ChangeManager(PurpleFileManager<T> manager)
        {
            string newFolderPath = Path.Combine(Directory.GetParent(Manager.FolderPath).FullName, manager.Name);
            Manager = manager;
            Manager.SelectFolder(newFolderPath);
        }
        public void SaveTasks()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                Manager.ChangeFileName("task"+i.ToString());
                Manager.Serialize(_tasks[i]);
            }
        }
        public void LoadTasks()
        {
            for (int i =0; i < _tasks.Count;i++)                   //ОШИБКООПАСНОЕ МЕСТО
            {
                Manager.ChangeFileName("task" + i.ToString());
                _tasks[i] = Manager.Deserialize();
            }
        }
    }
}
