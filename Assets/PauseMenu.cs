using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    static PauseMenu instance;
    [SerializeField]
    Button unPause;
    [SerializeField]
    Button reset;
    [SerializeField]
    Button mainMenu;
    static bool paused = false;

    //Singleton Pattern:
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            unPause.onClick.AddListener(() => UnPauseGame());
            reset.onClick.AddListener(() => ResetLevel());
            mainMenu.onClick.AddListener(() => MainMenu());
            UnPauseGame();
        }
        else
            Destroy(this);
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UnPauseGame();
    }

    //Command to pause the game
    public static void PauseGame()
    {
        instance.gameObject.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    //Command to unpause the game
    public static void UnPauseGame()
    {
        instance.gameObject.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    //Command to reset the game
    public static void ResetLevel()
    {
        instance.gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }

    //Command to go to MainMenu
    public static void MainMenu()
    {
        instance.gameObject.SetActive(false);
        SceneManager.LoadScene(0);

    }

    public static bool IsPaused()
    {
        return paused;
    }
}
