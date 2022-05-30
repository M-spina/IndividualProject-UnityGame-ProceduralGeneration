using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

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

    

    protected override void Start()
    {
        base.Start();
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        // know when we are able to swing 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > coolDown)
            {
                slashSoundEffects.Play();
                lastSwing = Time.time;
                Swing();
            }
        }
    }

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

    private void Swing()
    {
        //Debug.Log("Swing");
        anim.SetTrigger("Swing");
        
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        _spriteRenderer.sprite = GameManager.instance.WeponSprites[weaponLevel];
        
        //change stats
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        _spriteRenderer.sprite = GameManager.instance.WeponSprites[weaponLevel];
    }
    
}
