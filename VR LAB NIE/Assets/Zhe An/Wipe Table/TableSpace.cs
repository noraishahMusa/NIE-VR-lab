using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableSpace : MonoBehaviour
{
    RawImage img;
    Texture2D t;
    Collider coll;
    RectTransform rect;

    //for calculating cloth wipe area
    Vector3 collMax;
    Vector3 collMin;
    int clothWidth = 10;
    int clothHeight = 36;
    int clothArea;
    Vector2 boundsToTextRatio;

    [SerializeField]
    private Transform tissueTarget;

    private void Awake()
    {
        rect = GetComponentInParent<RectTransform>();
        coll = GetComponent<Collider>();
        img = GetComponent<RawImage>();
        t = new Texture2D(128, 128, TextureFormat.RGB24,true);
        img.texture = t;
        Debug.Log(t.height + " " + t.width);
        for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {

                Color color = Color.black;
                t.SetPixel(x, y, color);
            }
        }
        t.Apply();


        //For preparing cloth wipe action value
        InitiaiteClothCalculation();

    }

    bool hasZoneNotCovered;
    private void CheckAlpha()
    {
        float cleanedCount = 0;
        hasZoneNotCovered = false;
        for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {
                if(t.GetPixel(x,y) != Color.black)
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
            float percent = (cleanedCount / (t.width * t.height)) * 100;
            Debug.Log(percent + " has been covered");
            if(percent > 90)
            {
                Debug.Log(percent + " is greater than 90. Hence table is cleaned.");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CheckAlpha();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == tissueTarget.name)
        {
            ChangeTextureColor(other);
        }
    }



    void InitiaiteClothCalculation()
    {
        collMax = coll.bounds.max;
        collMin = coll.bounds.min;
        Debug.Log(collMax + " " + collMin);
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
            colors[i] = Color.white;
        }

        t.SetPixels((int)final.x - clothWidth/2, (int)final.y - clothHeight/2, clothWidth, clothHeight, colors);
        t.Apply();
    }


}
