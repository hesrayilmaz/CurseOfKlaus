using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    public Transform startPosition;
    public float speed;
    public float range;
    public GameObject ch;
    public bool isattack;
    public float attacktimer;
    public float attackrange;


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        isattack=false;
    }
    void Update()
    {
        if(Vector3.Distance(target.position,transform.position) <= range)
        {
            FollowPlayer();
        }
        else
        {
            GoStartPosition();
        }
        attack();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void GoStartPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition.position, speed * Time.deltaTime);
    }
     void attack()//bu kısım düzeltilecek
    {
         if(Vector3.Distance(target.position,transform.position) <= attackrange)
        {
           isattack=true;
        }
        else
        {
            isattack=false;
            attacktimer=3f;
        }
          if (attacktimer>0&&isattack==true)
          {
             attacktimer=attacktimer-Time.deltaTime;
          }
          else if (attacktimer<0&&isattack==true)
          {
              ch.GetComponent<Character>().hurt(20);
              attacktimer=1.5f;
          }
    }
}
