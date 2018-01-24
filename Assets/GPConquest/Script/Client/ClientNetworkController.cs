using BeardedManStudios.Forge.Networking;
using TC.GPConquest.MarkLight4GPConquest;
using UnityEngine;

namespace TC.GPConquest
{
    public class ClientNetworkController : CustomNetworkController
    {
        protected ServerOptions ClientServerOptions;

        private void Awake()
        {
            ServerOptions ClientServerOptions = gameObject.AddComponent<ServerOptions>(); 
            //ClientServerOptions.IpAddress.Value = "127.0.0.1";
            //ClientServerOptions.ServerPort.Value = "15937";
            //ClientServerOptions.XProtocol.Value = UIInfoLayer.UDPString;
        }

        public override void StartCustomNetworkController(ServerOptions _serverOptions)
        {
            base.StartCustomNetworkController(_serverOptions);
            Connect();
        }

        public void Connect()
        {
            ushort port;
            if (!ushort.TryParse(MultiplayerBasePort, out port))
            {
                Debug.LogError("The supplied port number is not within the allowed range 0-" + ushort.MaxValue);
                return;
            }

            NetWorker client;

            if (useTCP)
            {
                client = new TCPClient();
                ((TCPClient)client).Connect(MultiplayerBaseIp, (ushort)port);
            }
            else
            {
                client = new UDPClient();
                ((UDPClient)client).Connect(MultiplayerBaseIp, (ushort)port);
            }

            Connected(client);
        }

    }
}