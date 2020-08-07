using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike_SectorControl : MonoBehaviour
{
    [SerializeField] private GameObject[] sectors;
    [SerializeField] private GameObject sideSpot;
    List<GameObject> sequenceOfSectors = new List<GameObject>();
    static int currSequence;
    public static GameObject currSector;
    public static bool isSideSpot;


    private void OnEnable()
    {
        CreateSequence();
    }

    private void CreateSequence()
    {
        sequenceOfSectors.Add(sectors[1]);
        sequenceOfSectors.Add(sectors[0]);
        sequenceOfSectors.Add(sectors[2]);
        sequenceOfSectors.Add(sectors[0]);
        sequenceOfSectors.Add(sectors[3]);
        sequenceOfSectors.Add(sectors[0]);
        sequenceOfSectors.Add(sectors[4]);
        sequenceOfSectors.Add(sectors[5]);
    }

    public void DoNextSequence()
    {
        
        for(int i = 0; i < sequenceOfSectors.Count; i++)
        {
            if (i == currSequence)
            {
                currSector = sequenceOfSectors[i];
                sequenceOfSectors[i].SetActive(true);
            }
            else sequenceOfSectors[i].SetActive(false);
        }
        if (currSector == sideSpot) isSideSpot = true;
        else isSideSpot = false;
        Debug.Log(isSideSpot + " for side spot");
        currSequence++;
    }





}
