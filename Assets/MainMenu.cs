using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button comenzar;
    [SerializeField]
    Button creditos;
    [SerializeField]
    Button salir;

    private void Start()
    {
        comenzar.onClick.AddListener(() => SceneManager.LoadScene(2));
        creditos.onClick.AddListener(() => SceneManager.LoadScene(1));
        salir.onClick.AddListener(() => Application.Quit());
    }
}
