using UnityEngine;


public class PetridishCover : MonoBehaviour
{
    //for dev purpose: delete after done
    //[SerializeField] private Text text;
    [SerializeField] private GameObject petridishPanel;
    private bool triggerOnStep;
    private void OnEnable()
    {
        triggerOnStep = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "strike rod" && triggerOnStep)
        {

            //set petridish panel on with step control
            Debug.Log("trigger on petridish cover");
            FindObjectOfType<StepControl>().DoNextStep();
            triggerOnStep = false;
        } 
    }



}
