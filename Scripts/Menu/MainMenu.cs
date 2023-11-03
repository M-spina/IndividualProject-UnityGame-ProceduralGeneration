using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// this creates the main menu when the game first load where the will display two button and would call form one of two methods 

public class MainMenu : MonoBehaviour
{
    public void PlayGame() // this load the next scene in the buil index that woul be the sarting room to play the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuiteGame() // close the game in the build mode of the game , if ran in edior mode it will on display quite in the console.
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
