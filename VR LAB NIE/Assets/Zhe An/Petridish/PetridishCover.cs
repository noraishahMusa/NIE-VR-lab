using UnityEngine;


public class PetridishCover : MonoBehaviour
{
    //for dev purpose: delete after done
    //[SerializeField] private Text text;
    [SerializeField] private GameObject petridishPanel;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "strike rod")
        {

            //set petridish panel on with step control
            FindObjectOfType<StepControl>().DoNextStep();
        } 
    }



}
