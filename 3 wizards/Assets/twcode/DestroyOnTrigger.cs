using UnityEngine;
using System.Collections;

public class DestroyOnTrigger : MonoBehaviour {

    public GameObject watchedTrigger;

   
	// Update is called once per frame
	void Update () {
        if (watchedTrigger.GetComponent<Trigger_Stay>() != null)
        {
            if (((Trigger_Stay)watchedTrigger.GetComponent<Trigger_Stay>()).isActive)
            {
                Destroy(this.gameObject);
            }
        }
        else if (watchedTrigger.GetComponent<Trigger_Toggle>() != null)
        {
            if (((Trigger_Toggle)watchedTrigger.GetComponent<Trigger_Toggle>()).isActive)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
