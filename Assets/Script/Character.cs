using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float speed = 10.0f;
    public int health;
    public Transform checkpoint;
    public WaterCounter counter;

    void Start()
    {
        
    }
  
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3(horizontal, vertical) * speed * Time.deltaTime;
    }
    public void hurt(int damage)
    {
        health=health-damage;
        if (health<=0)
        {
           transform.position=checkpoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            counter.IncreaseScore();
            Destroy(collision.gameObject);
        }
    }
}
