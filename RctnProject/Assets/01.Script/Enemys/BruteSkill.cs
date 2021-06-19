using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteSkill : MonoBehaviour
{
    private Rigidbody2D rig;
    public float radius;
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void AddForce()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Player"));
        foreach (Collider2D player in objects)
        {
            //Player target = player.gameObject.GetComponent<Player>();
         //   if(target != null)
           // {
               // target.AddExplosion(transform.position , 20);
         //   }

        }
    }

     public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
