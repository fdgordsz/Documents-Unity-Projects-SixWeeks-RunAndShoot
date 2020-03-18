using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//U: Class that controls camera movement and propertys
public class GameCamera : MonoBehaviour
{
    //TODO: Move config data to json or similar:
    //--------------------------------------------------------------------------------
    // Camera configuration for CLOSEUP
    [Header("CloseUp")]
    [SerializeField]
    float closeSize;
    [SerializeField]
    Vector3 closePos;
    [SerializeField]
    Quaternion closeRot;

    // Camera configuration for STANDARD
    [Header("Isometric")]
    [SerializeField]
    float standardSize;
    [SerializeField]
    Vector3 standardPos;
    [SerializeField]
    Quaternion standardRot;

    // Other Camera configuration settings
    [Header("Other Config")]
    [SerializeField]
    float transDuration;
    //--------------------------------------------------------------------------------

    //Class vars:
    static GameCamera instance;
    static Transform camTrans;
    static Camera cam;
    static Transform target;
    public enum CamState {STANDARD, CLOSEUP, CUSTOM, TRANSITION}
    static CamState state;

    //Singleton Pattern:
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            camTrans = transform;
            cam = GetComponent<Camera>();
        }
        else
            Destroy(this);
    }

    //Returns Camera state
    public static CamState GetState()
    {
        return state;
    }

    //U: Sets camera target (player generally)
    public static void SetTarget(Transform targ)
    {
        target = targ;
    }

    //U: Interpolates smoothly to custom position, rotation and size
    public static void GoToCustom(Quaternion rot, Vector3 offset, float size, float duration, Action cb = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(GoToCo(rot, offset, size, duration, CamState.CUSTOM, cb));
    }

    //U: Interpolates smoothly to "Close Up" postion, rotation and size
    public static void GoToCloseUp(float duration, Action cb = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(GoToCo(instance.closeRot, instance.closePos, instance.closeSize, duration, CamState.CLOSEUP, cb));
    }

    public static void GoToCloseUp(Action cb = null)
    {
        GoToCloseUp(instance.transDuration, cb);
    }

    //U: Interpolates smoothly to "Standard" postion, rotation and size
    public static void GoToStandard(float duration, Action cb = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(GoToCo(instance.standardRot, instance.standardPos, instance.standardSize, duration, CamState.STANDARD, cb));
    }

    public static void GoToStandard(Action cb = null)
    {
        GoToStandard(instance.transDuration, cb);
    }
    
    //U: Coroutine that interpolates to final pos, rot and size
    static IEnumerator GoToCo(Quaternion rot, Vector3 offset, float size, float duration, CamState finalState, Action cb)
    {
        float counter = 0;
        float smoothedCounter = 0;
        float speed = 1f / duration;

        Vector3 initialOffset = camTrans.position - target.position;
        Quaternion initialRot = camTrans.rotation;
        float initialsSize = cam.orthographicSize;

        state = CamState.TRANSITION;
        //A: Smooth interpolation using smoothstep function
        while (counter < 1f)
        {
            counter += Time.deltaTime * speed;
            smoothedCounter = Mathf.SmoothStep(0, 1, counter);
            camTrans.position = target.position + Vector3.Lerp(initialOffset, offset, smoothedCounter);
            camTrans.rotation = Quaternion.Lerp(initialRot, rot, smoothedCounter);
            cam.orthographicSize = Mathf.Lerp(initialsSize, size, smoothedCounter);
            yield return null;
        }

        //A: Camera reachs final position, rotation and size:
        state = finalState;
        if (cb != null)
            cb();

        //Repeat once per frame until a new "GoTo()" is commanded:
        while (true)
        {
            camTrans.localPosition = target.position + offset;
            camTrans.rotation = rot;
            cam.orthographicSize = size;
            yield return null;
        }
    }
        
}
