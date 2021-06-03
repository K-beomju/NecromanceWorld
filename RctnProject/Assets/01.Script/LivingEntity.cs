using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Rigidbody2D rig;
    protected CapsuleCollider2D capsule;

    protected Vector2 normalizedDirection;
    protected Vector2 direction;
    protected Vector3 targetPosition;
    protected Collider2D hitCollider;


    protected float moveSpeed;
    protected bool dead;
    private float health;
    public float startingHealth;

    public Vector3 offset;
    protected Vector3 attackPosition;
    public AudioSource audioSource;

    [SerializeField]
    private AbilityData abilityData;
    public AbilityData ZombieData { set { abilityData = value; } }




    protected void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }

    protected void Start()
    {
        dead = false;
        moveSpeed = abilityData.MoveSpeed;
        health = startingHealth;
    }

    public void Rotate()
    {
        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);


        normalizedDirection = new Vector2(direction.x, direction.y);
        if (normalizedDirection.x > 0)
        {
            //SpriteRender가져와서 FlipX해도 가능
            transform.localScale = new Vector2(0.9f, 0.9f);
        }
        else
        {
            transform.localScale = new Vector2(-0.9f, 0.9f);
        }
    }

    public void Attack(string targetName)
    {
        int layerMask = 1 << LayerMask.NameToLayer(targetName);
        attackPosition = transform.position + offset;
        if (hitCollider = Physics2D.OverlapCircle(attackPosition, abilityData.AttackRange, layerMask))
        {

            anim.SetBool("isAttack", true);
        }
        else
        {
            moveSpeed = abilityData.MoveSpeed;
            anim.SetBool("isAttack", false);
        }
    }



    public void DAttack()
    {
        audioSource.Play();
        if (hitCollider != null)
        {
            if(hitCollider.transform.position.x  >= transform.position.x)
            {
                transform.localScale = new Vector2(0.9f, 0.9f);
            }
            else
            {
                 transform.localScale = new Vector2(-0.9f, 0.9f);
            }

            LivingEntity target = hitCollider.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                moveSpeed = 3;
                target.OnDamage(abilityData.AttackDamage);
            }

        }

    }





    public  void OnDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();

        }
    }




    protected abstract void Die();

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition, abilityData.AttackRange);
    }

}
