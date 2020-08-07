using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEditor;
using UnityEngine;

public class StepControl : MonoBehaviour
{
    [SerializeField] private List<Step> steps;
    [SerializeField] private ProcessCanvas processCanvas;
    public static int stepCount;
    public static Step currStep;

    private void Start()
    {
        //begin first step
        DoFirstStep();
    }

    void DoFirstStep()
    {
        currStep = steps[stepCount];
        processCanvas.enabled = true;
        processCanvas.instructionInCanvas.text = currStep.CurrInstructionSet();

    }

    public void DoNextStep()
    {
        if (currStep.deactivateAfterDone) currStep.StopThisStep();
        if (processCanvas.enabled) return;
        Debug.Log("do next step");
        stepCount++;
        currStep = steps[stepCount];
        Invoke("OpenNextStepInstruction", 1.5f);

    }

    private void OpenNextStepInstruction()
    {
        if (currStep.hasInstruction)
        {
            processCanvas.enabled = true;
            processCanvas.instructionInCanvas.text = currStep.CurrInstructionSet();
            StartCoroutine(CarryOnGivingAdditionalInstruction());
        }

    }

    IEnumerator CarryOnGivingAdditionalInstruction()
    {
        if (currStep != steps[stepCount + 1])
            yield break;
        //this step and next step is the same because there is additional instructions to be given.
        while (processCanvas.enabled)
        {
            Debug.Log("next set of instruction is the same step so we are waiting to do next step immediately after the canvas panel is closed.");
            yield return null;
        }

        DoNextStep();
    }

    public void ActivateNextStepInteractions()
    {
        currStep.DoThisStep();
    }




}

