using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public Animator FadeAnim;
    public string NextLevel;

    public GameObject Particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Particle.SetActive(GetComponent<Collectables>().canPass);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Escape());   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LevelEnd") && GetComponent<Collectables>().canPass)
        {
            StartCoroutine(EndLevel());
        }
    }
    IEnumerator EndLevel()
    {
        FadeAnim.SetTrigger("fadeOut");
        FindFirstObjectByType<AudioManager>().PlaySFX(FindFirstObjectByType<AudioManager>().End);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        PlayerPrefs.Save();
        GetComponent<PlayerScript>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(NextLevel);
    }
    IEnumerator Escape()
    {
        PlayerPrefs.SetString("Escaped", "True");
        FadeAnim.SetTrigger("fadeOut");
        GetComponent<PlayerScript>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
