using MarkLight.Views.UI;
using MarkLight;

namespace TC.GPConquest.MarkLight4GPConquest
{
    public class ServerStart : UIView
    {
        public _string ServerStatus;
        public GenericPopUp GenericPopUp;
        public ServerOptions ServerOptions;
        public ServerNetworkController ServerController;
        private ConnectionInfo ConnectionInfo; 

        public void Awake()
        {
            /* We add this components here because when you update the view,
            all references setted by the inspector are lost */
            ServerController = FindObjectOfType<ServerNetworkController>();
            ConnectionInfo = gameObject.AddComponent<ConnectionInfo>();
        }

        public void StartServer()
        {
            if (ServerOptions != null)
            {
               ConnectionInfo.SetConnectionInfo(ServerOptions.IpAddress,
                    ServerOptions.ServerPort,
                    (string)ServerOptions.XProtocol.Value);

                ServerController.StartCustomNetworkController(ConnectionInfo);
            }
            else
            {
                GenericPopUp.GenericPopUpMessage.Value = UIInfoLayer.ServerOptsNullMessage;
                GenericPopUp.ToggleWindow();
            }
        }

        public void StopServer()
        {
            ServerController.CloseMultiplayerBase();
        }

    }

}
