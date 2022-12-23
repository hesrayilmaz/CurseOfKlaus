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


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Character").transform;
    }

    // Update is called once per frame
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
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void GoStartPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition.position, speed * Time.deltaTime);
    }
}
