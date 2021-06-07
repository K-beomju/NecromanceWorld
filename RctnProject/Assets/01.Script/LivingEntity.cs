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
    protected bool bAttack;
    protected float health;
    public float startingHealth;

    public Vector3 offset; // 위치 보정
    private AudioSource attackAudio;
    protected Vector3 attackPosition;






    [SerializeField]
    private AbilityData abilityData;
    public AbilityData ZombieData { set { abilityData = value; } }

    private EffectObject hitEffect;
    private Uipanel deadTxt;


    protected void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        attackAudio = GetComponent<AudioSource>();

    }

    protected virtual void Start()
    {
        bAttack = false;
        dead = false;
        moveSpeed = abilityData.MoveSpeed;
        health = startingHealth;

    }

     public IEnumerator randomPosition()
    {
        while (true)
        {
        int time = UnityEngine.Random.Range(3,10);
         GameManager.instance.x = UnityEngine.Random.Range(-1,2);

        int y = UnityEngine.Random.Range(-1,2);

        GameManager.instance.moveVector = new Vector2( GameManager.instance.x,y);
        yield return new WaitForSeconds(time);

        }

    }




    public void Rotate()
    {
        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);
        if(bAttack)
        {

        }
        else
        {
            moveSpeed = abilityData.MoveSpeed;
        }


        normalizedDirection = new Vector2(direction.x, direction.y);
        if (normalizedDirection.x > 0)
        {

             sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;

        }
    }

    public void Attack(string targetName)
    {
        int layerMask = 1 << LayerMask.NameToLayer(targetName);
        attackPosition = transform.position + offset;
        if (hitCollider = Physics2D.OverlapCircle(attackPosition, abilityData.AttackRange, layerMask))
        {
            bAttack = true;
            anim.SetBool("isAttack", true);
        }
        else
        {
              bAttack = false;
            anim.SetBool("isAttack", false);
        }
    }



    public void DAttack()
    {
        attackAudio.Play();
        if (hitCollider != null)
        {
            if (hitCollider.transform.position.x >= transform.position.x)
            {
               sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }

            LivingEntity target = hitCollider.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                target.OnDamage(abilityData.AttackDamage);
            }

        }

    }





    public void OnDamage(float damage)
    {
        health -= damage;
        OnHitEffect();

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

    public void OnHitEffect()
    {
        hitEffect = GameManager.GetHitEffect();
        hitEffect.SetPositionData(new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, Random.Range(0, 360f)));
    }

    public void OnDeadTxt(GameObject gameObject)
    {
        deadTxt = GameManager.GetDeadText();
        deadTxt.SetPosition(gameObject.transform.position);
    }



}
