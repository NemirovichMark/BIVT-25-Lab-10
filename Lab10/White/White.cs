namespace Lab10.White
{
    public class White
    {
        private Lab9.White.White[] _tasks;//для хранения и управления набором объектов типа
                                          // Lab9.White.White
        private WhiteFileManager? _manager;// может быть null

        public WhiteFileManager? Manager => _manager;
        public Lab9.White.White[] Tasks => _tasks;

        public White(Lab9.White.White[]? tasks = null)//конструктор
        {
            _tasks = CopyOrEmpty(tasks);//если не null то копирует массив если null то создает пустой массив 
            _manager = null;//не назначен
        }

        public White(WhiteFileManager manager, Lab9.White.White[]? tasks = null)//необязательный массив задач
        {
            _manager = manager;
            _tasks = CopyOrEmpty(tasks);
        }

        public White(Lab9.White.White[]? tasks, WhiteFileManager manager)//сначала задачи потом менеджер другой порядок параметров
        {
            _tasks = CopyOrEmpty(tasks);
            _manager = manager;
        }

        public void Add(Lab9.White.White task)//1 объект
        {
            if (task == null)
            {
                return;
            }

            var arr = new Lab9.White.White[_tasks.Length + 1];
            for (int i = 0; i < _tasks.Length; i++)
            {
                arr[i] = _tasks[i];
            }
            arr[_tasks.Length] = task;//увеличение массива на 1 и добаление 1 элм
            _tasks = arr;
        }

        public void Add(Lab9.White.White[] tasks)
        {
            if (tasks == null)
            {
                return;
            }

            foreach (var t in tasks)
            {
                Add(t);//для каждого элм исползуем прошлый метод
            }
        }

        public void Remove(Lab9.White.White task)
        {
            if (task == null || _tasks.Length == 0)//если массив пустой или нул 
            {
                return;
            }

            int index = -1;//индекс удаляемого пока не найден поэтому -1
            for (int i = 0; i < _tasks.Length; i++)
            {
                if (ReferenceEquals(_tasks[i], task))//сравнивает ссылки, если тоже самое то присваиваем индекс и выходим
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)//если не найден то выходим
            {
                return;
            }

            var array = new Lab9.White.White[_tasks.Length - 1];//новый масив для удаляемого 
            for (int i = 0, j = 0; i < _tasks.Length; i++)
            {
                if (i == index)//если нашли то не записываем его и продолжаем цикл дальше
                {
                    continue;
                }
                array[j++] = _tasks[i];
            }
            _tasks = array;//новый массив 
        }

        public void Clear()
        {
            _tasks = new Lab9.White.White[0];//очищает коллекцию задач

            if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath) && Directory.Exists(_manager.FolderPath))//менеджер существует, путь к папке не пустой, папка существует 
            {
                Directory.Delete(_manager.FolderPath, true);//если все ок то удаляем (1 пар что удаляем (папку), 2 пар true те удаляем все вложенные файлы)
            }
        }

        public void SaveTasks()
        {
            if (_manager == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)//проходимся по каждой задаче
            {
                _manager.ChangeFileName("task" + i);//имя меняем 
                _manager.Serialize(_tasks[i]);//сохраняем задачу в файл (сериализуем)
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
                var loaded = _manager.Deserialize();//элемент из файла обратно в объект
                _tasks[i] = loaded!;//!уверена не null, сохр в массив
            }
        }

        public void ChangeManager(WhiteFileManager manager)//новый менеджер
        {
            if (manager == null)
            {
                return;
            }

            _manager = manager;

            var folder = Path.Combine(Directory.GetCurrentDirectory(), manager.Name ?? string.Empty);//склеивает нужный путь (сам добавляет слеши)
            //1 пар получает путь где запущена прога, 2 пар имя менеджера ( если null то пустая строка "")
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);//если папки нет то создаем 
            }

            _manager.SelectFolder(folder);//передаем папку менеджеру (теперь все файлы будут там)
        }

        private static Lab9.White.White[] CopyOrEmpty(Lab9.White.White[]? src)//внутри класса и принадлежит ему а не объекту; исходный массв
        {// src парам метода с которого копируем данные 
            if (src == null)
            {
                return new Lab9.White.White[0];//если null то пустой масив
            }

            var copy = new Lab9.White.White[src.Length];//новый массив того же размера 
            for (int i = 0; i < src.Length; i++)
            {
                copy[i] = src[i];//копируем каждый элм по одному
            }
            return copy;
        }
    }
}
