using System;
using System.Collections;
using UnityEngine;
using VRTK;
public class BunsenBurnerControl : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem flame;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip[] clips;

    IEnumerator coroutine;


    private VRTK_InteractableObject interactableObject;
    public bool isOn;
    private bool firstTimeOn;

    private void Awake()
    {
        flame.Stop();
        interactableObject = GetComponent<VRTK_InteractableObject>();

        interactableObject.InteractableObjectDisabled += WhenDisabled;
        interactableObject.InteractableObjectEnabled += WhenEnabled;
    }

    private void WhenEnabled(object sender, InteractableObjectEventArgs e)
    {
        interactableObject.InteractableObjectNearTouched += TouchObject;
    }

    private void WhenDisabled(object sender, InteractableObjectEventArgs e)
    {
        interactableObject.InteractableObjectNearTouched -= TouchObject;
    }

    private void TouchObject(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log(e.interactingObject.name + " touch object");
        VRTK_InteractGrab iGrab = e.interactingObject.GetComponent<VRTK.VRTK_InteractGrab>();
        if (iGrab.GetGrabbedObject() != null)
            return;


        isOn = !isOn;
        if (isOn)
        {
            flame.Play();
            coroutine = StartBurnerAudio();
            StartCoroutine(coroutine);
            if (!firstTimeOn)
            {
                FindObjectOfType<StepControl>().DoNextStep();
                firstTimeOn = true;
            }
        }

        else
        {
            flame.Stop();
            StopBurnerAudio();
        }

    }

    IEnumerator StartBurnerAudio()
    {
        audio.clip = clips[0];
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = clips[1];
        audio.Play();
        audio.loop = true;
    }

    void StopBurnerAudio()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        audio.loop = false;
        audio.clip = clips[2];
        audio.Play();
    }
}
