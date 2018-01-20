using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Lobby;
using BeardedManStudios.SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TC.GPConquest.MarkLight4GPConquest;

public class ServerController : MonoBehaviour {

    public ServerOptions ServerOptions;
    public string ServerIP;
    public string ServerPort;
    public string NetProtocol;
    public bool useTCP;
    public bool useMainThreadManagerForRPCs = true; 
    public bool getLocalNetworkConnections = false;
    public string natServerHost = string.Empty;
    public ushort natServerPort = 15941;
    private NetworkManager mgr;
    public GameObject networkManager;
    public bool DontChangeSceneOnConnect = false;
    public string masterServerHost = string.Empty;
    public ushort masterServerPort = 15940;
    public bool useElo = false;
    public int myElo = 0;
    public int eloRequired = 0;
    public bool useInlineChat = false;


    protected void InitServerOptions(ServerOptions _serverOptions)
    {
        if (_serverOptions != null)
        {
            ServerOptions = _serverOptions;
            ServerIP = ServerOptions.IpAddress.Value;
            ServerPort = ServerOptions.ServerPort.Value;
            NetProtocol = (string)ServerOptions.XProtocol.Value;
            useTCP = NetProtocol.Equals(UIInfoLayer.TCPString) ? true : false;
        }else throw new System.ArgumentNullException(UIInfoLayer.ServerOptsNullMessage);
    }

    protected void InitServer(ServerOptions _serverOptions)
    {
        InitServerOptions(_serverOptions);
        if (!useTCP)
        {
            // Do any firewall opening requests on the operating system
            NetWorker.PingForFirewall(ushort.Parse(ServerPort));
        }

        if (useMainThreadManagerForRPCs)
            Rpc.MainThreadRunner = MainThreadManager.Instance;

        if (getLocalNetworkConnections)
        {
            NetWorker.localServerLocated += LocalServerLocated;
            NetWorker.RefreshLocalUdpListings(ushort.Parse(ServerPort));
        }
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

            if (natServerHost.Trim().Length == 0)
                ((UDPServer)server).Connect(ServerIP, ushort.Parse(ServerPort));
            else
                ((UDPServer)server).Connect(natHost: natServerHost, natPort: natServerPort);
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

        if ( (mgr == null ) && (networkManager.GetComponent<NetworkManager>() == null))
        {
            Debug.LogWarning("A network manager was not provided, generating a new one instead");
            networkManager = new GameObject("Network Manager");
            mgr = networkManager.AddComponent<NetworkManager>();
        }
        else mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

        // If we are using the master server we need to get the registration data
        JSONNode masterServerData = null;
        if (!string.IsNullOrEmpty(masterServerHost))
        {
            string serverId = "myGame";
            string serverName = "Forge Game";
            string type = "Deathmatch";
            string mode = "Teams";
            string comment = "Demo comment...";

            masterServerData = mgr.MasterServerRegisterData(networker, serverId, serverName, type, mode, comment, useElo, eloRequired);
        }

        mgr.Initialize(networker, masterServerHost, masterServerPort, masterServerData);

        if (useInlineChat && networker.IsServer)
            SceneManager.sceneLoaded += CreateInlineChat;

        if (networker is IServer)
        {
            if (!DontChangeSceneOnConnect)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log("Server seems started correctly");
            }
            else
                NetworkObject.Flush(networker); //Called because we are already in the correct scene!
        }
    }

    private void CreateInlineChat(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= CreateInlineChat;
        var chat = NetworkManager.Instance.InstantiateChatManager();
        DontDestroyOnLoad(chat.gameObject);
    }

    public void StartServer(ServerOptions _serverOptions)
    {
        InitServer(_serverOptions);
        Host();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void EndServer()
    {
        mgr.Disconnect();
        Destroy(mgr.gameObject);
    }

    private void OnApplicationQuit()
    {
        if (getLocalNetworkConnections)
            NetWorker.EndSession();
    }
}
