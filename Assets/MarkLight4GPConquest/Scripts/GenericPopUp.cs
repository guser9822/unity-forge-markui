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
        public Window GenericWindow;
        public string GenericPopUpMessage;

        public void Start()
        {
            ToggleWindow();
        }

        public void ToggleWindow()
        {
            if (GenericWindow.IsOpen)
            {
                GenericWindow.Close();
            }
            else
            {
                GenericWindow.Open();
            }
        }

    }

}