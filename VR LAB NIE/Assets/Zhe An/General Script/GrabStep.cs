using VRTK;
using UnityEngine;
using System;

[RequireComponent(typeof(VRTK_InteractableObject))]
[RequireComponent(typeof(Step))]
public class GrabStep : MonoBehaviour
{
    VRTK_InteractableObject interactableObj;
    [SerializeField] private bool doStepOnGrab, doStepOnUngrab;
    bool hasDoneStep;

    private void Awake()
    {
        interactableObj = GetComponent<VRTK_InteractableObject>();
    }

    private void OnEnable()
    {
        if(doStepOnGrab) interactableObj.InteractableObjectGrabbed += DoNextStep;
        if (doStepOnUngrab) interactableObj.InteractableObjectUngrabbed += DoNextStep;
    }

    private void OnDisable()
    {
        if (doStepOnGrab) interactableObj.InteractableObjectGrabbed -= DoNextStep;
        if (doStepOnUngrab) interactableObj.InteractableObjectUngrabbed -= DoNextStep;
        hasDoneStep = false;
    }

    private void DoNextStep(object sender, InteractableObjectEventArgs e)
    {
        if (hasDoneStep) return;
        FindObjectOfType<StepControl>().DoNextStep();
        hasDoneStep = true;
    }
}
