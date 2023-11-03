using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// this class manages the Floating text objects
public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> _floatingTexts = new List<FloatingText>();

    private void Start()
    {
       // DontDestroyOnLoad(transform.parent.gameObject);
    }
    // we have an array of text that we can store and we want to cycle through them to display the when they are called
    private void Update()
    {
        foreach(FloatingText txt in _floatingTexts)// for every txt in the array we want to call this function
            txt.UpdateFloatingText();
        
    }
    // this method is used to create and show floating text given the parameter are correct
    public void Show(string msg, int fontsize, Color colour, Vector3 pos, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontsize;
        floatingText.txt.color = colour;
        // position is screen space not world space (not in game world)
        floatingText.txt.transform.position = Camera.main.WorldToScreenPoint(pos); // so we transfer the position form one space to another to use it in the UI
        floatingText.motion = motion;
        floatingText.duration = duration;
        
        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        
        FloatingText txt = _floatingTexts.Find(t => !t.active); // takes the text array and try to find one of the elements that is not active 

        if (txt == null) // if we dont find an inactive txt we need to create one so it wouldn't crash
        {
            // we are creating a new game-object and assigning to txt this is used to display the gameobject as a text in the game world
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();
            
            _floatingTexts.Add(txt);
        }

        return txt;
    }
    
}
