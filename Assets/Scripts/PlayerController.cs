using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//U: Class that controlls character movement/actions
public class PlayerController : MonoBehaviour
{
    //TODO: Move parameters to json or similar
    [SerializeField]
    float movementSpeed = 10;

    //U: If the player controlls are active (not in a dialog)
    static bool isActive = false;

    //U: C is for no movement
    enum Direction { C, N, NE, E, SE, S, SW, W, NW};

    //U: Direction mapping to vectors
    Dictionary<Direction, Vector3> directions = new Dictionary<Direction, Vector3>
    {
        [Direction.C] = Vector3.zero,
        [Direction.N] = Vector3.forward,
        [Direction.NE] = (Vector3.forward + Vector3.right) / 1.41f,
        [Direction.E] = Vector3.right,
        [Direction.SE] = (-Vector3.forward + Vector3.right) / 1.41f,
        [Direction.S] = -Vector3.forward,
        [Direction.SW] = (-Vector3.forward - Vector3.right) / 1.41f,
        [Direction.W] = -Vector3.right,
        [Direction.NW] = (Vector3.forward - Vector3.right) / 1.41f,
    };

    //U: Returns the player controls status
    public static void SetActive(bool active)
    {
        isActive = active;
    }

    //U: Check user inputs and execute player actions, only works if player control is active
    void Update()
    {
        if (isActive)
        {
            Move();
            LookAt();
            WeaponAction();
        }
    }

    //U: Returns the direction the player should move depending on user input
    private Direction GetDirectionInput()
    {
        Direction result = Direction.C;

        //U: Checks user input
        bool upKey = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
        bool rightKey = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
        bool downKey = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
        bool leftKey = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));

        //U: If two incompatible keys are pressed they neutralize each other
        if (downKey && upKey)
        {
            downKey = false;
            leftKey = false;
        }
        if (rightKey && leftKey)
        {
            rightKey = false;
            leftKey = false;
        }

        //U: First check for diagonals moves, then for axis movement
        if (upKey && rightKey)
        {
            result = Direction.NE;
        }
        else if (downKey && rightKey)
        {
            result = Direction.SE;
        }
        else if (downKey && leftKey)
        {
            result = Direction.SW;
        }
        else if (upKey && leftKey)
        {
            result = Direction.NW;
        }
        else if (upKey)
        {
            result = Direction.N;
        }
        else if (rightKey)
        {
            result = Direction.E;
        }
        else if (downKey)
        {
            result = Direction.S;
        }
        else if (leftKey)
        {
            result = Direction.W;
        }

        return result;
    }

    //U: Moves the player around
    private void Move()
    {
        float delta = Time.deltaTime * movementSpeed;
        Direction dir = GetDirectionInput();
        transform.localPosition += directions[dir] * delta;
    }

    //U: Rotates the player towards the aim
    private void LookAt()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;

        if (plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 forward = Vector3.Cross(Vector3.up, hitPoint - transform.position);
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
    }

    //U: Shoots or change weapon if commanded
    private void WeaponAction()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Player Controller: Weapon 0 Selected");
            WeaponController.SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Player Controller: Weapon 1 Selected");
            WeaponController.SwitchWeapon(1);
        }

        if (Input.GetMouseButton(0))
        {
            WeaponController.TryAction();
        }
    }
}
