using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Class that controls dialog display
public class DialogCanvas : MonoBehaviour
{
    static DialogCanvas instance;
    //TODO: find a way to avoid dependency injections
    [SerializeField]
    TextMeshProUGUI content;

    //Singleton Pattern:
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    //U: Hides dialog box
    public static void HideDialog()
    {
        instance.gameObject.SetActive(false);
    }

    //U: Enables dialog box and prints message
    public static void ShowDialog(string text)
    {
        instance.gameObject.SetActive(true);
        instance.content.text = text;
    }

    //U: Enables dialog box and prints last message
    public static void ShowDialog()
    {
        instance.gameObject.SetActive(false);
    }
}
