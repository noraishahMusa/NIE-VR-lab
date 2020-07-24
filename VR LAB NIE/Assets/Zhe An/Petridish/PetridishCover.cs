using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PetridishCover : MonoBehaviour
{

    Renderer rend;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInParent<Renderer>();
        originalColor = rend.material.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "strike rod" && this.name == "all")
        {
            Debug.Log("enter wrong area");
            ChangeColor(Color.blue);
        } 
        if(other.name == "strike rod" && this.name == "strike 1")
        {
            Vector3 direction = other.transform.position - transform.position;
            if (Vector3.Dot(transform.forward, direction) > 0)
            {
                print("exit at the back of the correct area");
                ChangeColor(Color.green);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.name == "strike rod")
        //{
        //    Debug.Log("strike rod is in petridish");
        //    ChangeColor(Color.red);

        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "strike rod" && this.name == "strike 1")
        {
            Debug.Log("strike rod out of petridish");
            //ChangeColor(originalColor);

            Vector3 direction = other.transform.position - transform.position;
            if (Vector3.Dot(transform.forward, direction) > 0)
            {
                print("exit at back of strike 1");
                ChangeColor(originalColor);
            } else
            {
                Debug.Log("exit from strike 1 at the wrong area.");
                ChangeColor(Color.black);
            }
            //if (Vector3.Dot(transform.forward, direction) < 0)
            //{
            //    print("exit at Front");
            //}
            //if (Vector3.Dot(transform.forward, direction) == 0)
            //{
            //    print("exit at Side");
            //}
        } else
        {
            ChangeColor(Color.cyan);
        }

        Invoke("ChangeBackAfterAwhile", 2);
    }

    private void ChangeBackAfterAwhile()
    {
        ChangeColor(originalColor);
    }

    private void ChangeColor(Color c)
    {
        rend.material.color = c;
    }


}
