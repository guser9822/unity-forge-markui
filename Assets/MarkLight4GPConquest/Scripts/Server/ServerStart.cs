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

        public void Awake()
        {

        }

        public void StartServer()
        {
            if (ServerOptions != null)
            {
                ServerController.StartCustomNetworkController(ServerOptions);
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
