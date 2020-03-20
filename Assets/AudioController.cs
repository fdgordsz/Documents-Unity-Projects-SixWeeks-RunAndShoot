using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    AudioSource tranca;
    [SerializeField]
    AudioSource accion;

    [SerializeField]
    float zMin = 10f;
    [SerializeField]
    float zMax = 40f;

    // Update is called once per frame
    void Update()
    {
        if (player.position.z < zMin)
        {
            tranca.volume = 1;
            accion.volume = 0;
        }
        else if (player.position.z < zMax)
        {
            float interpol = Mathf.SmoothStep(0,1,(player.position.z - zMin) / (zMax - zMin));
            Debug.Log(interpol);
            tranca.volume = Mathf.Pow(1f - interpol, 2);
            accion.volume = Mathf.Pow(interpol, 3);
        }
        else
        {
            tranca.volume = 0;
            accion.volume = 1;
        }
    }
}
