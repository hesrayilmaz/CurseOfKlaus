using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject pause;
    public bool ispause;
    public TextMeshProUGUI txt;
    void Start()
    {

        Time.timeScale=1;
        ispause=false;
    }
    public void health(int health)
    {
        txt.text=health+"";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ispause==false)
            {
                Time.timeScale=0;
                pause.SetActive(true);
                ispause=true;
            }
            else
            {
                Time.timeScale=1;
                pause.SetActive(false);
                ispause=false;
            }
        }
    }
    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Muted()
    {
        AudioManager.Instance.ToggleMusic();
         AudioManager.Instance.ToggleSFX();
    }
    public void paused()
    {
         if (ispause==false)
            {
                Time.timeScale=0;
                pause.SetActive(true);
                ispause=true;
            }
            else
            {
                Time.timeScale=1;
                pause.SetActive(false);
                ispause=false;
            }
    }
}
