using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellPanel : MonoBehaviour {

	public Image[] m_images;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void set_obscure(bool i_val)
	{
		foreach (Image image in m_images)
		{
			if (image != null)
			{
				image.color = i_val ? Color.black : Color.white;
			}
		}
	}
}
