using UnityEngine;
using VRTK;
using UnityEngine.UI;
using System;

public class ProcessCanvas : MonoBehaviour
{

    [SerializeField] private VRTK_ControllerEvents vrtkEvents;
    [SerializeField] private GameObject theCanvas;
    [SerializeField] private StepControl stepControl;
    public Text instructionInCanvas;

    // Start is called before the first frame update
    void OnEnable()
    {
        theCanvas.SetActive(true);
        vrtkEvents.ButtonOnePressed += ClosePanel;
    }

    private void OnDisable()
    {
        vrtkEvents.ButtonOnePressed -= ClosePanel;
    }

    private void ClosePanel(object sender, ControllerInteractionEventArgs e)
    {
        theCanvas.SetActive(false);
        stepControl.ActivateNextStepInteractions();
        this.enabled = false;
    }

    public void ChangeText(string _value)
    {
        instructionInCanvas.text = _value;
    }
}
