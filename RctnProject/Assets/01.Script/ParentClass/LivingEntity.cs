using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected CircleCollider2D circle;
    protected Rigidbody2D rig;
    protected Collider2D hitCollider;
    public LayerMask whatIsLayer;


    public bool isDead { get; protected set; }
    public bool isAttack { get; protected set; }
    public bool isIdle { get; protected set; }

    [SerializeField] private float health;
    public float attackDamage;
    protected float attackRange;
    protected float moveSpeed;

    [SerializeField]
    private AbilityData abilityData;
    public AbilityData AbilityData { set { abilityData = value; } }

    private EffectObject hitEffect;
    private Uipanel deadTxt;
    public Vector3 offset;
    protected Vector3 knifeLocalPos;

    public int mobGrade;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        if(mobGrade <= 2)
        {
        knifeLocalPos = gameObject.transform.GetChild(0).localPosition;
        }


    }

    protected virtual void OnEnable()
    {
        isDead = false;
        sprite.color = Color.white;
        transform.position = Vector2.zero;
    }





    public void SetAbility(float upHealth, float upAttackDmg)
    {
        health = abilityData.Health + upHealth;
        attackDamage = abilityData.AttackDamage + upAttackDmg;
        attackRange = abilityData.AttackRange;
    }

    public virtual void OnDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }


    protected abstract void Die();

    protected virtual void Attack()
    {

        if (isAttack)
        {
            if (hitCollider.transform.position.x > transform.position.x)
            {

                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {

                transform.localScale = new Vector3(-1, 1, 1);
            }



            LivingEntity target = hitCollider.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                target.OnDamage(attackDamage);
            }

        }
    }

    protected void SensingAttack()
    {
        if (!isDead)
        {
            hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, whatIsLayer);
            if (hitCollider)
            {
                transform.position = Vector2.MoveTowards(transform.position, hitCollider.transform.position, moveSpeed * Time.deltaTime);
                moveSpeed = 0.1f;
                isAttack = true;
            }
            else
            {
                moveSpeed = 3;
                isAttack = false;
            }
        }
    }






    protected IEnumerator ChangeColor()
    {
        sprite.color = new Color(255 / 255f, 133 / 255f, 133 / 255f, 255 / 255f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + offset, attackRange);
    }








    #region 풀링 오브젝트
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
