using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void StartLevel1()
    {
        SceneManager.LoadScene("MyScene");
    
    }
	
	public void StartLevel2()
    {
        SceneManager.LoadScene("level2");
    
    }

    public void QuitGame()
    {

        Application.Quit();

    }

    


}
