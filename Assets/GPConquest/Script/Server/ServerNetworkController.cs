using BeardedManStudios.Forge.Networking;
using UnityEngine;
using TC.GPConquest.MarkLight4GPConquest;

namespace TC.GPConquest
{

    public class ServerNetworkController : CustomNetworkController
    {
        public override void StartCustomNetworkController(ServerOptions _serverOptions)
        {
            base.StartCustomNetworkController(_serverOptions);
            Host();
        }

        protected void Host()
        {
            NetWorker server;

            if (useTCP)
            {
                server = new TCPServer(64);
                ((TCPServer)server).Connect();
            }
            else
            {
                server = new UDPServer(64);
                ((UDPServer)server).Connect(MultiplayerBaseIp, ushort.Parse(MultiplayerBasePort));
            }

            server.playerTimeout += (player, sender) =>
            {
                Debug.Log("Player " + player.NetworkId + " timed out");
            };

            Connected(server);
        }

        protected override void Connected(NetWorker networker)
        {
            base.Connected(networker);
            NetworkObject.Flush(networker);
        }

    }

}


