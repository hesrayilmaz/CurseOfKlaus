using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject aboutscene;

    void Update()
    {
        
    }
    public void start()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void About()
    {
        aboutscene.SetActive(true);
    }
    public void Back()
    {
        aboutscene.SetActive(false);
    }
    public void Muted()
    {
        AudioManager.Instance.ToggleMusic();
         AudioManager.Instance.ToggleSFX();
    }
}
