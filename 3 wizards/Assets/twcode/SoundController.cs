#region Namespaces

	using UnityEngine;
	using System.Collections;

#endregion

public class SoundController : MonoBehaviour
{
	#region Variables

	// Private reference which can be accessed by this class only
	private static SoundController instance;

	// Public static reference that can be accesd from anywhere
	public static SoundController Instance
	{
		get
		{
			// Check if instance has not been set yet and set it it is not set already
			// This takes place only on the first time usage of this reference
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<SoundController>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	// Max number of AudioSource components
	public int m_MaxAudioSource = 10;

	// AudioClip component for music
	public AudioClip[] m_Music = null;
	private int m_music_index = 0;

	// AudioClip component for buttons
	public AudioClip m_ButtonBack = null;
	public AudioClip m_ButtonClick = null;
	
	public AudioClip m_wizard_0 = null;
	public AudioClip m_wizard_1 = null;
	public AudioClip m_wizard_2 = null;
	public AudioClip m_spell_fail = null;
	public AudioClip m_spell_cast = null;
	public AudioClip m_dead = null;

    public AudioClip m_spell_charge = null;
    public AudioClip m_spell_chicken = null;
    public AudioClip m_spell_chicken_end = null;
    public AudioClip m_spell_dust = null;
    public AudioClip m_spell_fireball = null;
    public AudioClip m_spell_gravity = null;
    public AudioClip m_spell_jump = null;

    public AudioClip m_chicken_loop = null;
    public AudioClip m_chicken_death = null;
    public AudioClip m_unlock_door = null;
    public AudioClip m_flip_switch = null;
    public AudioClip m_flip_explosion = null;


    private AudioSource m_music_source = null;

	// Sound volume
	public float m_SoundVolume = 1.0f;

	// Music volume
	public float m_MusicVolume = 1.0f;

	#endregion Variables

	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

	// Awake is called when the script instance is being loaded.
	void Awake()
	{
		if (instance == null)
		{
			// Make the current instance as the singleton
			instance = this;

			// Make it persistent  
			DontDestroyOnLoad(gameObject);
		}
		else if (this != instance)
		{
			InitAudioListener();
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{
		// Initial AudioListener
		InitAudioListener();

		// Automatically play music if it is not playing
		if (IsMusicPlaying() == false)
		{
			// Play music
			Play_Music();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (m_music_source != null && !m_music_source.isPlaying)
		{
			m_music_source = null;
			Play_Music();
		}
	}

	#endregion //MonoBehaviour functions

	// ######################################################################
	// Utilitie functions
	// ######################################################################

	#region Functions

	// Initial AudioListener
	// This function remove all AudioListener in other objects then it adds new one this object.
	void InitAudioListener()
	{
		// Destroy other's AudioListener components
		AudioListener[] pAudioListenerToDestroy = GameObject.FindObjectsOfType<AudioListener>();
		foreach (AudioListener child in pAudioListenerToDestroy)
		{
			if (child.gameObject.GetComponent<SoundController>() == null)
			{
				Destroy(child);
			}
		}

		// Adds new AudioListener to this object
		AudioListener pAudioListener = gameObject.GetComponent<AudioListener>();
		if (pAudioListener == null)
		{
			pAudioListener = gameObject.AddComponent<AudioListener>();
		}
	}

		// Play music
	void PlayMusic(AudioClip pAudioClip)
	{
		// Return if the given AudioClip is null
		if (pAudioClip == null)
			return;

		AudioListener pAudioListener = gameObject.GetComponent<AudioListener>();
		if (pAudioListener != null)
		{
			// Look for an AudioListener component that is not playing background music or sounds.
			bool IsPlaySuccess = false;
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					// Play music
					if (pAudioSourceList[i].isPlaying == false)
					{
						pAudioSourceList[i].loop = false;
						pAudioSourceList[i].clip = pAudioClip;
						pAudioSourceList[i].ignoreListenerVolume = true;
						pAudioSourceList[i].playOnAwake = false;
						pAudioSourceList[i].volume = m_MusicVolume;
                        //pAudioSourceList[i].transform.position = new Vector3(-20.0f, 0.0f, 0.0f);
                        pAudioSourceList[i].Play();
						m_music_source = pAudioSourceList[i];
                        break;
					}
				}
			}

			// If there is not enough AudioListener to play AudioClip then add new one and play it
			if (IsPlaySuccess == false && pAudioSourceList.Length < 16)
			{
				AudioSource pAudioSource = pAudioListener.gameObject.AddComponent<AudioSource>();
				pAudioSource.rolloffMode = AudioRolloffMode.Linear;
				pAudioSource.loop = false;
				pAudioSource.clip = pAudioClip;
				pAudioSource.ignoreListenerVolume = true;
				pAudioSource.playOnAwake = false;
				pAudioSource.volume = m_MusicVolume;
				pAudioSource.Play();
				m_music_source = pAudioSource;
            }
		}
	}

	// Play sound one shot
	public void PlaySoundOneShot(AudioClip pAudioClip)
	{

		// Return if the given AudioClip is null
		if (pAudioClip == null)
			return;

		// We wait for a while after scene loaded
		if (Time.timeSinceLevelLoad < 1.5f)
			return;

		// Look for an AudioListener component that is not playing background music or sounds.
		AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
		if (pAudioListener != null)
		{
			bool IsPlaySuccess = false;
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					if (pAudioSourceList[i].isPlaying == false)
					{
                        // Play sound
                        pAudioSourceList[i].volume = m_SoundVolume;
                        pAudioSourceList[i].spatialBlend = 0;
                        pAudioSourceList[i].PlayOneShot(pAudioClip);
						break;
					}
				}
			}

			// If there is not enough AudioListener to play AudioClip then add new one and play it
			if (IsPlaySuccess == false && pAudioSourceList.Length < 16)
			{
				// Play sound
				AudioSource pAudioSource = pAudioListener.gameObject.AddComponent<AudioSource>();
                //pAudioSource.transform.position = new Vector3(20.0f, 0.0f, 0.0f);
                pAudioSource.rolloffMode = AudioRolloffMode.Linear;
                pAudioSource.spatialBlend = 0;
                pAudioSource.volume = m_SoundVolume;
                pAudioSource.PlayOneShot(pAudioClip);
			}
		}
	}

	// Set music volume between 0.0 to 1.0
	public void SetMusicVolume(float volume)
	{
		m_MusicVolume = volume;

		AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
		if (pAudioListener != null)
		{
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					if (pAudioSourceList[i].ignoreListenerVolume)
					{
						pAudioSourceList[i].volume = volume;
					}
				}
			}
		}
	}

	// If music is playing, return true.
	public bool IsMusicPlaying()
	{
		AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
		if (pAudioListener != null)
		{
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					if (pAudioSourceList[i].ignoreListenerVolume == true)
					{
						if (pAudioSourceList[i].isPlaying == true)
						{
							return true;
						}
					}
				}
			}
		}

		return false;
	}

	// Set sound volume between 0.0 to 1.0
	public void SetSoundVolume(float volume)
	{
		m_SoundVolume = volume;
		//AudioListener.volume = volume;
	}

	// Play music
	public void Play_Music()
	{
		if (m_Music == null || m_Music.Length == 0)
		{
			return;
		}
		PlayMusic(m_Music[m_music_index]);
		m_music_index = (m_music_index + 1) % m_Music.Length;
    }

	// Play Back button sound
	public void Play_SoundBack()
	{
		PlaySoundOneShot(m_ButtonBack);
	}

	// Play Click sound
	public void Play_SoundClick()
	{
		PlaySoundOneShot(m_ButtonClick);
	}

    public void Play_Wizard0()
    {
        PlaySoundOneShot(m_wizard_0);
    }

    public void Play_Wizard1()
    {
        PlaySoundOneShot(m_wizard_1);
    }

    public void Play_Wizard2()
    {
        PlaySoundOneShot(m_wizard_2);
    }

    public void Play_SpellCast()
    {
        PlaySoundOneShot(m_spell_cast);
    }

    public void Play_SpellFail()
    {
        PlaySoundOneShot(m_spell_fail);
    }

    public void Play_Dead()
    {
        PlaySoundOneShot(m_dead);
    }


    #endregion //Functions
}
