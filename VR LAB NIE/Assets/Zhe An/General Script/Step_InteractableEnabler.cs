using UnityEngine;
using VRTK;
public class Step_InteractableEnabler : MonoBehaviour
{
    [SerializeField] private GloveInteraction glove;
    [SerializeField] private Spray_Bottle sprayBottle;
    [SerializeField] private VRTK_InteractableObject tableCloth;
    [SerializeField] private BunsenBurnerControl bunsenBurner;
    [SerializeField] private VRTK_InteractableObject strikingRod;
    [SerializeField] private CultureTube cultureTube;
    [SerializeField] private CultureContactWithInnoculationTube cultureContact;
    [SerializeField] private VRTK_InteractableObject petridish;
    [SerializeField] private PetridishCover petridishCover;
    [SerializeField] private GameObject petridishStrikePanel;
    [SerializeField] private BurnerFire burnerFire;

    private void Start()
    {
        DisableInteract();
    }

    public void EnableInteract()
    {
        if (glove)
        {
            glove.enabled = true;
        }
        if (sprayBottle)
        {
            sprayBottle.enabled = true;
        }
        if (tableCloth)
        {
            tableCloth.isGrabbable = true;
        }
        if (bunsenBurner)
        {
            bunsenBurner.enabled = true;
        }
        if (cultureContact)
        {
            cultureContact.enabled = true;
        }
        if (strikingRod)
        {
            strikingRod.isGrabbable = true;
        }
        if (cultureTube)
        {
            cultureTube.enabled = true;
        }
        if (petridish)
        {
            petridish.isGrabbable = true;
        }
        if (petridishCover)
        {
            petridishCover.enabled = true;
        }
        if (petridishStrikePanel)
        {
            petridishStrikePanel.SetActive(true);
        }
        if (burnerFire)
        {
            burnerFire.enabled = true;
        }
    }

    public void DisableInteract()
    {
        if (glove)
        {
            glove.enabled = false;
        }
        if (sprayBottle)
        {
            sprayBottle.enabled = false;
        }
        if (tableCloth)
        {
            tableCloth.isGrabbable = false;
        }
        if (bunsenBurner)
        {
            bunsenBurner.enabled = false;
        }
        if (strikingRod)
        {
            strikingRod.isGrabbable = false;
        }
        if (cultureContact)
        {
            cultureContact.enabled = false;
        }
        if (cultureTube)
        {
            cultureTube.enabled = false;
        }
        if (petridish)
        {
            petridish.isGrabbable = false;
        }
        if (petridishCover)
        {
            petridishCover.enabled = false;
        }
        if (petridishStrikePanel)
        {
            petridishStrikePanel.SetActive(false);
        }
        if (burnerFire)
        {
            burnerFire.enabled = false;
        }
    }

}
