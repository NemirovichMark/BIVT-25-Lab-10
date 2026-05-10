using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
  public abstract class BlueFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Blue.Blue
  {
    
    public BlueFileManager(string name) : base(name) { }
    public BlueFileManager(string name, string folder, string file, string extension = "txt") : base(name, folder, file, extension) { }

    public override void EditFile(string countent)
    {
      if (File.Exists(FullPath))
        base.EditFile(countent);
    }
    public override void ChangeFileExtension(string format)
    {
      if (File.Exists(FullPath))
        base.ChangeFileExtension(format);
    }

    public abstract void Serialize(T obj);
    public abstract T Deserialize();
  }
}
