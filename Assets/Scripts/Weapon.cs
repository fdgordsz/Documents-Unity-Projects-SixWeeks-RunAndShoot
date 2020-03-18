using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public virtual bool TryAction()
    {
        return false;
    }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}
