using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WizardBullet : MonoBehaviour
{
    public float attackPower;
    public float knockTime;
    public float moveSpeed;
    private Transform player;
    private Vector2 target;
    private Vector2 dir;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        dir = (player.position - transform.position).normalized;

    }

    void Update()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LivingEntity target = other.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                target.OnDamage(20);
            }

            Rigidbody2D rig = other.GetComponent<Rigidbody2D>();
            if(rig !=null)
            {
                Vector2 dif = rig.transform.position - transform.position;
                dif = dif.normalized * attackPower;
                rig.AddForce(dif, ForceMode2D.Impulse);
            }


        }
    }


    private IEnumerator KnockCo(Rigidbody2D rig)
    {
        if(rig !=null)
        {
            yield return new WaitForSeconds(knockTime);
            rig.velocity = Vector2.zero;
        }
    }
}