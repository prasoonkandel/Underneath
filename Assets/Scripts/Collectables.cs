using UnityEngine;

public class Collectables : MonoBehaviour
{
    public float numOfCollectables;
    public bool canPass;
    private float collected;
    public GameObject Particle, Particle1;


    void Start()
    {
        numOfCollectables = GameObject.FindGameObjectsWithTag("collectable").Length;
        collected = 0f;
        canPass = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(collected == numOfCollectables)
        {
            canPass = true;
        }
        else
        {
            canPass = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectable") && other != null)
        {
            collected++;
            FindFirstObjectByType<AudioManager>().PlaySFX(FindFirstObjectByType<AudioManager>().Collect);
            if (!GetComponent<PlayerScript>().inverted) Instantiate(Particle, other.gameObject.transform.position, Quaternion.identity);
            else Instantiate(Particle1, other.gameObject.transform.position, Quaternion.identity);

            //yes
            Destroy(other.gameObject);

        }
    }
}
