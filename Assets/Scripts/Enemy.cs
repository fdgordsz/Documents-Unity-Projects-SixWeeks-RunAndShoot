using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls Enemy movement, interaction and AI (Not very intelligent...)
public class Enemy : MonoBehaviour
{
    Vector3 originalPos;
    [SerializeField]
    float amplitud = 1;
    [SerializeField]
    float frecuency = 1;
    [SerializeField]
    Vector3 direction = Vector3.right;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
    }

    // Moves back and forth in a line
    void Update()
    {
        transform.localPosition = originalPos + direction * amplitud * Mathf.Sin(Time.time * frecuency);
    }
}
