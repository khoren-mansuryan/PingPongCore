using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public interface IPinger
    {
        void StartPing();
        void StopPing();
    }
    
}
