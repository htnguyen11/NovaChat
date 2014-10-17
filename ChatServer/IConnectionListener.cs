using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public interface IConnectionListener
    {
        EventHandler<IncomingConnectionEventArgs> IncomingConnectionHandler { get; set; }


        void Start();

        void Close();

    }
}
