using UnityEngine;
using System.Collections;

public class Trigger_Spell : MonoBehaviour
{
    public int wizardsHere;
    public int wizardsNeeded;
    public Sprite inactiveSprite;
    public Sprite activeSprite;

    private SpriteRenderer sRender;
    private bool enoughWizards;
    public bool isActive;

    // Use this for initialization
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Wiz wiz = other.GetComponent<Wiz>();
        if (wiz == null)
        {
            return;
        }

        wizardsHere++;

        if (wizardsHere >= wizardsNeeded)
        {
            //sRender.sprite = activeSprite;
            enoughWizards = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        Wiz wiz = other.GetComponent<Wiz>();
        if (wiz == null)
        {
            return;
        }

        wizardsHere--;

        if (wizardsHere < wizardsNeeded)
        {
            //sRender.sprite = inactiveSprite;
            enoughWizards = false;
        }

    }
    public void SpellCast ()
    {
        if (enoughWizards)
        {
            isActive = !isActive;
        }
        if (!isActive)
        {
            sRender.sprite = activeSprite;

        }
        else
        {
            sRender.sprite = inactiveSprite;
        }
    }
}
