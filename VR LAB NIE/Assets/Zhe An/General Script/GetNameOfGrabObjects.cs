using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GetNameOfGrabObjects : MonoBehaviour
{

    public VRTK_InteractGrab leftInteractGrab, rightInteractGrab;
    public static Transform leftGrabbedItem, rightGrabbedItem;
    public static VRTK_Pointer strikeRodPointer;

    [SerializeField] private Transform strikingRod;

    private void Awake()
    {
        if(leftInteractGrab && rightInteractGrab)
        {
            leftInteractGrab.ControllerStartGrabInteractableObject += AddTransformOfLeftItem;
            leftInteractGrab.ControllerStartUngrabInteractableObject += RemoveTransformOfLeftItem;
            rightInteractGrab.ControllerStartGrabInteractableObject += AddTransformOfRightItem;
            rightInteractGrab.ControllerStartUngrabInteractableObject += RemoveTransformOfRighttem;
        }

    }

    private void RemoveTransformOfRighttem(object sender, ObjectInteractEventArgs e)
    {
        if (rightGrabbedItem == null) return;
        if (rightGrabbedItem == strikingRod)
        {
            strikeRodPointer = null;
        }
        rightGrabbedItem = null;

    }

    private void AddTransformOfRightItem(object sender, ObjectInteractEventArgs e)
    {
        rightGrabbedItem = e.target.transform;
        if(rightGrabbedItem == strikingRod)
        {
            strikeRodPointer = rightInteractGrab.GetComponent<VRTK_Pointer>();
        }
    }

    private void AddTransformOfLeftItem(object sender, ObjectInteractEventArgs e)
    {
        leftGrabbedItem = e.target.transform;

        if (leftGrabbedItem == strikingRod)
        {
            strikeRodPointer = leftInteractGrab.GetComponent<VRTK_Pointer>();
        }
    }

    private void RemoveTransformOfLeftItem(object sender, ObjectInteractEventArgs e)
    {
        if (leftGrabbedItem == null) return;
        if (leftGrabbedItem == strikingRod)
        {
            strikeRodPointer = null;
        }
        leftGrabbedItem = null;


    }
}
