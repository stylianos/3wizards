using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public enum TriggerEventType
{
	startMovement,
	stopMovement,
	changeColor,
	disable,
	enable
}

public class TriggerEvent : MonoBehaviour
{

	public GameObject[] m_targets;
	public TriggerEventType m_type = TriggerEventType.startMovement;
	public UnityEvent m_trigger_event;

	void OnTriggerEnter2D(Collider2D other)
	{
        Wiz a = other.GetComponent<Wiz>();
		if (a == null)
		{
			return;
		}
		if (m_targets != null)
		{
			foreach (GameObject g in m_targets)
			{
				g.GetComponent<ITriggerableGameEvent>().StartEvent(m_type);
			}
		}
		if (m_trigger_event != null)
		{
			m_trigger_event.Invoke();
		}
    }
}
