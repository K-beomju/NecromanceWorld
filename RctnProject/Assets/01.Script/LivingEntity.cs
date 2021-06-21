using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected SpriteRenderer sprite;


    protected Animator anim;
    protected Rigidbody2D rig;
    protected CircleCollider2D circle;


    private Collider2D hitCollider;



    protected bool isDead;
    public bool isAttack;

    protected float health;
    protected float attackRange;
    protected float attackDamage;
    protected float moveSpeed;






    [SerializeField]
    private AbilityData abilityData;
    public AbilityData AbilityData { set { abilityData = value; } }

    private EffectObject hitEffect;
    private Uipanel deadTxt;


    protected void Awake()
    {

        circle = GetComponent<CircleCollider2D>();
        anim = GetComponentInChildren<Animator>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();

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

    }



    public void Attack(string targetName)
    {
        int layerMask = 1 << LayerMask.NameToLayer(targetName);

        if ((hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, layerMask)) && !isDead)
        {
            moveSpeed = 0.55f;
            transform.position = Vector2.MoveTowards(transform.position, hitCollider.transform.position, moveSpeed * Time.deltaTime);
            anim.SetBool("Attack", true);
            isAttack = true;


        }
        else
        {
            moveSpeed = abilityData.MoveSpeed;
            anim.SetBool("Attack", false);
            isAttack = false;


        }
    }



    public void DAttack()
    {

        GameManager.instance.attackAudio[Random.Range(0,   GameManager.instance.attackAudio.Length)].Play();

        if (hitCollider != null)
        {
            if (hitCollider.transform.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            LivingEntity target = hitCollider.gameObject.GetComponent<LivingEntity>();
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
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
