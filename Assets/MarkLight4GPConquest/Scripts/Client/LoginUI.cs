using MarkLight.Views.UI;
using UnityEngine.SceneManagement;


namespace TC.GPConquest.MarkLight4GPConquest
{
    public class LoginUI : UIView
    {
        public InputField InputUsername;
        public InputField InputPass;
        public Button LogInButton;

        public void CallConnectToServer()
        {
            //Verify username/pass
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
