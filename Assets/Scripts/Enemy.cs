using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    //public Transform startPosition;
    public float speed;
    public float range;
    public bool isattack;
    public float attacktimer;
    public float attackrange;
    public float currentspeed;
    public int health;
    private GameObject startPosition;


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        isattack=false;
        startPosition = new GameObject("EnemyStartPosition");
        startPosition.transform.position = transform.position;
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
        transform.position = Vector3.MoveTowards(transform.position, startPosition.transform.position, speed * Time.deltaTime);
    }
    public void hurt(int damage)
    {
        health=health-damage;
          if (health<=0)
          {
             Destroy(gameObject);
          }
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
            attacktimer=1f;
        }
          if (attacktimer>0&&isattack==true)
          {
             attacktimer=attacktimer-Time.deltaTime;
          }
          else if (attacktimer<0&&isattack==true)
          {
              target.GetComponent<Character>().hurt(20);
              attacktimer=1f;
          }
    }
    private void OnCollisionEnter2D(Collision2D col)
     {
        if (col.gameObject.tag=="Player")
        {
               speed=0.001f;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
     {
        if (col.gameObject.tag=="Player")
        {
               speed=currentspeed;
        }
    }
}
