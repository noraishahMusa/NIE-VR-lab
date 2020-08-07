using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    private Step_InteractableEnabler stepInteractable;
    public  string[] instructionText;
    private int instructionSetCount;
    [HideInInspector]
    public bool hasInstruction;
    public bool deactivateAfterDone;


    private void Awake()
    {
        stepInteractable = GetComponent<Step_InteractableEnabler>();
        if (instructionText.Length == 0) hasInstruction = false;
        else hasInstruction = true;
    }

    public void DoThisStep()
    {
        if (stepInteractable == null) return;
        stepInteractable.EnableInteract();
    }

    public void StopThisStep()
    {
        if (stepInteractable == null) return;
        stepInteractable.DisableInteract();
    }

    public string CurrInstructionSet()
    {
        instructionSetCount++;
        return instructionText[instructionSetCount -1];
    }
}
