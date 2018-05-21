using UnityEngine;
using System.Collections;

public class Wiz : MonoBehaviour {

    public string m_horizontal_axis = "Horizontal";
    public string m_spell_button = "Spell";
	public int m_wizard_num = 0;
	public PizzaSpell m_pizza_spell;
    public bool m_is_chicken = false;
    public Animator anim; 
    protected float m_move_speed = 5.0f;
    protected float m_dir = 0.0f;
    public float m_look_dir = 1.0f;
    protected Rigidbody2D m_rb2d;
	protected bool m_spell_held = false;
	protected bool m_spell = false;
    protected Vector3 m_default_scale;
    protected float m_dash_force = 12.0f;

    protected bool m_gravity_rot_on = false;
    protected float m_rot_for_gravity = 0.0f;
    protected float m_ghost_time = 0.0f;
    protected float m_max_ghost_time = 5.0f;

    public AudioSource m_chicken_run = null;

    // Use this for initialization
    public void Start () {
        anim = GetComponent<Animator>();
        m_rb2d = GetComponent<Rigidbody2D>();
        m_default_scale = transform.localScale;
        m_rb2d.centerOfMass = new Vector2(0.0f, -1.0f);
        m_rb2d.freezeRotation = true;
        if (m_pizza_spell == null)
        {
            m_pizza_spell = GameObject.Find("PizzaSpell").GetComponent<PizzaSpell>();
        }
    }
	
	// Update is called once per frame
	public void Update () {
        m_dir = Input.GetAxis(m_horizontal_axis);
    }

    public void FixedUpdate()
    {
        if (m_ghost_time > 0.0f)
        {
            m_ghost_time = Mathf.Max(m_ghost_time - Time.deltaTime, 0.0f);
            if (m_ghost_time <= 0.0f)
            {
                if (m_wizard_num == 1)
                {
                    SoundController.Instance.PlaySoundOneShot(SoundController.Instance.m_spell_dust);
                }
                set_ghost(false);
            }
        }
        bool is_wrong = m_rb2d.gravityScale < 0.0f;
        if (m_gravity_rot_on)
        {
           
            m_rot_for_gravity += Time.deltaTime * 300.0f * (is_wrong ? 1.0f : -1.0f);
            if (is_wrong && m_rot_for_gravity > 180.0f)
            {
                m_rot_for_gravity = 180.0f;
                m_gravity_rot_on = false;
            }
            else if (!is_wrong && m_rot_for_gravity < 0.0f)
            {
                m_rot_for_gravity = 0.0f;
                m_gravity_rot_on = false;
            }
            m_rb2d.rotation = m_rot_for_gravity;
        }
        if (m_dir < -0.001f)
        {
            m_look_dir = -1.0f;
        }
        else if (m_dir > 0.001f)
        {
            m_look_dir = 1.0f;
        }
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (is_wrong ? -m_look_dir : m_look_dir), transform.localScale.y, transform.localScale.z);
        Vector2 pos = m_rb2d.position;
        float diff = m_dir * m_move_speed * Time.deltaTime;
        if (m_is_chicken)
        {
            diff = m_look_dir * m_move_speed * Time.deltaTime * 0.8f;
        }
        //set the animation to moving 
        if ( diff != 0 )
        {
            anim.SetBool("movement", true);
        }
        else
        {
            anim.SetBool("movement", false);
        }

        Vector2 new_pos = new Vector2(pos.x + diff, pos.y);
        m_rb2d.position = new_pos;
        //
        bool was_held = m_spell_held;
		m_spell_held = Input.GetButton(m_spell_button);
		if (!was_held && m_spell_held)
		{
			m_pizza_spell.add_spell_item(m_wizard_num);
		}
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Deadly deadly = col.gameObject.GetComponent<Deadly>();
        if (deadly == null)
        {
            return;
        }
        //Debug.Log("I collided with something that killed me!!");
        //Do stuff with the game over...
        //There is a refence to the game logic script already...
        set_dead();
        Destroy(col.gameObject);
    }


    public float get_look_dir()
    {
        return m_look_dir;
    }

	public void launch_float()
	{
        bool is_wrong = m_rb2d.gravityScale < 0.0f;
        m_rb2d.AddForce(new Vector2(0.0f, is_wrong ? -6.0f : 6.0f), ForceMode2D.Impulse);

        Vector2 pos_start = transform.TransformPoint(new Vector2(0.0f, -0.5f));
        GameObject go = (GameObject)Object.Instantiate(m_pizza_spell.m_float_particle_init, pos_start, transform.rotation);
        //go.transform.parent = gameObject;

        DestroyObject(go, 3.0f);

        /*Vector2 pos_start2 = transform.TransformPoint(new Vector2(0.0f, -1.0f));
        GameObject go2 = (GameObject)Object.Instantiate(m_pizza_spell.m_float_particle_air, pos_start2, transform.rotation);
        DestroyObject(go2, 3.0f);
        go2.transform.parent = gameObject.transform;*/
    }

    public void launch_fireball()
	{
        bool is_wrong = m_rb2d.gravityScale < 0.0f;
        Vector2 pos_start = transform.TransformPoint(new Vector2(1.0f, 0.0f));
        GameObject go = (GameObject)Object.Instantiate(m_pizza_spell.m_fireball_prefab, pos_start, m_pizza_spell.m_fireball_prefab.transform.rotation);
        Fireball fi = go.GetComponent<Fireball>();
        if (fi == null)
        {
            return;
        }
        fi.m_direction = m_look_dir;
        fi.m_casting_wizard = this;

        //check if the fireball hits a collision
        //if (fi hits a collision)
        {
            //play explosion
            //Vector2 pos_start2 = new Vector2(transform.position.x, transform.position.y);
            //GameObject go2 = (GameObject)Object.Instantiate(m_pizza_spell.m_fireball_particle_end, pos_start, m_pizza_spell.m_fireball_particle_end.transform.rotation);
            //DestroyObject(go2, 3.0f);
        }
    }

	public void launch_dash()
	{
		m_rb2d.AddForce(new Vector2(m_dash_force * m_look_dir, 0.0f), ForceMode2D.Impulse);

        Vector2 pos_start = transform.TransformPoint(new Vector2(-0.5f, 0.0f));

        Quaternion quat = Quaternion.AngleAxis(180.0f * m_look_dir, new Vector3(0.0f, 0.0f, 1.0f));
        GameObject go = (GameObject)Object.Instantiate(m_pizza_spell.m_dash_particle_air, pos_start, quat);
        DestroyObject(go, 3.0f);
        go.transform.parent = gameObject.transform;
    }

    public void set_dead()
    {
        m_pizza_spell.restart_level();
    }

    public void launch_chicken()
    {

        Vector2 pos_start = new Vector2(transform.position.x, transform.position.y);
        GameObject go = (GameObject)Object.Instantiate(m_pizza_spell.m_chicken_transformation, pos_start, m_pizza_spell.m_chicken_transformation.transform.rotation);
        DestroyObject(go, 3.0f);

        m_is_chicken = !m_is_chicken;
        if (m_is_chicken)
        {
            transform.localScale = m_default_scale * 0.5f;
            anim.SetBool("is_chicken", true);
            if (m_chicken_run)
            {
                m_chicken_run.PlayDelayed(Random.value);
            }
        }
        else
        {
            transform.localScale = m_default_scale;
            anim.SetBool("is_chicken", false);
            if (m_chicken_run)
            {
                m_chicken_run.Stop();
            }
        }
    }

    public void apply_gravity()
    {
        m_look_dir = -m_look_dir;
        m_rb2d.gravityScale = m_rb2d.gravityScale * -1.0f;
        m_gravity_rot_on = true;
    }


    public void set_ghost(bool i_ghost)
    {
        gameObject.layer = i_ghost ? 11: 0;

        Color c = GetComponent<SpriteRenderer>().color;
        c.a = i_ghost ? 0.3f : 1.0f;
        GetComponent<SpriteRenderer>().color = c;

        m_ghost_time = i_ghost ?  m_max_ghost_time : 0.0f;
    }

    public void launch_ghost()
    {
        set_ghost(gameObject.layer == 0);
        //bool will_be_ghost =
        //gameObject.layer = gameObject.layer == 11 ? 0 : 11;
 
    }
}
