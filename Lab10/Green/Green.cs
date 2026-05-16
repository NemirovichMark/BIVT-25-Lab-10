using Lab9.Green;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Green
{
    public class Green
    {
        private GreenFileManager _manager;
        private Lab9.Green.Green[] _Tasks;

        public GreenFileManager Manager => _manager;
        public Lab9.Green.Green[] Tasks => _Tasks;

        public Green(Lab9.Green.Green[] tasks = null)
        {
            if (tasks == null)
            {
                _Tasks = new Lab9.Green.Green[0];
            }
            else
            {
                _Tasks = Tasks.ToArray();
            }
        }
        public Green(Lab10.Green.GreenFileManager manager, Lab9.Green.Green[] tasks = null)
        {
            _manager = manager;
            if (tasks == null)
            {
                _Tasks = new Lab9.Green.Green[0];
            }
            else
            {
                _Tasks = tasks.ToArray();
            }
        }

        public void Add(Lab9.Green.Green obiect)
        {
            Array.Resize(ref _Tasks, _Tasks.Length + 1);
            _Tasks[^1] = obiect;
        }

        public void Add(Lab9.Green.Green[] obiects)
        {
            Lab9.Green.Green[] result = new Lab9.Green.Green[_Tasks.Length + obiects.Length];
            Array.Copy(_Tasks, 0, result, 0, _Tasks.Length);
            Array.Copy(obiects, 0, result, _Tasks.Length, obiects.Length);
            _Tasks = result;
        }

        public void Remove(Lab9.Green.Green obiect)
        {
            int index = Array.IndexOf(_Tasks, obiect);

            for (int i = index; i < _Tasks.Length - 1; i++)
            {
                _Tasks[i] = _Tasks[i + 1];
            }
            Array.Resize(ref _Tasks, _Tasks.Length - 1);
        }

        public void Clear()
        {
            _Tasks = new Lab9.Green.Green[0];
            Directory.Delete(_manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            int index = 0;
            foreach (var task in Tasks)
            {
                string name = "task_" + index;
                Manager.ChangeFileName(name);
                Manager.Serialize(task);
                index++;
            }
        }

        public void LoadTasks()
        {
            for (int i = 0; i < _Tasks.Length; i++)
            {
                _manager.ChangeFileName("Task_" + i);
                _Tasks[i] = _manager.Deserialize<Lab9.Green.Green>(); // Читаем файл и обновляем ячейку
            }
        }

        public void ChangeManager(GreenFileManager manager)
        {
            _manager = manager;
            Directory.CreateDirectory(_manager.Name);
            // Выбираем эту папку как рабочую
            _manager.SelectFolder(_manager.Name);
        }
    }
}
