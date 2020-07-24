using System.Collections;
using UnityEngine;
using VRTK;

public class CultureContactWithInnoculationTube : MonoBehaviour
{
    private VRTK_InteractableObject interactableObject;
    private IEnumerator coroutine;

    [SerializeField]
    private Transform strikeRod;

    [SerializeField]
    private float collectAlgaeDuration = 2.0f;

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
            if (coroutine != null)
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
        ; Debug.Log(iGrab.GetGrabbedObject().name + " " + target);
        if (obj.name == strikeRod.name)
        {
            Debug.Log("rod is entering fire");
            coroutine = CollectAlgae(target);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator CollectAlgae(GameObject obj)
    {
        float t = 0;
        //Material mat = obj.GetComponent<Renderer>().material;
        //Color originalColor = mat.color;
        while (t < collectAlgaeDuration)
        {
            t += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("color has changed to red");
    }

}
