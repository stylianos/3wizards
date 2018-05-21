using UnityEngine;
using System.Collections;

public enum MovementMode { None, Once, Loop, PingPong };

[System.Serializable]
public class MovementWay {
	private float m_current = 0.0f;
	private float m_pause = 0.0f;

	[Tooltip("Where to start along the path [0, 1]")]
	public float m_start = 0.0f;

	[Tooltip("Movement speed")]
	public float m_speed = 1.0f;

	public float m_ping_pause = 0.0f;

	public float m_pong_pause = 0.0f;

	public MovementMode mode = MovementMode.Once;

	public void start()
	{
		m_current = m_start;
    }

	public float step()
	{
		if (mode == MovementMode.None)
		{
			return 0.0f;
		}
		float prev = m_current;
		if (m_pause != 0.0f)
		{
			m_pause -= Time.deltaTime;
			if (m_pause <= 0.0f)
			{
				m_current += m_speed * Mathf.Abs(m_pause);
				m_pause = 0.0f;
                return m_current - prev;
			}
			return 0.0f;
        }
		m_current += Time.deltaTime * m_speed;
		if (mode == MovementMode.Loop && m_current > 1.0f)
		{
			m_current -= 1.0f;
		}
		else if (mode == MovementMode.PingPong && m_current > 1.0f)
		{
			m_speed = Mathf.Abs(m_speed) * -1.0f;
			if (m_ping_pause > 0.0f)
			{
				float elapsed = m_current - 1.0f;
				m_pause = m_ping_pause - elapsed;
				m_current = 1.0f;
				if (m_pause < 0.0f)
				{
					m_current = 1.0f - m_pause;
					m_pause = 0.0f;
				}
            }
			else
			{
				m_current = 2.0f - m_current;
			}
		}
		else if (mode == MovementMode.PingPong && m_current < 0.0f)
		{
			m_speed = Mathf.Abs(m_speed);
			if (m_pong_pause > 0.0f)
			{
				//TODO_ANDI: speed vs. time
				float elapsed = Mathf.Abs(m_current);
				m_pause = m_ping_pause - elapsed;
				m_current = 0.0f;
				if (m_pause < 0.0f)
				{
					m_current = m_pause;
					m_pause = 0.0f;
				}
			}
			else
			{
				m_current = Mathf.Abs(m_current);
			}
		}
		m_current = Mathf.Clamp01(m_current);
		return m_current - prev;
	}

	public float Current() { return m_current; }

	public float get_current_with_offset(float i_offset)
	{
		if (mode == MovementMode.None)
		{
			return 0.0f;
		}
		float val = m_current + i_offset;
		if (mode == MovementMode.Loop && val > 1.0f)
		{
			val -= 1.0f;
		}
		else if (mode == MovementMode.PingPong && val > 1.0f)
		{
			val = 2.0f - val;
		}
		else if (mode == MovementMode.PingPong && val < 0.0f)
		{
			val = Mathf.Abs(val);
		}
		val = Mathf.Clamp01(val);
		return val;
	}
}
