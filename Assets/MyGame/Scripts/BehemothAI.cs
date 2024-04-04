using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BehemothAI : EnemyAI
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Animator anim;
    private Transform target;
    [SerializeField] private float speedMove = 3;
    [SerializeField] private float stopDistance = 3;
    private Vector3 pointC;
    private Vector3 targetPos;
    private int isIdle;
    [SerializeField] private float attackDistance;
    void Start()
    {
        health = maxHealth;
        pointC = pointA.position;
        isIdle = Animator.StringToHash("isIdle");
        target=GameObject.FindGameObjectWithTag("Player").transform;
       
    }
    public void TakeDamage()
    {
        health -= attack;
        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            Invoke("DesTroys", 1);
        }
    }
    void DesTroys()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
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
                

                if (Vector2.Distance(transform.position, target.position) < attackDistance && health>0)
                {
                    anim.SetBool("isAttack", true);
                }
                else
                {
                    rb.velocity = dir* speedMove; //di chuyen
                    anim.SetBool("isAttack", false);
                }

            }
            else if(Vector2.Distance(transform.position, target.position) > stopDistance) 
            {
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
   

}
