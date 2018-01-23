using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using TC.GPConquest.MarkLight4GPConquest;
using UnityEngine;

public class MultiplayerBaseController : MonoBehaviour {

    public ServerOptions ServerOptions;
    protected string ServerIP;
    protected string ServerPort;
    protected string NetProtocol;
    protected bool useTCP;
    protected NetworkManager mgr;
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

    protected void InitMultiplayerBase(ServerOptions _serverOptions)
    {
        InitServerOptions(_serverOptions);
        if (!useTCP)// Do any firewall opening requests on the operating system
            NetWorker.PingForFirewall(ushort.Parse(ServerPort));

        Rpc.MainThreadRunner = MainThreadManager.Instance;
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
            NetworkObject.Flush(networker);

    }

}
