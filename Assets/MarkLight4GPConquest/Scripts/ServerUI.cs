using MarkLight.Views.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TC.GPConquest.MarkLight4GPConquest {

    public class ServerUI : UIView{

        public ViewSwitcher ContentViewSwitcher;
        public ServerStart ServerStart;
        public ServerOptions ServerOptions;
        public Button ToggleServerActivationButton;
        
        private static readonly string StartServerLabel = "Start Server";
        private static readonly string StopServerLabel = "Stop Server";

        public override void Initialize()
        {
            base.Initialize();
            //Sets references
            ServerStart.ServerOptions = ServerOptions;
        }

        private void Start()
        {
            //When ServerUI starts(ServerOptions is already initialized), uses default settings for the server
            ServerOptions.DefaultLocalServer();
        }

        public void ToggleServerActivation()
        {
            string label = ToggleServerActivationButton.Text;
            if (label.Equals(StartServerLabel))
            {
                ToggleServerActivationButton.Text.Value = StopServerLabel;
                ContentViewSwitcher.SwitchTo(1);
                ServerStart.StartServer();
            }
            else if(label.Equals(StopServerLabel))
            {
                ToggleServerActivationButton.Text.Value = StartServerLabel;
                ContentViewSwitcher.SwitchTo(2);
            }
        }

        public void CallServerOptions()
        {
            ContentViewSwitcher.SwitchTo(3);
        }

        public void CallCloseServer()
        {
            ContentViewSwitcher.SwitchTo(0);
            //Application.Quit();
        }

  }

}
