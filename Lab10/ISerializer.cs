using Lab9.Green;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface ISerializer
    {
        public abstract T Deserialize<T>()
    where T : Lab9.Green.Green;

        public abstract void Serialize<T>(T obj)
            where T : Lab9.Green.Green;
    }
}