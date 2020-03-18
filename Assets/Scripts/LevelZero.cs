using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class with levelzero logic
public class LevelZero : MonoBehaviour
{
    //Text that shows in initial message box
    [SerializeField]
    string initialText;
    //The target that camera will follow (player)
    [SerializeField]
    Transform target;

    //Initial State of the scene
    void Start()
    {
        DialogCanvas.ShowDialog(initialText);
        GameCamera.SetTarget(target);
        GameCamera.GoToCloseUp(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Camera moves away from the player (bird view)
        if (Input.GetKeyDown(KeyCode.Space) && GameCamera.GetState() == GameCamera.CamState.CLOSEUP)
        {
            DialogCanvas.HideDialog();
            GameCamera.GoToStandard(() => {
                PlayerController.SetActive(true);
                CamGUI.ShowLifes();
                });
        }

        //Camera moves in front of the player
        if (Input.GetKeyDown(KeyCode.Space) && GameCamera.GetState() == GameCamera.CamState.STADARD)
        {
            
            GameCamera.GoToCloseUp(() => {
                DialogCanvas.ShowDialog(initialText);
                PlayerController.SetActive(false);
                CamGUI.HideLifes();
            });
        }

    }
}
