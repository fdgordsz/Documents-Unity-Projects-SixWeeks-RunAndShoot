using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform poolTrans;

    bool active;
    Vector3 originalPos;
    [SerializeField]
    float lifeSpam = 3f;
    float life = 0;
    [SerializeField]
    float speed = 3f;

    private void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    private void Start()
    {
        poolTrans = BulletPool.GetTransform();
    }

    public void Sleep()
    {
        if(poolTrans == null)
            poolTrans = BulletPool.GetTransform();
        transform.parent = poolTrans;
        transform.localPosition = originalPos;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        active = false;
    }

    public bool isActive()
    {
        return active;
    }

    public void Shoot()
    {

        gameObject.SetActive(true);
        active = true;
        transform.parent = null;
        life = 0;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        life += Time.deltaTime;
        if (life > lifeSpam)
            Sleep();
    }
}
