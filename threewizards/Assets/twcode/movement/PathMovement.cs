using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PathMovementAnchor
{
	public float m_offset;
	public GameObject m_target;
	public int m_duplicate_amount = 0;
	public float m_duplicate_distance = 0.0f;

	protected Vector3 m_pos_offset;

	public Vector3 pos_offset
	{
		get
		{
			return m_pos_offset;
		}
		set
		{
			m_pos_offset = value;
		}
	}
}

[System.Serializable]
public class PathMovement : BaseMovement {

	public Vector3[]				m_points;
	public List<PathMovementAnchor> m_items;
	public MovementWay				m_way = new MovementWay();

	public float get_total_length_local()
	{
		float total_lengt = 0.0f;
		for (int i = 1; i < m_points.Length; ++i)
		{
			total_lengt += (m_points[i] - m_points[i - 1]).magnitude;
		}
		if (m_way.mode == MovementMode.Loop && m_points.Length > 1)
		{
			total_lengt += (m_points[0] - m_points[m_points.Length - 1]).magnitude;
		}
		return total_lengt;
	}

	public Vector3 get_pos_on_path_local(float i_pos)
	{
		i_pos = Mathf.Clamp01(i_pos);
		float total = get_total_length_local();
		float p = i_pos * total;
		for (int i = 1; i < m_points.Length; ++i)
		{
			float length = (m_points[i] - m_points[i - 1]).magnitude;
			if (p <= length + 0.001f)
			{
				return Vector3.Lerp(m_points[i - 1], m_points[i], length == 0.0f ? 0.0f : p / length);
            }
			p -= length;
		}
		if (m_way.mode == MovementMode.Loop && m_points.Length > 1)
		{
			float length = (m_points[0] - m_points[m_points.Length - 1]).magnitude;
			if (p <= length + 0.001f)
			{
				return Vector3.Lerp(m_points[m_points.Length - 1], m_points[0], length == 0.0f ? 0.0f : p / length);
			}
		}
		return Vector3.zero;
	}

	// Use this for initialization
	void Start ()
	{
		m_way.start();
		foreach (PathMovementAnchor anchor in m_items)
		{
			if (anchor.m_target == null) { continue; }
			Vector3 on_path = get_pos_on_path_local(anchor.m_offset);
			Vector3 local_pos = transform.InverseTransformPoint(anchor.m_target.transform.position);
			anchor.pos_offset = local_pos - on_path;


        }
		int num_items = m_items.Count;
        for (int idx = 0; idx < num_items; ++idx)
		{
			PathMovementAnchor source = m_items[idx];
			if (source.m_target == null) { continue; }
			for (int i = 0; i < source.m_duplicate_amount; ++i)
			{
				PathMovementAnchor copy = new PathMovementAnchor();
				copy.m_offset = (source.m_offset + (float)(i + 1) * source.m_duplicate_distance) % 1.0f;
				copy.pos_offset = source.pos_offset;
				Vector3 on_path = get_pos_on_path_local(copy.m_offset);
				copy.m_target = (GameObject)Object.Instantiate(source.m_target, transform.TransformPoint(on_path), source.m_target.transform.rotation);
				m_items.Add(copy);
            }
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!m_active) { return; }
		m_way.step();

		float total_lengt = get_total_length_local();

		foreach (PathMovementAnchor anchor in m_items)
		{
			if (anchor.m_target == null) { continue; }
			float current = m_way.get_current_with_offset(anchor.m_offset);
            Vector3 on_path = get_pos_on_path_local(current);
			anchor.m_target.transform.position = transform.TransformPoint(on_path + anchor.pos_offset);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		for (int i = 1; i < m_points.Length; ++i)
		{
			Vector3 a = transform.TransformPoint(m_points[i - 1]);
			Vector3 b = transform.TransformPoint(m_points[i]);
			Gizmos.DrawLine(a, b);
		}

		if (m_way.mode == MovementMode.Loop && m_points.Length > 1)
		{
			Vector3 a = transform.TransformPoint(m_points[0]);
			Vector3 b = transform.TransformPoint(m_points[m_points.Length - 1]);
			Gizmos.color = Color.grey;
			Gizmos.DrawLine(a, b);
		}

		Gizmos.color = Color.gray;
		foreach (PathMovementAnchor anchor in m_items)
		{
			if (anchor.m_target == null) { continue; }
			Vector3 v = get_pos_on_path_local(anchor.m_offset);
			Gizmos.DrawLine(transform.TransformPoint(v), anchor.m_target.transform.position);
		}
	}
}
