using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientController : MonoBehaviour {

    public string IpAddress;
    public string ServerPort;
    public bool useTCP;
    public GameObject networkManager = null;
    private NetworkManager mgr = null;
    //public bool DontChangeSceneOnConnect = true;


    // Use this for initialization
    void Start () {
        //InitClient();
        MultiplayerMenu m = gameObject.GetComponent<MultiplayerMenu>();
        m.Connect();
	}

    private void InitClient()
    {
        ushort port;
        if (!ushort.TryParse(ServerPort, out port))
        {
            Debug.LogError("The supplied port number is not within the allowed range 0-" + ushort.MaxValue);
            return;
        }

        NetWorker client;

        if (useTCP)
        {
            client = new TCPClient();
            ((TCPClient)client).Connect(IpAddress, (ushort)port);
        }
        else
        {
            client = new UDPClient();
            ((UDPClient)client).Connect(IpAddress, (ushort)port);
        }

        Connected(client);
    }

    private void Connected(NetWorker networker)
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
        else if (mgr == null)
            mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

        /* So, I don't know why Unity can't use the NetworkManager setted from the inspector,
             * so we need to load dynamically our Dwarf prefab. What a fucking shit. */
        //mgr.DwarfNetworkObject = new GameObject[1] { Resources.Load<GameObject>("Dwarf") };

        mgr.Initialize(networker);

        //if (networker is IServer)
        //{
        //    if (!DontChangeSceneOnConnect)
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    else
                NetworkObject.Flush(networker); //Called because we are already in the correct scene!
        }
}
