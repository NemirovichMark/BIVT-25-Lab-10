using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public interface IWhiteSerializer
    {
        White Deserialize();
        void Serialize(White obj);
    }
}
