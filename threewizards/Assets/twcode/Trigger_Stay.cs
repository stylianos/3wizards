using UnityEngine;
using System.Collections;

public class Trigger_Stay : MonoBehaviour {

    public int wizardsHere;
    public int wizardsNeeded;
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    public bool isActive;

    public bool isShootable;

    private SpriteRenderer sRender;
    public string m_level_name;
    
	// Use this for initialization
	void Start () {
        sRender = GetComponent<SpriteRenderer>();
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        Wiz wiz = other.GetComponent<Wiz>();

        Fireball fireball = other.GetComponent<Fireball>();
        if (fireball != null && isShootable)
        {
            isActive = true;
            return;
        }

        if (wiz == null)
        {
            return;
        }

        wizardsHere++;
        
        if (wizardsHere >= wizardsNeeded)
        {
            sRender.sprite = activeSprite;
            isActive = true;
            if (m_level_name != null && m_level_name != "")
            {
                ((PizzaSpell)GameObject.Find("PizzaSpell").GetComponent<PizzaSpell>()).change_level(m_level_name);
            }
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
            sRender.sprite = inactiveSprite;
            isActive = false;
        }

    }

}
