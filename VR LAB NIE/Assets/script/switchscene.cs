using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchscene : MonoBehaviour
{
   public void playGame1() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
               
    }
    public void playGame2()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }
    public void playGame3()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);

    }
    public void back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}
