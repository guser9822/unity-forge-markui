using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarkLight.Views.UI;
using MarkLight;

namespace TC.GPConquest.MarkLight4GPConquest
{
    [HideInPresenter]
    public class GenericPopUp : UIView
    {
        public Window Window;
        public _string GenericPopUpMessage;

        public void ToggleWindow()
        {
            if (Window.IsOpen)
            {
                Window.Close();
            }
            else
            {
                Window.Open();
            }
        }

    }

}