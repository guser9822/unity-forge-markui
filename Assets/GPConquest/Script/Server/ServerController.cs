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

    public class ServerController : MultiplayerBaseController
    {
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

        public void StartServer(ServerOptions _serverOptions)
        {
            InitMultiplayerBase(_serverOptions);
            Host();
        }

        public void EndServer()
        {
            mgr.Disconnect();
            Destroy(mgr.gameObject);
        }

    }

}


