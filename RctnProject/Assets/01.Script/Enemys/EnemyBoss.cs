using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : LivingEntity
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    private bool isPatrol;
    public Vector2 moveSpot;

    // protected override void Start()
    // {

    //     waitTime = startWaitTime;
    //     moveSpot = new Vector2(Random.Range(-25, -15), Random.Range(-25, 15));
    // }

    void Update()
    {


        if (transform.position.x <= moveSpot.x)
        {

            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpot.x, moveSpot.y), speed * Time.deltaTime);
        isPatrol = true;
        if (Vector2.Distance(transform.position, moveSpot) < 1f)
        {
            isPatrol = false;
            if (waitTime <= 0)
            {

                waitTime = startWaitTime;
                moveSpot = new Vector2(Random.Range(-25, -15), Random.Range(-25, 15));
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }


    // public void Attack()
    // {
    //     Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, attackRange, 1 << LayerMask.NameToLayer("Player"));
    //     foreach (var player in objects)
    //     {
    //         LivingPlayer target = player.gameObject.GetComponent<LivingPlayer>();
    //         if (target != null)
    //         {
    //             target.OnDamage(attackDamage);
    //         }
    //     }
    // }




    protected override void Die()
    {

    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    // protected override void Attack()
    // {
    //     throw new System.NotImplementedException();
    // }
}
