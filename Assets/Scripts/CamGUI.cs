using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Controls GUI elements like life
public class CamGUI : MonoBehaviour
{
    public static void ShowLifes(Action cb = null)
    {
        Lifes.Enable(cb);
    }

    public static void HideLifes()
    {
        Lifes.Disable();
    }
}
