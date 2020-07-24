using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Video;
using VRTK;

public class VideoFollower : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    Transform headsetTran;
    bool isVideoOpen;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    public float CameraDistance = 3.0F;
    private IEnumerator coroutine;

    private void Awake()
    {
        StartCoroutine(FindHeadset());

    }

    IEnumerator FindHeadset()
    {
        while(headsetTran == null)
        {
            headsetTran = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
            yield return null;
        }
        Debug.Log(headsetTran);
    }
    
    public void OnOpenPlayer()
    {
        player.gameObject.SetActive(true);
        isVideoOpen = true;
        player.Play();
        coroutine = WhenVideoEnd();
        StartCoroutine(coroutine);
    }

    public void ForceStopPlayer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        player.Stop();
        isVideoOpen = false;
        player.gameObject.SetActive(false);
    }

    IEnumerator WhenVideoEnd()
    {
        yield return new WaitForSeconds((float)player.length);
        while (player.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        isVideoOpen = false;
        player.gameObject.SetActive(false);
        coroutine = null;
    }

    private void Update()
    {
        if (isVideoOpen && headsetTran != null)
        {
            Vector3 targetPosition = headsetTran.TransformPoint(new Vector3(0, 0, CameraDistance));

            // Smoothly move my object towards that position ->
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // version 1: my object's rotation is always facing to camera with no dampening  ->
           // transform.LookAt(transform.position + Camera2Follow.transform.rotation * Vector3.forward, Camera2Follow.transform.rotation * Vector3.up);

            // version 2 : my object's rotation isn't finished synchronously with the position smooth.damp ->
            transform.rotation = Quaternion.RotateTowards(transform.rotation, headsetTran.rotation, 35 * Time.deltaTime);
        }
    }
        
}
