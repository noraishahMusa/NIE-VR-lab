using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRTK;
public class CultureTube : MonoBehaviour
{
    [SerializeField] private Animator capAnim;
    [SerializeField] private VideoFollower videoFollower;
    VRTK_InteractableObject interactableObject;
    bool waitToOpenCap;
    int timeToWaitToOpenCap = 3;
    bool isCapOpen;
    bool playVideoForFirst;
    IEnumerator coroutine;

    private void Awake()
    {
        interactableObject = GetComponent<VRTK_InteractableObject>();
        interactableObject.InteractableObjectGrabbed += ObjectGrab;
        interactableObject.InteractableObjectUngrabbed += ObjectUngrab;
        capAnim.speed = 0;
    }

    private void ObjectUngrab(object sender, InteractableObjectEventArgs e)
    {
        capAnim.Play("closeCap");
        capAnim.speed = 1;
    }



    private void ObjectGrab(object sender, InteractableObjectEventArgs e)
    {
        capAnim.Play("openCap");
        capAnim.speed = 1;
        if (!playVideoForFirst)
        {
            videoFollower.OnOpenPlayer();
            playVideoForFirst = true;
        }
    }

}
