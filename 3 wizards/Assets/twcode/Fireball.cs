using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

    protected float m_speed = 5.0f;
    public float m_direction = 1.0f;
    public Wiz m_casting_wizard;
    protected float m_duration = 0.0f;
    public PizzaSpell m_pizza_spell;

    // Use this for initialization
    void Start()
    {
        m_pizza_spell = GameObject.Find("PizzaSpell").GetComponent<PizzaSpell>();
        if (m_direction < 0.0f)
        {
            transform.rotation = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        m_duration += Time.deltaTime;
        if (m_duration > 4.0f)
        {
            Destroy(gameObject, 0.1f);
        }
        float diff = Time.deltaTime * m_speed * m_direction;
        Vector2 pos = new Vector2(transform.position.x + diff, transform.position.y);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Wiz wiz = other.GetComponent<Wiz>();
        if (wiz == m_casting_wizard)
        {
            return;
        }
        if (wiz == null)
        {
            explode();
            return;
        }
        explode();
        wiz.set_dead();
    }

    void explode()
    {
        Vector2 pos_start2 = new Vector2(transform.position.x, transform.position.y);
        if (m_pizza_spell != null && m_pizza_spell.m_fireball_particle_end != null)
        {
            GameObject go2 = (GameObject)Object.Instantiate(m_pizza_spell.m_fireball_particle_end, transform.position, m_pizza_spell.m_fireball_particle_end.transform.rotation);
            DestroyObject(go2, 3.0f);
        }

        SoundController.Instance.PlaySoundOneShot(SoundController.Instance.m_flip_explosion);

        Destroy(gameObject, 0.01f);
    }
}
