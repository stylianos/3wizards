using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float m_move_speed = 5.0f;
    protected float m_dir = 1.0f;
    protected Rigidbody2D m_rb2d;

    // Use this for initialization
    void Start () {
        m_rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        Vector2 pos = m_rb2d.position;
        float diff = m_dir * m_move_speed * Time.deltaTime;
        Vector2 new_pos = new Vector2(pos.x + diff, pos.y);
        m_rb2d.position = new_pos;
    }

    void switch_dir(float i_dir)
    {
        m_dir = i_dir;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * m_dir, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "monster_wall")
        {
            switch_dir(-m_dir);
        }

        Fireball fire = col.gameObject.GetComponent<Fireball>();
        if (fire)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {


        Enemy deadly = col.gameObject.GetComponent<Enemy>();
        if (deadly == null)
        {
            return;
        }
        float diff = deadly.transform.position.x - transform.transform.position.x;
        switch_dir(diff < 0.0f ? 1.0f : -1.0f);

       
    }
}
