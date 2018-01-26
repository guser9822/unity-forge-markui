using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using TC.GPConquest.MarkLight4GPConquest;

namespace TC.GPConquest {

    public class GameController : MonoBehaviour
    {

        private ClientNetworkController ClientNetworkController;
        private ServerOptions ServerOption;
               
        void Awake()
        {
            ClientNetworkController = GetComponent<ClientNetworkController>();
            ServerOption = gameObject.AddComponent<ServerOptions>();

            ServerOption.SetServerOptions();

            ServerOption.XProtocol.Value = UIInfoLayer.UDPString;

            ClientNetworkController.StartCustomNetworkController(ServerOption);

        }

        // Use this for initialization
        void Start()
        { 
            NetworkManager.Instance.InstantiateDwarf(0, new Vector3(10, 10, 10));
            //NetworkManager.Instance.InstantiateDwarfNetworkObject();
        }

    }

}