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
  void Add(T obj)
  {
    _tasks = _tasks.Append<T>(obj).ToArray<T>();
  }

  void Add(T[] arr)
  {
    foreach (T obj in arr)
      _tasks = _tasks.Append<T>(obj).ToArray<T>();
  }



}
