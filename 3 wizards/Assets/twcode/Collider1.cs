using UnityEngine;
using System.Collections;

public class Collider1 : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Wiz wiz = other.GetComponent<Wiz>();
        if (wiz == null)
        {
            return;
        }

        Debug.Log(other.gameObject.name);

    }
}


