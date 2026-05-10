using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
  public class Blue<T> where T : Lab9.Blue.Blue
  {
    private BlueFileManager<T> _manager;
    private T[] _tasks;

    public BlueFileManager<T> Manager => _manager;
    public T[] Tasks => _tasks.ToArray();

    public Blue(T[] tasks = null)
    {
      _tasks = tasks != null ? tasks.ToArray() : new T[0];
    }
    public Blue(BlueFileManager<T> fileManager, T[] tasks = null)
    {
      _manager = fileManager;
      _tasks = tasks != null ? tasks.ToArray() : new T[0];
    }
    public Blue(T[] tasks, BlueFileManager<T> fileManager)
    {
      _manager = fileManager;
      _tasks = tasks.ToArray();
    }

    public bool ArrayAndItemType<TItem>(T[] array, TItem item)
    {
      return typeof(T) == typeof(TItem);
    }
    public void Add(T obj)
    {
      if (ArrayAndItemType(_tasks, obj))
      {
        T[] newTasks = new T[_tasks.Length + 1];
        Array.Copy(_tasks, newTasks, _tasks.Length);
        newTasks[_tasks.Length] = obj;
        _tasks = newTasks;
      }
    }
    public void Add(T[] array)
    {
      if (_tasks.GetType() == array.GetType())
      {
        for (int i = 0; i < array.Length; i++)
        {
          Add(array[i]);
        }
      }
    }
    public void Remove(T obj)
    {
      if (_tasks == null) return;
      _tasks = _tasks.Except(new T[] { obj }).ToArray();
    }
    public void Clear()
    {
      _tasks = new T[0];
      if (_manager != null && Directory.Exists(_manager.FolderPath))
      {
        Directory.Delete(_manager.FolderPath, true);
      }
    }
    public void SaveTasks()
    {
      for (int i = 0; i < _tasks.Length; i++)
      {
        _manager.ChangeFileName($"Task{i}");
        _manager.Serialize(_tasks[i]);
      }
    }
    public void LoadTasks()
    {
      if (_manager == null) return;

      var loadedTasks = new List<T>();
      int i = 0;

      while (true)
      {
        _manager.ChangeFileName($"Task{i}");
        string fullPath = _manager.FullPath;

        if (File.Exists(fullPath))
        {
          T task = _manager.Deserialize();
          if (task != null)
          {
            loadedTasks.Add(task);
          }
          i++;
        }
        else
        {
          break;
        }
      }

      _tasks = loadedTasks.ToArray();
    }
    public void ChangeManager(BlueFileManager<T> manager)
    {
      _manager = manager;
      _manager.SelectFolder(_manager.Name);
      if (!Directory.Exists(_manager.FolderPath))
        Directory.CreateDirectory(_manager.FolderPath);
    }
  }
}
