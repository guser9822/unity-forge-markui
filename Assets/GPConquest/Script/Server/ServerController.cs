using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Lobby;
using BeardedManStudios.SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TC.GPConquest.MarkLight4GPConquest;

namespace TC.GPConquest
{

    public class ServerController : MonoBehaviour
    {

        public ServerOptions ServerOptions;
        public string ServerIP;
        public string ServerPort;
        public string NetProtocol;
        public bool useTCP;
        private NetworkManager mgr;
        public GameObject networkManager;

        protected void InitServerOptions(ServerOptions _serverOptions)
        {
            if (_serverOptions != null)
            {
                ServerOptions = _serverOptions;
                ServerIP = ServerOptions.IpAddress.Value;
                ServerPort = ServerOptions.ServerPort.Value;
                NetProtocol = (string)ServerOptions.XProtocol.Value;
                useTCP = NetProtocol.Equals(UIInfoLayer.TCPString) ? true : false;
            }
            else throw new System.ArgumentNullException(UIInfoLayer.ServerOptsNullMessage);
        }

        protected void InitServer(ServerOptions _serverOptions)
        {
            InitServerOptions(_serverOptions);
            if (!useTCP)
            {
                // Do any firewall opening requests on the operating system
                NetWorker.PingForFirewall(ushort.Parse(ServerPort));
            }

            Rpc.MainThreadRunner = MainThreadManager.Instance;
        }

        private void LocalServerLocated(NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
        {
            Debug.Log("Found endpoint: " + endpoint.Address + ":" + endpoint.Port);
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
                ((UDPServer)server).Connect(ServerIP, ushort.Parse(ServerPort));
            }

            server.playerTimeout += (player, sender) =>
            {
                Debug.Log("Player " + player.NetworkId + " timed out");
            };

            Connected(server);
        }

        protected void Connected(NetWorker networker)
        {
            if (!networker.IsBound)
            {
                Debug.LogError("NetWorker failed to bind");
                return;
            }

            if (mgr == null && networkManager == null)
            {
                Debug.LogWarning("A network manager was not provided, generating a new one instead");
                networkManager = new GameObject("Network Manager");
                mgr = networkManager.AddComponent<NetworkManager>();
            }
            else mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

            mgr.Initialize(networker);

            if (networker is IServer)
                NetworkObject.Flush(networker); //Called because we are already in the correct scene!
            
        }

        public void StartServer(ServerOptions _serverOptions)
        {
            InitServer(_serverOptions);
            Host();
        }

        public void EndServer()
        {
            mgr.Disconnect();
            Destroy(mgr.gameObject);
        }

    }

}


