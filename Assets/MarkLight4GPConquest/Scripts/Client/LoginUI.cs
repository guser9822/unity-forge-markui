using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkLight.Views.UI;
using MarkLight;
using UnityEngine.SceneManagement;


namespace TC.GPConquest.MarkLight4GPConquest
{
    public class LoginUI : UIView
    {
        public InputField InputUsername;
        public InputField InputPass;
        public Button LogInButton;
        public string IpAddress = "127.0.0.1";
        public string ServerPort = "15672";

        public void CallConnectToServer()
        {
            //Verify username/pass
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
