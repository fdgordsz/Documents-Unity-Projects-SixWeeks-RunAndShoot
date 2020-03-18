using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    static BulletPool instance;

    [SerializeField]
    float reloadTime = 1f;

    float coolDown;

    Bullet[] bullets;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }


    void Start()
    {
        bullets = GetComponentsInChildren<Bullet>();
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].Sleep();
        }
        
    }

    public static Transform GetTransform()
    {
        return instance.transform;
    }

    private void Update()
    {
        coolDown = Mathf.Clamp(coolDown - Time.deltaTime, -1f, reloadTime);
    }

    private static Bullet GetIdleBullet()
    {
        Bullet result = null;
        for (int i = 0; i < instance.bullets.Length; i++)
        {
            if (!instance.bullets[i].isActive())
            {
                result = instance.bullets[i];
                return result;
            }
        }
        return result;
        
    }

    public static void TryShoot()
    {
        if(instance.coolDown <= 0f)
        {
            var idleBullet = GetIdleBullet();
            if(idleBullet != null)
            {
                idleBullet.Shoot();
                instance.coolDown = instance.reloadTime;
            }
            else
            {
                Debug.Log("Bullet pool is empty!");
            }
        }
    }
}
