using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manage weapons actions, selection, etc.
public class WeaponController : MonoBehaviour
{
    static WeaponController instance;
    static Weapon[] weapons;
    static Weapon activeWeapon;

    //Singleton Pattern
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

    //Looks for all the weapons in childs
    private void Start()
    {
        weapons = instance.GetComponentsInChildren<Weapon>();
        if (weapons != null && weapons.Length > 0)
        {
            activeWeapon = weapons[0];
            Debug.Log("Weapon Controller: " + weapons.Length.ToString() +" weapons found!");
        }
        else
        {
            Debug.Log("Weapon Controller: No weapon found!");
        }
            
    }

    //U: Changes weapon, id is the position in weapons Hierarchy
    public static void SwitchWeapon(int id)
    {
        Debug.Log("id:" + id.ToString());
        if (id >= 0 && id < weapons.Length)
        {
            Debug.Log("inside");
            for (int i = 0; i < weapons.Length; i++)
            {
                if(i == id)
                {
                    activeWeapon = weapons[i];
                    activeWeapon.Enable();
                }
                else
                {
                    weapons[i].Disable();
                }
            }
        }
    }

    //U: ShootS/AttackS if weapon is loaded/ready
    public static void TryAction()
    {
        if (activeWeapon != null)
        {
            activeWeapon.TryAction();
        } 
    }
}
