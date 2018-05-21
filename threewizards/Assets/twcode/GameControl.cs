using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

public class GameControl : MonoBehaviour
{

    //public static GameControl control;
    public bool is_paused = false; 
    public string levels_meta_file = "/levelsmeta.xml";

    /*void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }*/

    public void new_level()
    {
        load_level("level00");
    }

    public void load_level(string level_name)
    {
        //StartCoroutine(change_level(level_name));
        Application.LoadLevel(level_name);
    }

    public static void LoadLevel(string level_name)
    {
        //StartCoroutine(change_level(level_name));
        Application.LoadLevel(level_name);
    }

    /*IEnumerator change_level(string i_level)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject go = (GameObject)GameObject.Find("EventSystem");
        if (go != null)
        {
            Destroy(go);
        }
        Application.LoadLevel(i_level);
    }*/

    public void exit()
    {
        //Debug.Log("I am clicked!");
        Application.Quit();

    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            load_level("main_menu");
        }
    }
}
