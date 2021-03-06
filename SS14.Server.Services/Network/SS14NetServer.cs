﻿using Lidgren.Network;
using SS14.Server.Interfaces.Configuration;
using SS14.Server.Interfaces.Network;
using SS14.Shared.IoC;
using System.Collections.Generic;

namespace SS14.Server.Services.Network
{
    public class SS14NetServer : NetServer, ISS14NetServer
    {
        public SS14NetServer()
            : base(LoadNetPeerConfig())
        {
        }

        #region ISS13NetServer Members

        public void SendToAll(NetOutgoingMessage message)
        {
            SendToAll(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendMessage(NetOutgoingMessage message, NetConnection client)
        {
            SendMessage(message, client, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendToMany(NetOutgoingMessage message, List<NetConnection> recipients)
        {
            SendMessage(message, recipients, NetDeliveryMethod.ReliableOrdered, 0);
        }

        #endregion

        public static NetPeerConfiguration LoadNetPeerConfig()
        {
            var _config = new NetPeerConfiguration("SS13_NetTag");
            _config.Port = IoCManager.Resolve<IConfigurationManager>().Port;
            return _config;
        }
    }
}