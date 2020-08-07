using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GloveInteraction : MonoBehaviour
{
    VRTK_InteractableObject interactableObject;
    [SerializeField] private Material gloveMat;
    [SerializeField] private SkinnedMeshRenderer[] hands;

    private void OnEnable()
    {
        interactableObject = GetComponent<VRTK_InteractableObject>();
        interactableObject.InteractableObjectUsed += PutOnGlove;
    }

    private void PutOnGlove(object sender, InteractableObjectEventArgs e)
    {
        for(int i = 0; i < hands.Length; i++)
        {
            hands[i].material = gloveMat;
        }
        this.gameObject.SetActive(false);

        //GoToNextStepUsingStepControl//
        GoToNextStep();
    }

    private void GoToNextStep()
    {
        FindObjectOfType<StepControl>().DoNextStep();
    }
}
