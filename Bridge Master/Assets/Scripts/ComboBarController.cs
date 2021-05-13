using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack; // MUIP namespace required

public class ComboBarController : MonoBehaviour
{

    void Update()
    {
       // ComboBar();
    }

public ProgressBar myBar; // Your pb variable
void ComboBar()
{
        myBar.textPercent.text = "X5";
    //myBar.currentPercent = 48f; // set current percent
    myBar.speed = 15; // set speed
    myBar.invert = true; // 100 to 0
   myBar.invert = false; // 0 to 100
    myBar.restart = true; // restart when it's 100
    myBar.isOn = false; // enable or disable counting
}
}
