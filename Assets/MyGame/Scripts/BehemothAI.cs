using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BehemothAI : EnemyAI, ICanTakeDamage
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Animator anim;
    [SerializeField] private float attackDistance;
    [SerializeField] private float speedMove = 3;
    [SerializeField] private float stopDistance = 3;
    [SerializeField] GameObject HurtEffect;
    [SerializeField] int damageToGive;
    [SerializeField] Vector2 fore;
    private Transform target;    
    private Vector3 pointC;
    private Vector3 targetPos;
    private int isIdle;
    private bool isDead = false;
    private float nextTime;
    private float rateTime=0.2f;

    void Start()
    {

        health = maxHealth;
        pointC = pointA.position;
        isIdle = Animator.StringToHash("isIdle");
        target=GameObject.FindGameObjectWithTag("Player").transform;
       
    }
    /*public void TakeDamage()
    {
        health -= attack;
        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            Invoke("DesTroys", 1);
        }
    }*/
    void Update()
    {
        if (target != null)
        {
           
            Vector2 dir = (target.position - transform.position).normalized;
            if (Vector2.Distance(transform.position,target.position) < stopDistance)
            {
                
                if(target.position.x - transform.position.x < 0)
                   {
                      // sp.flipX = true; // flipx
                       Vector2 scale = transform.localScale; //flipx
                       scale.x = 1;//flipx
                       transform.localScale = scale;//flipx
                   }
                else if(target.position.x - transform.position.x > 0)
                   {
                    // sp.flipX = false; //flip x
                        Vector2 scale = transform.localScale; //flipx
                        scale.x = -1;//flipx
                        transform.localScale = scale;//flipx
                   }
                

                if (Vector2.Distance(transform.position, target.position) < attackDistance && isDead==false)
                {
                    anim.SetBool("isAttack", true);
                }
                else
                {
                    if (isDead == false)
                    { rb.velocity = dir * speedMove; } //di chuyen
                    anim.SetBool("isAttack", false);
                }

            }
            else if(Vector2.Distance(transform.position, target.position) > stopDistance) 
            {
                if (isDead == true) return;
                /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                     return;*/
                if (pointC == pointA.position)
                {
                    targetPos = pointB.position;
                   // sp.flipX = false;
                    Vector2 scale = transform.localScale; //flipx
                    scale.x = -1;//flipx
                    transform.localScale = scale;//flipx*/                
                    anim.SetTrigger(isIdle);
                    pointC = pointB.position;
                }
                if (transform.position==pointB.position)
                {
                    targetPos = pointA.position;
                    //sp.flipX = true;
                    Vector2 scale = transform.localScale; //flipx
                    scale.x = 1;//flipx
                    transform.localScale = scale;//flipx*/
                    anim.SetTrigger(isIdle);
                }
                if (transform.position == pointA.position)
                {
                    pointC = pointA.position;                   
                }                      
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }        
    }
    public void TakeDamage (int damage, Vector2 force, GameObject instigator) //ke thua phuong thuc cua class ICanTakeDamage
    {
        if(isDead) return;
        health -= damage;
        if (HurtEffect != null)
        {
            Instantiate(HurtEffect, instigator.transform.position, Quaternion.identity); // tao hieu ung khi bi danh trung
        }
        if(health<=0)
        {
            isDead = true;
            anim.SetTrigger("isDead");
            Destroy(gameObject, 3f);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = target.GetComponent<PlayerController>(); // tao va gan doi tuong player va thanh phan cua doi tuong target trong class PlayerController
        if (player == null) return;
        if (isDead == true) return;
        {
            nextTime = Time.time + rateTime;
            player.TakeDamage(damageToGive, fore, gameObject); // goi ham TakeDamege cua class playerController va truyen gia tri (damageToGive, fore, gameObject) vao
        }
    }

}
