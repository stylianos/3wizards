using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {


    public bool enemy_dead = false;
    public GameObject enemy_prefab;
    public GameObject current_enemy; 
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
       if (current_enemy == null )
        {
            //Debug.Log("The enemy is dead!!!");
            current_enemy = (GameObject)Instantiate(enemy_prefab, this.transform.position,Quaternion.identity);
            
        }
	}
}
