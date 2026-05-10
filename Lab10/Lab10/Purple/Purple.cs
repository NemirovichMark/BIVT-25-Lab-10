using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10.Purple;
using System.IO;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private PurpleFileManager<T> _manager; private T[] _tasks;
        public PurpleFileManager<T> Manager => _manager;
        public T[] Tasks => (T[])_tasks.Clone();
        public Purple(T[] tasks=null)
        {
            _tasks = tasks == null ? new T[0] : (T[])tasks.Clone();
            _manager = null;
        }
        public Purple(PurpleFileManager<T> manager, T[] tasks=null)
        {
            _manager = manager;
            _tasks = tasks == null ? new T[0] : (T[])tasks.Clone();
        }
        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            _manager = manager;
            _tasks = tasks == null ? new T[0] : (T[])tasks.Clone();
        }
        public void Add(T task)
        {
            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[^1]=task;
        }
        public void Add(T[] task)
        {
            if (task == null) return;
            Array.Resize(ref _tasks, _tasks.Length + task.Length);
            foreach (var i in task)
                Add(i);
        }
        public void Remove(T task)
        {
            if (task == null) return;
            List<T> list = _tasks.ToList();
            list.Remove(task);
            _tasks = list.ToArray();
        }
        public void Clear()
        {
            _tasks = Array.Empty<T>(); // => пустой массив
            if (_manager==null) return;
            Directory.Delete(_manager.FolderPath, true );
            // => удаляем папку и всё внутри; _manager.FolderPath —  путь к папке на диске,
        }
        public void SaveTasks()
        { 
            if (_manager==null) return;
            for (int i=0; i< _tasks.Length;i++)
            {
                _manager.ChangeFileName($"task_{i}"); // имя файла для задачи
                _manager.Serialize(_tasks[i]);  // сериализует задачу в файл
            }
        }
        public void LoadTasks()
        { 
            if (_manager==null) return;
            List<T> l = new List<T>();
            while (true)
            {
                T task=_manager.Deserialize();  // Загружаем задачу из файла
                l.Add(task);         // Добавляем в список
            }
            _tasks = l.ToArray();  // список в массив, сохраняем
        }
        public void ChangeManager(PurpleFileManager<T> m)
        {
            if (m == null) return;
            _manager = m;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), _manager.FolderPath ?? _manager.Name);
            _manager.SelectFolder(folderPath);
        }
    }
}
