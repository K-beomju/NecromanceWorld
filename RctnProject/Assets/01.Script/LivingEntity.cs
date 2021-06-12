using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Rigidbody2D rig;
    protected CircleCollider2D circle;

    protected Vector2 normalizedDirection;
    protected Vector2 direction;
    protected Vector3 targetPosition;
    protected Collider2D hitCollider;


    protected bool isDead;
    protected bool isAttack;

    protected float health;
    protected float attackRange;
    protected float attackDamage;
    protected float moveSpeed;


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
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        attackAudio = GetComponent<AudioSource>();
        SetAbility();


    }
    public void SetAbility()
    {
        health = abilityData.Health;
        attackRange = abilityData.AttackRange;
        attackDamage = abilityData.AttackDamage;
        moveSpeed = abilityData.MoveSpeed;
    }

    protected virtual void Start()
    {
        isAttack = false;
        isDead = false;


    }

     public IEnumerator randomPosition()
    {
        while (true)
        {
        int time = UnityEngine.Random.Range(5,11);
         GameManager.instance.x = UnityEngine.Random.Range(-1,2);
         int y = UnityEngine.Random.Range(-1,2);


        GameManager.instance.moveVector = new Vector2( GameManager.instance.x,y);
        yield return new WaitForSeconds(time);

        }

    }


    public void Attack(string targetName)
    {
        int layerMask = 1 << LayerMask.NameToLayer(targetName);
        attackPosition = transform.position + offset;
        if (hitCollider = Physics2D.OverlapCircle(attackPosition, attackRange, layerMask))
        {
            isAttack = true;
            anim.SetBool("isAttack", true);
        }
        else
        {
              isAttack = false;
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
                target.OnDamage(1);
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
        Gizmos.DrawWireSphere(attackPosition,attackRange);
    }

    #region 풀링 오브젝트
    public void OnHitEffect()
    {
        hitEffect = GameManager.GetHitEffect(0);
        hitEffect.SetPositionData(transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
    }
      public void OnNecroEffect(int i)
    {
        hitEffect = GameManager.GetHitEffect(i);
        hitEffect.SetPositionData(transform.position, Quaternion.identity);
    }

    public void OnDeadTxt(GameObject gameObject)
    {
        deadTxt = GameManager.GetDeadText();
        deadTxt.SetPosition(gameObject.transform.position);
    }
    #endregion




}
