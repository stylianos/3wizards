using UnityEngine;
using System.Collections;

public class DebugDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;
	public PizzaSpell m_pizza_spell;

	void Start()
	{
        m_pizza_spell = GameObject.Find("PizzaSpell").GetComponent<PizzaSpell>();
    }

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
        if (!Application.isEditor)
        {
            return;
        }

        int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		int font_size = Mathf.Max(h * 2 / 100, 12);
		float hy = font_size;
		float py = h - ((hy + 5) * 2);
        Rect rect = new Rect(0, py, w, hy);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = font_size;
		style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);

		py += hy + 5;
		Rect rect2 = new Rect(0, py, w, hy);

		text = string.Format("A[{0}] B[{1}] C[{2}] rem[{3}]", m_pizza_spell.get_item(0), m_pizza_spell.get_item(1), m_pizza_spell.get_item(2), m_pizza_spell.remaining_item());
		GUI.Label(rect2, text, style);
	}
}