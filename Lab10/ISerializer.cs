using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab10.Purple
{
    public interface ISerializer<T> where T : Lab9.Purple.Purple
    {
        T Deserialize();
        void Serialize(T obj);
    }
}