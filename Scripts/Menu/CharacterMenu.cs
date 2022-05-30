using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// this class handles UI elements in the game 

public class CharacterMenu : MonoBehaviour
{
    //Text field 
    public Text LevelText, hitPointText, CoinText, UpgardeCostText, ExpText;
    
    // Logic for  sprites -- might add changing character sprites to the final game

    private int currentCharacterSelction = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform expBar;

    private static CharacterMenu instance;
    
    
    private void Awake()
    {
        if (CharacterMenu.instance != null) // if a duplicate game object class exitst it will destroy it 
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this; // only one instance of game-manager is allowed to exist in the game  
        //Add a delegate to this to get notifications when a Scene has loaded.
        // when the scene loads the SceneManager would also load any and all functions in the added event and extend them too.
        //DontDestroyOnLoad(gameObject); // when the game starts or change scene the game manager would remain
    }
    
    // Character Selection: when we click a button in the menu we swap out the current player sprite to another sprite in the list of Player sprites 
    // this method only work with the sprite displayed on the menu and not attually chages the sprite in game didnt have time to create walk animatin for the new sprite loaded in.
    public void OnButtonClick(bool right)
    {
        if (right)
        {
            currentCharacterSelction++;
            if (currentCharacterSelction == GameManager.instance.PlayerSprites.Count)
                currentCharacterSelction = 0;
            OnSelectionChange();
        }
        else
        {
            currentCharacterSelction--;

            if (currentCharacterSelction < 0)
                currentCharacterSelction = GameManager.instance.PlayerSprites.Count - 1;

            OnSelectionChange();

        }
    }
    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.PlayerSprites[currentCharacterSelction];
        
    }


    //Weapon Upgrade: when a button that calls this method is click it will call the TryUpgradeWeapon() method form th game manager and and then update the menu to refect the change 

    public void OnUpgradeClick()
    {
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }
    
    // update Character info : this updates all the varibles in the menu and check if certain methods should stop being called.
    public void UpdateMenu()
    {
        // wep
        weaponSprite.sprite = GameManager.instance.WeponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.WeponPrices.Count) // if the current weapon is the last in the array of weapon sprites it is the max weapon and text is displayed to show that there are no more upgades
            UpgardeCostText.text = "MAX";
        else // else if the weapon is not the max level it displays the pirce for the next level
        {
            UpgardeCostText.text = GameManager.instance.WeponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
            
    
        //meta: this is use to display variable we want to show in the UI menu as text like health, exp  and coins 
        LevelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitPointText.text = GameManager.instance.player.hitpoint.ToString() + " / " + GameManager.instance.player.maxHitpoint.ToString();
        CoinText.text = GameManager.instance.coins.ToString();
        
        // exp Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
          // check if max level. if the player is at max level it will display the total amount to exp gained.
        if (currentLevel == GameManager.instance.expTable.Count)
        {
            ExpText.text = GameManager.instance.exp.ToString() + " total exp points"; // display exp
            expBar.localScale = Vector3.one;
        }
        else
        {
            // getting the ratio of what we have and what we have to reach this will change the exp bar to show that the player is making progress
            int prevLevelExp = GameManager.instance.GetExpToLevel(currentLevel - 1);
            int currLevelExp = GameManager.instance.GetExpToLevel(currentLevel);

            int diff = currLevelExp - prevLevelExp;
            int currExpInToLevel = GameManager.instance.exp - prevLevelExp;

            float completionRatio = (float)currExpInToLevel / (float)diff;
            expBar.localScale = new Vector3(completionRatio, 1, 1);
            ExpText.text = currExpInToLevel.ToString() + " / " + diff;
        }
        
        //ExpText.text = "NOT IMPLEMENTED";
        //expBar.localScale = new Vector3(0.5f, 0, 0);
    }
}
