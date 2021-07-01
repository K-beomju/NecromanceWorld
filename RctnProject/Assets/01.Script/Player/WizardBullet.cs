using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WizardBullet : MonoBehaviour
{
    public float attackPower;
    public float knockTime;

    public float moveSpeed;
    private Transform targetPos;
    public string targetName;
    public float attackDamage;
    private Vector2 dir;

    void OnEnable()
    {
    }

    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag(targetName).GetComponent<Transform>();
        dir = (targetPos.position - transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(targetName))
        {
            LivingEntity target = other.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                target.OnDamage(attackDamage);
            }
            Destroy(this.gameObject, 10);

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