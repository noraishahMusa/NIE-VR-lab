using System.Collections;
using UnityEngine;
using VRTK;

public class CultureContactWithInnoculationTube : MonoBehaviour
{
    private IEnumerator coroutine;

    [SerializeField]
    private Transform strikeRod;
    [SerializeField]
    private Renderer strikeRodTip;
    [SerializeField]
    private Transform strikeRodTipTrans;

    [SerializeField]
    private float collectAlgaeDuration = 2.0f;

    [SerializeField]
    private Collider coll;

    private bool isHoldingStrikeRod;
    private float timer;
    private Color strikeRodTipOriginalColor;

    private void Awake()
    {
        strikeRodTipOriginalColor = strikeRodTip.material.color;
    }

    private void OnEnable()
    {
        //Debug.Log("culture tube is enabled");
        isHoldingStrikeRod = false;
        
    }

    private void Update()
    {
        if (isHoldingStrikeRod) return;
        if (GetNameOfGrabObjects.leftGrabbedItem == strikeRod || GetNameOfGrabObjects.rightGrabbedItem == strikeRod)
        {
            //Debug.Log("strike rod is held on");
            coll.enabled = true;
            isHoldingStrikeRod = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other + " is colliding with culture tube" + strikeRodTipTrans);
        if(other.transform == strikeRodTipTrans)
        {
            strikeRodTip.enabled = true;
            strikeRodTip.material.color = Color.Lerp(strikeRodTipOriginalColor, Color.green, timer/2);
            //Debug.Log("strike rod is collided with");
            timer += Time.deltaTime;
            if (strikeRodTip.material.color == Color.green)
            {
                //Debug.Log("culture has been collected");
                CultureCollected();
                coll.enabled = false;
            }
        }

    }

    private void CultureCollected()
    {
        FindObjectOfType<StepControl>().DoNextStep();
        this.enabled = false;

    }

}
