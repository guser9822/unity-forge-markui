using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkLight.Views.UI;
using MarkLight;

namespace TC.GPConquest.MarkLight4GPConquest
{
    public class ServerStart : UIView
    {
        public _string ServerStatus;
        public GenericPopUp GenericPopUp;
        public ServerOptions ServerOptions;
         
        public void StartServer()
        {
            if (ServerOptions != null)
            {
                Debug.Log("startted");
            }
            else
            {
                GenericPopUp.GenericPopUpMessage = ServerOptions.OptsNull;    
            }
        }

        public void StopServer(ServerOptions _ServerOptions)
        {
            if (_ServerOptions != null)
                Debug.Log("Stopping server");
            else Debug.Log("Error");
        }

    }

}
