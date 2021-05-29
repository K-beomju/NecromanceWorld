using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Rigidbody2D rig;

    protected Vector2 normalizedDirection;
    protected Vector2 direction;
    protected  Vector3 targetPosition;
    protected Collider2D hitCollider;


    protected float moveSpeed;
    protected bool dead;
    public float health;
    public float startingHealth;
    public float attackRange;
    public float attackDamage;



    protected void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }

    protected  void Start()
    {
        dead = false;
        moveSpeed = 5f;
        health = startingHealth;
    }

    public void Rotate()
    {
        transform.Translate(normalizedDirection  * moveSpeed * Time.deltaTime);

        normalizedDirection = new Vector2(direction.x,direction.y);
        if(normalizedDirection.x > 0)
        {
            //SpriteRender가져와서 FlipX해도 가능
            transform.localScale = new Vector2(0.7f,0.7f);
        }
        else
        {
             transform.localScale = new Vector2(-0.7f,0.7f);
        }
    }

    public void Attack(string targetName)
    {
        int layerMask = 1 << LayerMask.NameToLayer(targetName);
        if (hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, layerMask))
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }

      public void DAttack()
    {
        if (hitCollider != null)
        {
            LivingEntity target = hitCollider.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                target.OnDamage(attackDamage);
            }

        }

    }


    public void OnDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();

        }
    }

    protected abstract void Die();

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
