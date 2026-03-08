using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip Jump, End, Death, Collect, Reverse;
    public static AudioManager Instance;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            GetComponent<AudioSource>().volume = 0.3f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    void Awake()
    {
        // If another instance exists, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance
        Instance = this;

        // Prevent destruction when loading new scenes
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySFX(AudioClip adc)
    {
        SFX.PlayOneShot(adc);

    }
}
