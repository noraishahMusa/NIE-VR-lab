using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StrikingLine_Drawer : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private Material lineMat;
    [SerializeField] private Transform allLines;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Strike_SectorControl sectorControl;
    [SerializeField] private Renderer strikeRodTip;

    [SerializeField] private Transform[] targets;
    VRTK_Pointer controllerPointer;

    List<Vector3> linePositions = new List<Vector3>();

    private bool itemsOnHand;
    private bool activate;
    private bool completeStep;


    private void OnEnable()
    {
        sectorControl.enabled = true;
    }

    private void Update()
    {
        UpdateForLeftRightGrabbedItems();
    }

    private void UpdateForLeftRightGrabbedItems()
    {
        //Debug.Log(GetNameOfGrabObjects.rightGrabbedItem + " " + GetNameOfGrabObjects.leftGrabbedItem);
        if (GetNameOfGrabObjects.rightGrabbedItem == null || GetNameOfGrabObjects.leftGrabbedItem == null)
        {
            DeactivateStrikingPanel();
            return;
        }
        if ((GetNameOfGrabObjects.rightGrabbedItem == targets[0] && GetNameOfGrabObjects.leftGrabbedItem == targets[1]) ||
            (GetNameOfGrabObjects.rightGrabbedItem == targets[1] && GetNameOfGrabObjects.leftGrabbedItem == targets[0]))
        {
            if (itemsOnHand == false)
            {

                ActivateStrikingPanel();
            }

        }
    }

    private void ActivateStrikingPanel()
    {

        controllerPointer = GetNameOfGrabObjects.strikeRodPointer;
        //Debug.Log(controllerPointer);
        controllerPointer.SelectionButtonPressed += StartStrike;
        controllerPointer.SelectionButtonReleased += StopStrike;
        sectorControl.DoNextSequence();
        controllerPointer.DestinationMarkerHover += HoveringOverStrikePanel;
        itemsOnHand = true;
    }

    private void StopStrike(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("stop strike");
        activate = false;
        bool sideSpot = Strike_SectorControl.isSideSpot;
        bool lastSpot = Strike_SectorControl.isLastSector;
        if (!completeStep) return;
        if (sideSpot || lastSpot)
        {
            sectorControl.DoNextSequence();
            positionA = Vector3.zero;
            if (line) line = null;
            completeStep = false;
            linePositions.Clear();
            FindObjectOfType<StepControl>().DoNextStep();
            return;

        } else
        {
            DeactivateStrikingPanel();
        }


    }

    private void StartStrike(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("start strike");
        activate = true;
    }

    private void DeactivateStrikingPanel()
    {
        if (controllerPointer == null) return;
        Debug.Log("deactivate strike panel");
        controllerPointer.SelectionButtonPressed -= StartStrike;
        controllerPointer.SelectionButtonReleased -= StopStrike;
        controllerPointer.DestinationMarkerHover -= HoveringOverStrikePanel;
        itemsOnHand = false;
        positionA = Vector3.zero;
        if (line) line = null;
        linePositions.Clear();
        controllerPointer = null;
        completeStep = false;
        this.gameObject.SetActive(false);

        //change current section to the next one
        

        //GoToNextStep
        FindObjectOfType<StepControl>().DoNextStep();
    }

    private void HoveringOverStrikePanel(object sender, DestinationMarkerEventArgs e)
    {
        //Debug.Log(e.target.name);
        if (e.target.name == "Plate" && activate)
        {

            DrawLine(e.destinationPosition);
        }

    }

    


    Vector3 positionA = Vector3.zero;

    private void DrawLine(Vector3 pos)
    {
        if (!line)
        {
            Debug.Log("create line");
            GameObject lineObj = Instantiate(linePrefab, allLines);
            line = lineObj.GetComponent<LineRenderer>();
            line.positionCount += 1;
            Vector3 v = allLines.parent.position - pos;
            positionA = new Vector3(v.x, -v.y, v.z);
            //line.material.color = CheckIfInSection(positionA, sections[0]);
            line.SetPosition(0, positionA);
            linePositions.Add(positionA);
            //Debug.Log("set pos A");

        }
        else
        {
            if (Vector3.Distance(positionA, pos - allLines.parent.position) > 0.1f)
            {
                line.positionCount += 1;
                Vector3 v = allLines.parent.position - pos;
                positionA = new Vector3(v.x, -v.y, v.z);
                //line.material.color = CheckIfInSection(positionA, sections[0]);
                line.SetPosition(line.positionCount - 1, positionA);
                linePositions.Add(positionA);
                //Debug.Log(pos);


                CheckLineIfInSection();
            }
        }

    }

    private void CheckLineIfInSection()
    {
        //Debug.Log(line.positionCount + " total number of line positions");
        if (Strike_SectorControl.isSideSpot)
        {
            if(line.positionCount > 10)
            {
                Debug.Log("side spot line position count");
                completeStep = true;
            }
        } else
        {
            if (line.positionCount > 200)
            {
                Debug.Log("non side spot line position count");
                completeStep = true;

            }
        }

    }

    private Color CheckIfInSection(Vector3 pos, Section currSection)
    {
        if (pos.x < currSection.minX || pos.x > currSection.maxX || pos.y < currSection.minY || pos.y > currSection.maxY)
        {
            return Color.red;
        }
        else
            return Color.cyan;
    }

    [Serializable]
    public class Section
    {
        public float minX, maxX, minY, maxY;
    }

    public Section[] sections;



}
