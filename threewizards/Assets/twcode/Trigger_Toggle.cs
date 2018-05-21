using UnityEngine;
using System.Collections;

public class Trigger_Toggle: MonoBehaviour {

    public Sprite inactiveSprite;
    public Sprite activeSprite;

    private SpriteRenderer sRender;
    public bool isActive;

    // Use this for initialization
    void Start () {
        sRender = GetComponent<SpriteRenderer>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Wiz wiz = other.GetComponent<Wiz>();
        if (wiz == null)
        {
            return;
        }

        if (!isActive)
        {
            isActive = true;
            sRender.sprite = activeSprite;

        }
        else
        {
            isActive = false;
            sRender.sprite = inactiveSprite;
        }

    }

}
