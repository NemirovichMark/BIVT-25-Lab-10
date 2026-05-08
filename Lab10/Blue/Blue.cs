namespace Lab10.Blue;

public class Blue<T> where T:Lab9.Blue.Blue
{
  private BlueFileManager<T> _manager;
  private T[] _tasks;

  public BlueFileManager<T> Manager => _manager;
  public T[] Tasks => _tasks;

  public Blue(T[] tasks = null)
  {
    _manager = null;
    if (tasks == null) _tasks = new T[0];
    else
    {
      _tasks = new T[tasks.Length];
      for (int i = 0; i < tasks.Length; i++)
      {
	_tasks[i] = tasks[i];
      }
    }
  }

  public Blue(BlueFileManager<T> manager, T[] tasks = null)
  {
    _manager = manager;
    if (tasks == null) _tasks = new T[0];
    else
    {
      _tasks = new T[tasks.Length];
      for (int i = 0; i < tasks.Length; i++)
      {
	_tasks[i] = tasks[i];
      }
    }
  }

  public Blue(T[] tasks, BlueFileManager<T> manager)
  {
    _manager = manager;
    if (tasks == null) _tasks = new T[0];
    else
    {
      _tasks = new T[tasks.Length];
      for (int i = 0; i < tasks.Length; i++)
      {
	_tasks[i] = tasks[i];
      }
    }
  }
  public void Add(T obj)
  {
    _tasks = _tasks.Append<T>(obj).ToArray<T>();
  }

  public void Add(T[] arr)
  {
    foreach (T obj in arr)
      _tasks = _tasks.Append<T>(obj).ToArray<T>();
  }

  public void Remove(T obj)
  {
    if (_tasks == null) return;
    if (obj == null) return;
    int index = -1;
    for (int i = 0; i < _tasks.Length; i++)
    {
      if (_tasks[i] == obj)
      {
	index = i;
      }
    }

    if (index == -1) return;
    T[] array = new T[_tasks.Length - 1];
    for (int i = 0; i < array.Length; i++)
    {
      if (i < index)
      {
	array[i] = _tasks[i];
      }
      else
      {
	array[i] = _tasks[i + 1];
      }
    }

    _tasks = array;
  }

  public void Clear()
  {
    if (_tasks == null) return;
    for (int i = 0; i < _tasks.Length; i++)
    {
      Remove(_tasks[i]);
      i--;
    }

    if (!Directory.Exists(_manager.FolderPath)) return;
    Directory.Delete(_manager.FolderPath, true); //true for recursive deletion
  }

  public void SaveTasks()
  {
    if (_tasks == null) return;
    for (int i = 0; i < _tasks.Length; i++)
    {
      _manager.ChangeFileName(_tasks[i].GetType().ToString());
      _manager.Serialize(_tasks[i]);
    }
  }

  public void LoadTasks()
  {
    if (_tasks == null) return;
    for (int i = 1; i <= 4; i++)
    {
      _manager.ChangeFileName($"Task{i}");

      if (File.Exists(_manager.FullPath))
      {
	T obj = _manager.Deserialize();
	this.Add(obj);
      }
    }
  }

  public void ChangeManager(BlueFileManager<T> manager)
  {
    if (manager == null) return;
    _manager = manager;
    _tasks = new T[0];
    if (!Directory.Exists(_manager.FolderPath))
    {
      Directory.CreateDirectory(_manager.Name);
    }
    _manager.SelectFolder(_manager.Name);
  }
}
