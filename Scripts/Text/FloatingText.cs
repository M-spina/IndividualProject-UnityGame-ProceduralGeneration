using UnityEngine;
using UnityEngine.UI;

public class FloatingText 
{
    public bool active;
    public GameObject go; // reference for the game object 
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }
    
    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
            return;
        
        if(Time.time - lastShown > duration) // if the current time in that game minus the moment we show the text , is grater than the duration we hide the text
            Hide();

        go.transform.position += motion * Time.deltaTime; // every frame we are showing the text we would move the text by the motion (which is a vector)

    }
}
