using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
// this class handles weapon actions and  stores damage and push force of upgraded weapons 

public class Weapon : Collidable
{
    // Damage
    public int[] damagePoint = {1,3,5,8};
    public float[] pushForce = {1.0f,1.2f,1.5f,1.6f};
    
    
    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer _spriteRenderer;
    
    //Combat 
    private Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing;
    
    //sound
    public AudioSource slashSoundEffects;

    
    // calls the base start method form the overriden method and set the animator to the animator component of the gamobjects 
    protected override void Start()
    {
        base.Start();
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // calls the base update method form the overriden method and check to see if the correct key is presed to swing the weapon and play the animaition
    protected override void Update()
    {
        base.Update();
        // know when we are able to swing 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > coolDown) // we dont want to swing every frame the update id being called so we a cool down timer to swing the weapon after the cool down timer 
            {
                slashSoundEffects.Play();
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    // checks collision to not do anything if the collider hits the player but would creeat a damage object to send to another collidable game object to damage them 
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            {
                return;
            }
            
            // create a new damage object, then we'll send it to the fighter we've hit
            Damage dmg = gameObject.AddComponent<Damage>();
            dmg.damageAmount = damagePoint[weaponLevel];
            dmg.origin = transform.position;
            dmg.pushForce = pushForce[weaponLevel];
            
            
            coll.SendMessage("ReceiveDamage",dmg);
            
            Debug.Log(coll.name);
        }
        
        
        
    }
    // plays the swing animation
    private void Swing()
    {
        //Debug.Log("Swing");
        anim.SetTrigger("Swing");
        
    }
    // when the weapon is upgrade uing the UI menu button this class is called to change weapon sprites and increment the weapon level.
    public void UpgradeWeapon()
    {
        weaponLevel++;
        _spriteRenderer.sprite = GameManager.instance.WeponSprites[weaponLevel];
        
        //change stats
    }
    // used to set the weapon level upon loading the new scene
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        _spriteRenderer.sprite = GameManager.instance.WeponSprites[weaponLevel];
    }
    
}
