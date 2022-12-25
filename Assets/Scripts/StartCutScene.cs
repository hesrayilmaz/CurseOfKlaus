using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour
{
    public Animator anim;   
    void OnTriggerEnter2D(Collider2D col)
     {
        if (col.gameObject.tag=="Player")
        {
            anim.SetBool("Cutscene1",true);
            Invoke(nameof(stop),1.5f);
        }
    }
    void stop()
    {
        anim.SetBool("Cutscene1",false);
    }
}
