﻿using UnityEngine;
using MarkLight.Views.UI;
using MarkLight;

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
            ServerController = FindObjectOfType<ServerController>();
        }

        public void StartServer()
        {
            if (ServerOptions != null)
            {
                ServerController.StartServer(ServerOptions);
            }
            else
            {
                GenericPopUp.GenericPopUpMessage.Value = UIInfoLayer.ServerOptsNullMessage;
                GenericPopUp.ToggleWindow();
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
