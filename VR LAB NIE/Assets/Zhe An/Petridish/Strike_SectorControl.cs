using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike_SectorControl : MonoBehaviour
{
    [SerializeField] private GameObject[] sectors;
    [SerializeField] private GameObject sideSpot;
    [SerializeField] private GameObject lastSector;
    List<GameObject> sequenceOfSectors = new List<GameObject>();
    static int currSequence;
    public static GameObject currSector;
    public static bool isSideSpot;
    public static bool isLastSector;


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
        for (int i = 0; i < sequenceOfSectors.Count; i++)
        {
            sequenceOfSectors[i].SetActive(false);
        }
        sequenceOfSectors[currSequence].SetActive(true);
        currSector = sequenceOfSectors[currSequence];
        currSequence++;
        if (currSector == sideSpot) isSideSpot = true;
        else isSideSpot = false;
        if (currSector == lastSector) isLastSector = true;
        else isLastSector = false;

    }





}
