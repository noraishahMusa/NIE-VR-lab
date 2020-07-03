using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VRTK;

public class BurnerFire : MonoBehaviour
{
    [SerializeField]
    private BunsenBurnerControl burnerControl;

    private VRTK_InteractableObject interactableObject;
    private IEnumerator coroutine;

    [SerializeField]
    private Transform strikeRod;

    [SerializeField]
    private float turnRedDuration = 2.0f;

    private void Awake()
    {
        interactableObject = GetComponent<VRTK_InteractableObject>();


        interactableObject.InteractableObjectDisabled += WhenDisabled;
        interactableObject.InteractableObjectEnabled += WhenEnabled;
    }

    private void WhenEnabled(object sender, InteractableObjectEventArgs e)
    {
        interactableObject.InteractableObjectNearTouched += TouchObject;
        interactableObject.InteractableObjectNearUntouched += UntouchObject;
    }



    private void WhenDisabled(object sender, InteractableObjectEventArgs e)
    {
        interactableObject.InteractableObjectNearTouched -= TouchObject;
        interactableObject.InteractableObjectNearUntouched -= UntouchObject;
    }

    private void UntouchObject(object sender, InteractableObjectEventArgs e)
    {
        VRTK_InteractGrab iGrab = e.interactingObject.GetComponent<VRTK_InteractGrab>();
        Debug.Log(iGrab.GetGrabbedObject().name);
        if (iGrab.GetGrabbedObject().name == strikeRod.name)
        {
            Debug.Log("rod is leaving fire");
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }

    private void TouchObject(object sender, InteractableObjectEventArgs e)
    {
        VRTK_InteractGrab iGrab = e.interactingObject.GetComponent<VRTK_InteractGrab>();

        GameObject obj = iGrab.GetGrabbedObject();
        GameObject target = obj.transform.Find("inoculation loop/strike rod").gameObject;
;        Debug.Log(iGrab.GetGrabbedObject().name + " " + target);
        if (obj.name == strikeRod.name)
        {
            Debug.Log("rod is entering fire");
            coroutine = TurnMaterialRed(target);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator TurnMaterialRed(GameObject obj)
    {
        float t = 0;
        Material mat = obj.GetComponent<Renderer>().material;
        Color originalColor = mat.color;
        while(t < turnRedDuration)
        {
            mat.color = Color.Lerp(originalColor, Color.red, t/turnRedDuration);
            t += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("color has changed to red");
    }

    }
