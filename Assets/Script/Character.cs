using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float speed ;
    public int health;
    public Transform checkpoint;
    public WaterCounter counter;
    public float attacktimer;  
     public float horizontal;
     public float vertical;
     public bool facingRight;
     public Animator anim;
     public float attackrange;
     public Transform attackpoint;
     public LayerMask enemylayers;
     public bool canattack;
     void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
       vertical = Input.GetAxis("Vertical");
        Animation();
        attackspacing();
        if (Input.GetKeyDown(KeyCode.Space)&&canattack==true)
        {        
            attack(); 
            attackanimation();  
            canattack=false;      
        }
        transform.position = transform.position + new Vector3(horizontal, vertical) * speed * Time.deltaTime;
    }
    void attackspacing()
    {
        if (attacktimer>0&&canattack==false)
        {
            attacktimer=attacktimer-Time.deltaTime;
        }
        else if (attacktimer<=0&&canattack==false)
        {
            attacktimer=0.3f;
            canattack=true;
        }
    }
    void attack()
    {
        Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(attackpoint.position,attackrange,enemylayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().hurt(20);
        }
    }
    void attackanimation()
    {
         if (vertical>0)
         {
            anim.SetTrigger("up.attack");
         }
         else if (vertical<0)
         {
             anim.SetTrigger("down.attack");
         }
         else
         {
             anim.SetTrigger("side.attack");
         }
    }
    void OnDrawGizmosSelected() 
    {
        if (attackpoint==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position,attackrange);
    }
    void Animation()
    {
        if (horizontal>0)
        {     
           Characterwalking();
        }
        else if(horizontal<0)
        {
              Characterwalking();
        }  
        else if (horizontal==0)
        {
             anim.SetBool("side.walk",false);
        }
        if (facingRight==false&&horizontal>0)
        {
            CharacterFlip();
            
        }
        if (facingRight == true && horizontal < 0)
        {
            CharacterFlip();
        }     
        if (vertical>0)
        {
               characterwalkingup();
        }
        else if(vertical<0)
        {
             characterwalkingdown();
        }
         else if (vertical==0)
        {
            anim.SetBool("up.walk",false);
             anim.SetBool("down.walk",false);
        } 
    }
    void Characterwalking()
    {
            anim.SetBool("down.iddle",false);
              anim.SetBool("up.iddle",false);
            anim.SetBool("side.iddle",true);
             anim.SetBool("side.walk",true);   
    }
    void characterwalkingup()
    {
        anim.SetBool("side.iddle",false);
        anim.SetBool("down.iddle",false);
        anim.SetBool("up.iddle",true);
         anim.SetBool("up.walk",true);

    }
     void characterwalkingdown()
    {
        anim.SetBool("side.iddle",false);
        anim.SetBool("up.iddle",false);
        anim.SetBool("down.iddle",true);
         anim.SetBool("down.walk",true);

    }
    void CharacterFlip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        
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
