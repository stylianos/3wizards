using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour, ITriggerableGameEvent
{
	public bool m_active = true;

	public void StartEvent(TriggerEventType i_type)
	{
		m_active = true;
	}
}
