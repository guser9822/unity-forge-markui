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
        public Button ServerOptionsButton;

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
            if (label.Equals(UIInfoLayer.StartServerLabel))
            {
                ToggleServerActivationButton.Text.Value = UIInfoLayer.ServerStatusLabel;
                ContentViewSwitcher.SwitchTo(1);
                ServerStart.StartServer();
                ServerOptionsButton.IsVisible.Value = false;
            }
            else if(label.Equals(ToggleServerActivationButton.Text.Value = UIInfoLayer.ServerStatusLabel))
            {
                ContentViewSwitcher.SwitchTo(1);
            }
        }

        public void CallServerOptions()
        {
            ContentViewSwitcher.SwitchTo(3);
        }

        public void CallCloseServer()
        {
            ContentViewSwitcher.SwitchTo(0);
            Application.Quit();
    }

  }

}
