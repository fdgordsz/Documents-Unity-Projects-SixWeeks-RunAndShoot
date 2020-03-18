using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls hearts displays and animtations at top left corner of the screen
public class Lifes : MonoBehaviour
{
    static Lifes instance;
    [SerializeField]
    Transform[] hearts;

    //Singleton Pattern
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            foreach(var heart in hearts)
            {
                heart.localScale = Vector3.zero;
            }
        }
    }

    //U: Plays animation
    static public void Enable(Action cb = null)
    {
        instance.StartCoroutine(instance.EnableCo(0,cb));
    }

    //U: Hides hearts
    static public void Disable()
    {
        instance.StopAllCoroutines();
        foreach (var heart in instance.hearts)
        {
            heart.transform.localScale = Vector3.zero;
        }
    }

    //U: Plays animation
    IEnumerator EnableCo(float delay = 0, Action cb = null)
    {
        float speed = 3;
        float counter = 0;
        yield return new WaitForSeconds(delay);
        while(counter < hearts.Length + 1)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                float interpol = counter - i;
                float val;
                if(interpol > 0)
                {
                    if(interpol < 1f)
                    {
                        val = Mathf.SmoothStep(0,1.3f, interpol);
                    }
                    else if (interpol < 1.5f)
                    {
                        val = Mathf.SmoothStep(1.3f, 1f, (interpol - 1f) * 2f);
                    }
                    else
                    {
                        val = 1f;
                    }
                    hearts[i].localScale = Vector3.one * val;
                }
            }
            counter += Time.deltaTime * speed;
            yield return null;
        }
        if (cb != null)
            cb();
    }
}
