using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Animator Anim;
    public Animator FadeAnim;
        public Sprite completedSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerPrefs.GetString("Escaped","False") != "False")
        {
            Anim.Play("LevelSelectScreen");
        }
        int levelIndex = 1;

        while (true)
        {
            string levelKey = "Level" + levelIndex;
            string buttonName = "LevelChoose (" + levelIndex + ")";

            if (!GameObject.Find(buttonName))
                break;

            if (PlayerPrefs.GetInt(levelKey, 0) == 1)
            {
                GameObject button = GameObject.Find(buttonName);
                Image img = button.GetComponent<Image>();
                img.sprite = completedSprite;
            }

            levelIndex++;
        }
    }


    public void PlayLevel(string Level)
    {
        if (FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            PlayerPrefs.SetString("Escaped", "False");

            StartCoroutine(StartLevel(Level));
        }
    }
    IEnumerator StartLevel(string Level)
    {
        FadeAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(Level);
    }
    public void Select()
    {
        if(FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        Anim.SetTrigger("select");
    }
    public void Quit()
    {
        if (FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            Application.Quit();
    }
    public void Back()
    {
        if (FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && FadeAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            Anim.SetTrigger("back");
    }
}
