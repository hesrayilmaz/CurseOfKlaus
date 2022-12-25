using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject aboutscene;
     public GameObject stscene;

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
    public void St()
    {
         aboutscene.SetActive(false);
         stscene.SetActive(true);
    }
    public void About()
    {
        stscene.SetActive(false);
        aboutscene.SetActive(true);
    }
    public void Back()
    {
        aboutscene.SetActive(false);
        stscene.SetActive(false);
    }
    public void Muted()
    {
        AudioManager.Instance.ToggleMusic();
         AudioManager.Instance.ToggleSFX();
    }
}
