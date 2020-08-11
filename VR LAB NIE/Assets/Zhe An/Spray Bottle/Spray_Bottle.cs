using System;
using System.Collections;
using UnityEngine;
using VRTK;
public class Spray_Bottle : MonoBehaviour
{
    IEnumerator coroutine;
    [SerializeField] private Animator sprayAnim;

    private VRTK_InteractableObject interactableObject;
    public bool isOn;
    private bool firstTimeOn;

    private void OnEnable()
    {
        interactableObject = GetComponent<VRTK_InteractableObject>();
        interactableObject.InteractableObjectNearTouched += TouchObject;
        sprayAnim.speed = 0;
    }

    private void OnDisable()
    {
        interactableObject.InteractableObjectNearTouched -= TouchObject;
    }

    private void TouchObject(object sender, InteractableObjectEventArgs e)
    {
        //Debug.Log(e.interactingObject.name + " touch object");
        VRTK_InteractGrab iGrab = e.interactingObject.GetComponent<VRTK.VRTK_InteractGrab>();
        if (iGrab.GetGrabbedObject() != null)
            return;
        sprayAnim.Play("sprayWater");
        sprayAnim.speed = 1;
        StartCoroutine(WaitToAnimFinish());
    }

    IEnumerator WaitToAnimFinish()
    {

        yield return new WaitForSeconds(sprayAnim.runtimeAnimatorController.animationClips[0].length);
        sprayAnim.speed = 0;
        FindObjectOfType<StepControl>().DoNextStep();
        this.enabled = false;
    }
}
