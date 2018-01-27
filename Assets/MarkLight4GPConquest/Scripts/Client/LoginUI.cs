using MarkLight.Views.UI;
using UnityEngine.SceneManagement;


namespace TC.GPConquest.MarkLight4GPConquest
{
    public class LoginUI : UIView
    {
        public InputField InputUsername;
        public InputField InputPass;
        public Button LogInButton;
        public ClientNetworkController ClientNetworkController;
        public ConnectionInfo ConnectionInfo;

        private void Start()
        {
            ClientNetworkController = FindObjectOfType<ClientNetworkController>();
            ConnectionInfo = gameObject.AddComponent<ConnectionInfo>();
        }

        public void CallConnectToServer()
        {
            //Verify username/pass
            ConnectionInfo.SetConnectionInfo();
            ClientNetworkController.StartCustomNetworkController(ConnectionInfo);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
