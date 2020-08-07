using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Video;
using VRTK;

public class VideoControl : MonoBehaviour
{

    [SerializeField] private VideoPlayer player;
    private IEnumerator coroutine;
    [SerializeField] private HeadsetFollower headsetFollower;

    public void OnOpenPlayer()
    {
        player.gameObject.SetActive(true);
        headsetFollower.DoFollowing(true);
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
        headsetFollower.DoFollowing(false);
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
        headsetFollower.DoFollowing(false);
        player.gameObject.SetActive(false);
        coroutine = null;
        FindObjectOfType<StepControl>().DoNextStep();
    }

}
