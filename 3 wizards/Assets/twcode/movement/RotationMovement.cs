using UnityEngine;
using System.Collections;
using System;

public class RotationMovement : BaseMovement
{
	public float m_max_rotation = 0.0f;
	public Vector3 m_rotation_centre = new Vector3(0, 0, 0);
	public MovementWay m_way = new MovementWay();

	//private Vector3 m_start_position;

	// Use this for initialization
	void Start ()
	{
		//m_start_position = transform.TransformPoint(m_rotation_centre);
    }
	
	void FixedUpdate()
	{
		if (!m_active) { return; }
		float diff = m_way.step();
		if (diff == 0.0f)
		{
			return;
		}
		float rot = m_max_rotation == 0.0f ? 360.0f : m_max_rotation;
        float amount = diff * rot;
		Vector3 v = transform.TransformPoint(m_rotation_centre); // m_start_position;
		transform.RotateAround(v, new Vector3(0, 0, 1), amount);
    }

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.TransformPoint(m_rotation_centre));
	}
}
