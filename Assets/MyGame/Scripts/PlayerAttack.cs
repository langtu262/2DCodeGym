using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] Transform pointAttack;
    [SerializeField] float radiusAttack;
    [SerializeField] LayerMask enemyLayer;
    private Animator anim;
    private int idAttack;
    float nextAttack=0f;
    public float attackRate=0.2f;
    public float attackAfterTime=0.15f;
    public int damageToGive = 10;
    public Vector2 fore;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        idAttack = Animator.StringToHash("isAttack");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           if (GetKeyR())
            {
                anim.SetTrigger(idAttack);
            }
        }
    }
    private bool GetKeyR()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            StartCoroutine(Attack(attackAfterTime));
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator Attack(float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(pointAttack.position, radiusAttack,enemyLayer);
        foreach(var enemy in hitEnemys)
        {
            enemy.GetComponent<ICanTakeDamage>().TakeDamage(damageToGive,fore,gameObject);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (pointAttack != null)
        { 
            Gizmos.DrawWireSphere(pointAttack.position, radiusAttack);
        }
    }
}
