using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public interface ISerializer<T> where T: Lab9.Purple.Purple
    {
        void Serialize(Lab9.Purple.Purple obj);
    }
}
