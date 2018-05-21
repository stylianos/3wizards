using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{

    public string NextLevel;
    private bool m_done = false;
    /*public bool m_only_for_debug = false;

    private AirPlane target;
    private Vector3 start_pos;
    private Quaternion start_rot;
    private float start_angle;
    private Rigidbody2D target_rb2d;
    private float lerp_time = 0.0f;
    private float total_time = 0.0f;
    private float center_rot;
    private bool load_asked = false;
    private bool plane_found = false;
    private LevelControl m_level_control;*/

    void Start()
    {
        /*m_level_control = GameObject.Find("world").GetComponent<LevelControl>();
        if (!Application.isEditor && m_only_for_debug)
        {
            gameObject.SetActive(false);
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_done /*|| m_level_control.get_level_state() != LevelState.playing*/) { return; }
        Wiz wiz = other.GetComponent<Wiz>();
        if (wiz == null)
        {
            return;
        }
        Application.LoadLevel(NextLevel);
        /*m_level_control.finished(NextLevel);

        plane_found = true;
        lerp_time = 0.0f;
        total_time = 0.0f;
        target = a;
        target_rb2d = rb2d;
        target.m_autopilot = true;
        start_pos = target_rb2d.transform.position;
        start_rot = target_rb2d.transform.rotation;
        start_angle = target_rb2d.rotation;
        Vector3 dest = this.transform.position;
        center_rot = target.clamp_angle(Mathf.Atan2(dest.y - start_pos.y, dest.x - start_pos.x) / Mathf.PI * 180.0f);
        GetComponent<AudioSource>().PlayDelayed(0.2f);*/
    }

    /*IEnumerator ChangeLevel()
    {
        float secs = GameObject.Find("world").GetComponent<Fading>().BeginFade(1.0f);
        yield return new WaitForSeconds(secs);
        Application.LoadLevel(NextLevel);
    }*/

    void FixedUpdate()
    {
        /*if (target == null || target_rb2d == null)
        {
            return;
        }
        Vector3 dest = this.transform.position;
        lerp_time = Mathf.Min(lerp_time + Time.deltaTime * 2.5f, 1.0f);
        total_time += Time.deltaTime;

        target_rb2d.rotation -= Time.deltaTime * 500.0f * lerp_time;//Mathf.LerpAngle(start_angle, center_rot, Mathf.Min(lerp_time * 2.0f, 1.0f));

        Vector3 pos = Vector3.Lerp(start_pos, dest, lerp_time);
        target_rb2d.transform.position = pos;
        transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * 500.0f * lerp_time));

        if (total_time > 0.5f)
        {
            target.enableParticles(false);
            target.hitTurbo(false);
        }

        if (total_time > 2.0f && !load_asked)
        {
            load_asked = true;
            //StartCoroutine(ChangeLevel());
        }*/
    }
}