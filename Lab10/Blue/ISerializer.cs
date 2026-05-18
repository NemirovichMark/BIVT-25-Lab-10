using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab9.Blue;

namespace Lab10
{
    public interface ISerializer<T> where T : Lab9.Blue.Blue
    {
        void Serialize(T obj);
        T Deserialize();
    }
}
