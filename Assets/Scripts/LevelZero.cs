using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class with levelzero logic
public class LevelZero : MonoBehaviour
{
    [SerializeField]
    string initialText;
    [SerializeField]
    Transform target;

    void Start()
    {
        DialogCanvas.ShowDialog(initialText);
        GameCamera.SetTarget(target);
        GameCamera.GoToCloseUp(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameCamera.GetState() == GameCamera.CamState.CLOSEUP)
        {
            DialogCanvas.HideDialog();
            GameCamera.GoToStandard(() => {
                PlayerController.SetActive(true);
                CamGUI.ShowLifes();
                });
        }

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
