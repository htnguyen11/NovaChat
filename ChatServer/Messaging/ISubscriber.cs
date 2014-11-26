﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Messaging
{
    public interface ISubscriber
    {

        void Subscribe(ISubscription subscription);
    }
}
