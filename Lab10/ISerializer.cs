using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface ISerializer<T> where T : Lab9.Purple.Purple
    {
        T Deserialize();
        void Serialize(T obj);
    }
}
