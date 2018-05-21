using UnityEngine;
using System.Collections;

public class LinearMovement : BaseMovement {

	public Vector3 m_point_b;
	public MovementWay m_way = new MovementWay();

	private Vector3 m_start_position;
	private Vector3 m_point_a = Vector3.zero;

	// Use this for initialization
	void Start () {
		m_start_position = transform.localPosition;
		m_way.start();
    }
	
	void FixedUpdate() {
		if (!m_active) { return; }
		m_way.step();

        Vector3 final = m_start_position + (m_point_a + (m_point_b - m_point_a) * m_way.Current());
		this.transform.localPosition = final;
    }


	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.localPosition + m_point_a, transform.localPosition + m_point_a);
    }
}
