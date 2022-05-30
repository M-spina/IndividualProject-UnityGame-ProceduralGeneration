using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Game manager handles a good majority of sub features in the game 
//and instances of game manager can be called to run certain methods required for class to perform their functions

public class GameManager : MonoBehaviour
{
// we first create a create a static GameManager instance so that other class can have acces to the same GameManger 
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null) // if a duplicate game object class exitst it will destroy it 
        {
            Destroy(gameObject);
            //
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            //
            return;
        }
        
        instance = this; // only one instance of game-manager is allowed to exist in the game  
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        //Add a delegate to this to get notifications when a Scene has loaded.
        // when the scene loads the SceneManager would also load any and all functions in the added event and extend them too.
        //DontDestroyOnLoad(gameObject); // when the game starts or change scene the game manager would remain
    }
    
    // resources for the game 
    public List<Sprite> PlayerSprites;
    public List<Sprite> WeponSprites;
    public List<int> WeponPrices;
    public List<int> expTable;

    private GameObject[] Players;
    private GameObject[] FloatingTexts;



    // References for classes
    public Player_Movement player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform healthBar;

    public Animator deathMenuAnime;
    public GameObject hud;
    public GameObject menu;
    //
    // logic 

    public int coins;
    public int exp;

   // private bool readyOrNot = true;
    // floating text
    public void ShowText(string msg, int fontSize, Color colour, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, colour, pos, motion, duration);
    }
    
    //SaveState 
    /* what we are going to save :
     *
     * int coins
     * int exp
     * int weapon level
     */
    // upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is weapon maxed  if not then we call the weapon upgrade method and returns a true bool 
        if (WeponPrices.Count <= weapon.weaponLevel)
            return false;
        if (coins >= WeponPrices[weapon.weaponLevel])
        {
            coins -= WeponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;

    }
    
    //Health Bar

    public void OnHealthChange()
    {
        float ratio = (float) player.hitpoint / (float) player.maxHitpoint;
        healthBar.localScale = new Vector3(1, ratio, 1);

    }
    
    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0; // return value which would be the players level
        int add = 0; // int that we would add to the sxp table 

        while (exp >= add) // as long as experience is grater than add we increase the player level
        {
            add += expTable[r]; // we add the first entry in the exp table at index r 
            r++;

            if (r == expTable.Count) // when player reaches max level
                return r;

        }

        return r; // return current level
    }
    public int GetExpToLevel(int level) // the total exp to reach a certain level 
    {
        int r = 0;
        int exp = 0;
        while (r < level)
        {
            exp += expTable[r]; // store the amount of exp for the next level
            r++;
        }

        return exp; // returns the amount
    }
    public void GrantExp(int xp) // when the player kills an enemy it grants exp and this is used to to add teh exp to that total exp and check if we can level up 
    { // and check if we can level up and if so we run the GmOnLevelUp method
        int currLevel = GetCurrentLevel();
        exp += xp;
        if (currLevel < GetCurrentLevel())
            GmOnLevelUp();

    }
    public void GmOnLevelUp() // This method calls the player  on level up method to level up the player 
    {
        
        Debug.Log("Level Up !");
        player.OnLevelUp();
    }
    // Scene Loaded, when a new sceen is loaded the player would spawn at the spawn point of the new sceen

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    // DeathMenu/ Respawn, when the player respawn we want to hid the death screen and load the start of the game to spawn the player
    public void Respawn()
    {
        deathMenuAnime.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        
        player.respawn();
        
        
        //player.respawn();

    }
     
    // Save/Load state
    public void SaveState()
    {
        string s = ""; // we will append all the data we want to save

        s += coins.ToString() + "|";
        s += exp.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        
        PlayerPrefs.SetString("saveState", s); // set a string of data to player pref with a key and value "string s"
        // Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        //readyOrNot = false;
        //
        SceneManager.sceneLoaded -= LoadState;
        //
        if (!PlayerPrefs.HasKey("saveState"))
        {
            return;
        }
        
        string[] data = PlayerPrefs.GetString("saveState").Split('|'); // retrieve that set string with the key 
        
        // changing values with what is in the data array 

        coins = int.Parse(data[0]);
        exp = int.Parse(data[1]);
        if (GetCurrentLevel() != 1)
           player.SetLevel(GetCurrentLevel());
        
        // change weapon level
        //weapon.weaponLevel = int.Parse(data[2]);
        weapon.SetWeaponLevel(int.Parse(data[2]));
        
        //Spawn the player at the spawnpoint gameobject when the game loads in.
        //player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        // if anything ggoes bad this is probs a problem
        
            
        //
        Debug.Log("LoadState");
        //readyOrNot = true;

    }
}
