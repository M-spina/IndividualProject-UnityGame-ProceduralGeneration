using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;

    private bool hit = false;
    // Start is called before the first frame update
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && hit == false) // when the player's collider  is detected it will pick a random scene[level] form the array to load into the game.
        {
            hit = true;
            if (hit == true)
            {
                GameManager.instance.SaveState(); // saves the game state when we load a new scene 
                string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
                SceneManager.LoadScene(sceneName);
            }
            
            
        }

        hit = false;
    }
}
