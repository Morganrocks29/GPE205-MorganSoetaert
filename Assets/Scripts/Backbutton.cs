using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backbutton : MonoBehaviour
{
    public void BackToTitle()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateTitleScreen();
        }
    }
}
