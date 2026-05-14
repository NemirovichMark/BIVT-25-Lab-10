using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private PurpleFileManager<T> _manageer;
        private T[] _task;

        public PurpleFileManager<T> Manager => _manageer;
        public T[] Tasks => _task.ToArray();
        public Purple(T[] tasks = null)
        {
            if (tasks != null)
            {
                _task = new T[tasks.Length];
                for (int i = 0; i < tasks.Length; i++)
                {
                    _task[i] = tasks[i];
                }
            }
            else
            {
                _task = new T[0];
            }
            _manageer = null;

        }
        public Purple(PurpleFileManager<T> manager, T[] tasks = null)
        {
            if (tasks != null)
            {
                _task = new T[tasks.Length];
                for (int i = 0; i < tasks.Length; i++)
                {
                    _task[i] = tasks[i];
                }
            }
            else
            {
                _task = new T[0];
            }
            _manageer = manager;
        }
        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            if (tasks != null)
            {
                _task = new T[tasks.Length];
                for (int i = 0; i < tasks.Length; i++)
                {
                    _task[i] = tasks[i];
                }
            }
            else
            {
                _task = new T[0];
            }
            _manageer = manager;
        }
        public void Add(T item)
        {
            if (_task == null) return;
            if (item == null) return;
            Array.Resize(ref _task, _task.Length + 1);
            _task[_task.Length - 1] = item;
        }
        public void Add(T[] tasks)
        {
            if (_task == null) return;
            if (tasks == null || tasks.Length == 0) return;
            for (int i = 0; i < tasks.Length; i++)
            {
                Add(tasks[i]);
            }
        }
        public void SaveTasks()
        {
            if (_manageer == null) return;
            if (_task == null) return;
            for (int i = 0; i < _task.Length; i++)
            {
                _manageer.ChangeFileName(i.ToString());
                _manageer.Serialize(_task[i]);
            }
        }
        public void LoadTasks()
        {
            if (_manageer == null) return;
            if (_task == null) return;
            for (int i = 0; i < _task.Length; i++)
            {
                _manageer.ChangeFileName(i.ToString());
                _task[i] = _manageer.Deserialize();
            }
        }
        public void Remove(T item)
        {
            if (_task == null) return;
            if (item == null) return;

            T[] tasks = new T[_task.Length - 1];
            int ii = -1;
            for (int i = 0; i < _task.Length; i++)
            {
                if (_task[i] == item)
                {
                    ii = i;
                }
            }
            if (ii == -1) return;
            for (int i = 0; i < ii; i++)
            {
                tasks[i] = _task[i];
            }
            int c = ii;
            for (int i = ii + 1; i < _task.Length; i++)
            {
                tasks[c++] = _task[i];
            }
            _task = tasks;

        }
        public void Clear()
        {
            if (_task == null) return;
            if (_manageer == null || Manager.FolderPath == null) return;
            _task = new T[0];
            if (!Directory.Exists(Manager.FolderPath)) return;
            Directory.Delete(Manager.FolderPath, true); //true удаляет все папки и файла даже если в них чтото есть 
        }

        // заменяем менеджера создаем с новым name папку 
        public void ChangeManager(PurpleFileManager<T> manager)
        {
            if (_manageer == null || manager == null) return;
            if (_task == null) return;
            _manageer = manager; // заменили менеджера 
            if (!Directory.Exists(_manageer.Name)) // если не сущ папки с name создаем ее 
            {
                Directory.CreateDirectory(_manageer.Name);
            }
            _manageer.SelectFolder(Manager.Name); // делаем название папке
        }


    }
}

