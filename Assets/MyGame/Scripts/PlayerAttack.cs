using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private int idAttack;
    [SerializeField] Transform pointAttack;
    [SerializeField] float radiusAttack;
    [SerializeField] LayerMask enemyLayer;
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
            Attack();
        }
    }
    void Attack()
    {
        anim.SetTrigger(idAttack);
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll (transform.position, radiusAttack,enemyLayer);
        foreach (var enemy in hitEnemys) 
        {
            enemy.GetComponent<BehemothAI>().TakeDamage();
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
