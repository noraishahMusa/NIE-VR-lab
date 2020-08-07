using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Video;
using VRTK;
using VRTK.Examples.Archery;

public class HeadsetFollower : MonoBehaviour
{
    [SerializeField] private bool followOnAwake;
    Transform headsetTran;
    bool isVideoOpen;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    public float CameraDistance = 3.0F;

    private void Awake()
    {
        StartCoroutine(FindHeadset());
    }

    public void DoFollowing(bool value)
    {
        isVideoOpen = value;
    }

    IEnumerator FindHeadset()
    {
        while (headsetTran == null)
        {
            headsetTran = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
            yield return null;
        }
        Debug.Log(headsetTran);
        if (followOnAwake)
        {
            DoFollowing(true);
        }
    }

    private void Update()
    {
        if (isVideoOpen && headsetTran != null)
        {
            Vector3 targetPosition = headsetTran.TransformPoint(new Vector3(0, 0, CameraDistance));

            // Smoothly move my object towards that position ->
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // version 1: my object's rotation is always facing to camera with no dampening  ->
             transform.LookAt(transform.position + headsetTran.transform.rotation * Vector3.forward, headsetTran.transform.rotation * Vector3.up);

            // version 2 : my object's rotation isn't finished synchronously with the position smooth.damp ->
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, headsetTran.rotation, 35 * Time.deltaTime);
        }
    }

}
