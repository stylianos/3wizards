using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class CustomButton : MonoBehaviour, ISelectHandler// required interface when using the OnSelect method.
{
    public string m_level_name;
    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
        //((Button)GetComponent<Button>()).onClick.Invoke();
        load_level(m_level_name);
    }

    public void load_level(string level_name)
    {
        if (level_name == "exit")
        {
            Application.Quit();
            return;
        }
        if (level_name != null && level_name != "")
        {
            //StartCoroutine(change_level(level_name));
            Application.LoadLevel(level_name);
        }
    }
}