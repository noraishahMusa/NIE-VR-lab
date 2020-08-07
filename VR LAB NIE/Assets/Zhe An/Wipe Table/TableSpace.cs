using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableSpace : MonoBehaviour
{
    RawImage img;
    Texture2D t;
    [SerializeField]Texture2D dirtyTexture;
    Collider coll;
    RectTransform rect;
    Color clear;

    //for calculating cloth wipe area
    Vector3 collMax;
    Vector3 collMin;
    int clothWidth = 20;
    int clothHeight = 56;
    int clothArea;
    int pixelAmt;
    Vector2 boundsToTextRatio;

    [SerializeField]
    private Transform tissueTarget;

    private void Awake()
    {
        rect = GetComponentInParent<RectTransform>();
        coll = GetComponent<Collider>();
        img = GetComponent<RawImage>();
        clear = new Color(0, 0, 0, 0);
        t = new Texture2D(dirtyTexture.width, dirtyTexture.height, TextureFormat.ARGB32, true);
        for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {
                t.SetPixel(x, y, dirtyTexture.GetPixel(x,y));
            }
        }
        t.Apply();
        img.texture = t;
        pixelAmt = (int)(t.width / 20) * (int)(t.height / 20);
        Debug.Log(pixelAmt + " " + t.width + " " + t.height);
        //For preparing cloth wipe action value
        InitiaiteClothCalculation();

    }

    bool hasZoneNotCovered;
    private void CheckAlpha()
    {
        float cleanedCount = 0;
        hasZoneNotCovered = false;
        for (int y = 0; y < t.height; y+=20)
        {
            for (int x = 0; x < t.width; x+=20)
            {
                if(t.GetPixel(x,y) == clear)
                {
                    cleanedCount++;
                    Debug.Log("area cleaned");
                    hasZoneNotCovered = true;
                }
            }
        }
        if (!hasZoneNotCovered)
        {
            Debug.Log("All is black");
        } else
        {
            float percent = (cleanedCount / pixelAmt) * 100;
            Debug.Log(percent + " has been covered");
            if(percent > 45)
            {
                Debug.Log(percent + " is greater than 50. Hence table is cleaned.");
                //GoToNextStepUsingStepControl//
                FindObjectOfType<StepControl>().DoNextStep();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.name == tissueTarget.name)
        {
            ChangeTextureColor(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == tissueTarget.name)
        {
            CheckAlpha();
        }
    }



    void InitiaiteClothCalculation()
    {
        collMax = coll.bounds.max;
        collMin = coll.bounds.min;
        Vector2 v = new Vector2(collMax.x - collMin.x, collMax.z - collMin.z);
        boundsToTextRatio = new Vector2(t.width/v.x, t.height/v.y);
        clothArea = clothWidth * clothHeight;
    }

    void ChangeTextureColor(Collider _other)
    {
        Vector3 c = coll.bounds.ClosestPoint(_other.transform.position);
        Vector2 cFromMin = new Vector2(c.x - collMin.x, c.z - collMin.z);
        Vector2 final = new Vector2(boundsToTextRatio.x * cFromMin.x, boundsToTextRatio.y * cFromMin.y);


        Color[] colors = new Color[clothWidth*clothHeight];
        for(int i = 0; i < colors.Length; i++)
        {
            colors[i] = clear;
        }

        t.SetPixels((int)final.x - clothWidth/2, (int)final.y - clothHeight/2, clothWidth, clothHeight, colors);
        t.Apply();
        img.texture = t;
    }


}
