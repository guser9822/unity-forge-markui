using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkLight.Views.UI;
using MarkLight;

namespace TC.GPConquest.MarkLight4GPConquest
{

    public class ServerOptions : UIView
    {
        public ComboBox ComboBoxProtocols;
        public ObservableList<string> InternetProtocols;
        public InputField InputIp;
        public InputField InputPort;
        [MapTo("InputIp.Text")]
        public _string IpAddress;
        [MapTo("InputPort.Text")]
        public _string ServerPort;
        [MapTo("ComboBoxProtocols.SelectedItem")]
        public _object XProtocol;

        public override void Initialize()
        {
            base.Initialize();
            // initialize and populate ip list
            InternetProtocols = InitializeIPList("UDP","TCP");
        }

        private ObservableList<string> InitializeIPList(params string[] ips)
        {
            InternetProtocols = new ObservableList<string>();
            foreach (string ip in ips) {
                InternetProtocols.Add(ip);
            }
            return InternetProtocols;
        }

        public void DefaultLocalServer()
        {
            //Sets up a local UDP server as default
            IpAddress.Value = "127.0.0.1";
            ServerPort.Value = "15672";
            //Sets UDP as default internet protocol
            InternetProtocols.SelectedIndex = 0;
            XProtocol = ComboBoxProtocols.SelectedItem;
        }
    }

}
