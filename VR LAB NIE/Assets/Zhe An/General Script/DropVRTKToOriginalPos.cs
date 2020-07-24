using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DropVRTKToOriginalPos : MonoBehaviour
{
    VRTK_InteractableObject vrtkObject;
    Vector3 originalPos;
    Vector3 originalRot;

    private void Awake()
    {
        originalPos = transform.position;
        originalRot = transform.eulerAngles;
        vrtkObject = GetComponent<VRTK_InteractableObject>();
        vrtkObject.InteractableObjectEnabled += EnableVRTK;
        vrtkObject.InteractableObjectDisabled += DisableVRTK;
    }

    private void DisableVRTK(object sender, InteractableObjectEventArgs e)
    {
        vrtkObject.InteractableObjectUngrabbed -= DropToOriginalPos;
    }

    private void EnableVRTK(object sender, InteractableObjectEventArgs e)
    {
        vrtkObject.InteractableObjectUngrabbed += DropToOriginalPos;
    }

    private void DropToOriginalPos(object sender, InteractableObjectEventArgs e)
    {
        transform.position = originalPos;
        transform.eulerAngles = originalRot;
    }
}
