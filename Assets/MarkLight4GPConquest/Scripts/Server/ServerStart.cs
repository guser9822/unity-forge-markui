using UnityEngine;
using MarkLight.Views.UI;
using MarkLight;
using TC.GPConquest;

namespace TC.GPConquest.MarkLight4GPConquest
{
    public class ServerStart : UIView
    {
        public _string ServerStatus;
        public GenericPopUp GenericPopUp;
        public ServerOptions ServerOptions;
        public ServerController ServerController;

        public void Awake()
        {
            //ServerController = GameObject.FindObjectOfType<ServerController>();
        }

        public void StartServer()
        {
            if (ServerOptions != null)
            {
                //ServerController.StartServer(ServerOptions);
            }
            else
            {
                GenericPopUp.GenericPopUpMessage.Value = UIInfoLayer.ServerOptsNullMessage;
                GenericPopUp.ToggleWindow();
            }
        }

        public void StopServer()
        {
            //ServerController.EndServer();
        }

    }

}
