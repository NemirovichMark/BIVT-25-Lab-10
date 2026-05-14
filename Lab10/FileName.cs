using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface ISerializer
    {
        public T Deserialize <T>() where T: Lab9.Green.Green;
        public void Serialize<T>(T a) where T: Lab9.Green.Green;
    }
}
