using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VRTK;

public class BurnerFire : MonoBehaviour
{
    [SerializeField]
    private BunsenBurnerControl burnerControl;

    private IEnumerator coroutine;

    [SerializeField]
    private Transform strikeRod;
    [SerializeField]
    private Renderer strikeRodTip;
    [SerializeField]
    private Transform strikeRodTipTrans;

    [SerializeField]
    private float turnRedDuration = 2.0f;

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
        Debug.Log("culture tube is enabled");
        isHoldingStrikeRod = false;
    }

    private void Update()
    {
        if (isHoldingStrikeRod) return;
        if (GetNameOfGrabObjects.leftGrabbedItem == strikeRod || GetNameOfGrabObjects.rightGrabbedItem == strikeRod)
        {
            Debug.Log("strike rod is held on");
            coll.enabled = true;
            isHoldingStrikeRod = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform == strikeRodTipTrans)
        {
            strikeRodTip.enabled = true;
            strikeRodTip.material.color = Color.Lerp(Color.black, Color.red, timer / turnRedDuration);
            timer += Time.deltaTime;
            if (strikeRodTip.material.color == Color.red)
            {
                Debug.Log("rod has been heated");
                RodHeated();
                coll.enabled = false;
            }
        }

    }

    private void RodHeated()
    {
        FindObjectOfType<StepControl>().DoNextStep();
        this.enabled = false;

    }

    }
