using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    // Character Selection
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


    //Weapon Upgrade

    public void OnUpgradeClick()
    {
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }
    
    // update Character info 
    public void UpdateMenu()
    {
        // wep
        weaponSprite.sprite = GameManager.instance.WeponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.WeponPrices.Count)
            UpgardeCostText.text = "MAX";
        else
        {
            UpgardeCostText.text = GameManager.instance.WeponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
            
    
        //meta
        LevelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitPointText.text = GameManager.instance.player.hitpoint.ToString() + " / " + GameManager.instance.player.maxHitpoint.ToString();
        CoinText.text = GameManager.instance.coins.ToString();
        
        // exp Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
          // check if max level
        if (currentLevel == GameManager.instance.expTable.Count)
        {
            ExpText.text = GameManager.instance.exp.ToString() + " total exp points"; // display exp
            expBar.localScale = Vector3.one;
        }
        else
        {
            // getting the ratio of what we have and what we have to reach 
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
