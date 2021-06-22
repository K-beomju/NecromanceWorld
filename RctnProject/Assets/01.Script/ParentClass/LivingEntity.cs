using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//생명체로 동작할 게임 오브젝트들의 뼈대
//체력, 피해받음, 사망 기능, 사망 , 공격

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected SpriteRenderer sprite; // 마우스 위치 비교, 어디서 공격하는지 비교, 적 그룹 순찰할때 비교
    protected Animator anim; // 대기 , 달리기 , 공격
    protected CircleCollider2D circle; // 죽으면 콜라이더 꺼줌
    protected Collider2D hitCollider; // 공격 감지 콜라이더 ,추적
    public LayerMask whatIsLayer;
    public Color myColor;


    public bool isDead { get; protected set;}
    public bool isAttack { get; protected set;}

    protected float health;
    protected float attackRange;
    protected float attackDamage;
    protected float moveSpeed;

    [SerializeField]
    private AbilityData abilityData;
    public AbilityData AbilityData { set { abilityData = value; } }

    private EffectObject hitEffect;
    private Uipanel deadTxt;
    public Vector3 offset;


    protected void Awake()
    {
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        isDead = false;
        SetAbility();
    }

     public void SetAbility()
    {
        health = abilityData.Health;
        attackRange = abilityData.AttackRange;
        attackDamage = abilityData.AttackDamage;
        moveSpeed = abilityData.MoveSpeed;
    }

    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if (health <= 0  && !isDead)
        {
            Die();
        }
    }


    protected abstract void Die();

    protected abstract void Attack();


    protected IEnumerator ChangeColor(Color color)
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = color;
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











    // public void DAttack()
    // {

    //     GameManager.instance.attackAudio[Random.Range(0,   GameManager.instance.attackAudio.Length)].Play();

    //     if (hitCollider != null)
    //     {
    //         if (hitCollider.transform.position.x >= transform.position.x)
    //         {
    //           transform.localScale = new Vector3(1,1,1);
    //         }
    //         else
    //         {
    //             transform.localScale = new Vector3(-1,1,1);
    //         }
    //         LivingEntity target = hitCollider.gameObject.GetComponent<LivingEntity>();
    //         if (target != null)
    //         {
    //             target.OnDamage(1);
    //         }

    //     }

    // }


    //     public void Attack(string targetName)
    // {
    //     int layerMask = 1 << LayerMask.NameToLayer(targetName);

    //     if ((hitCollider = Physics2D.OverlapCircle(transform.position + offset, attackRange, layerMask)) && !isDead)
    //     {
    //        //  transform.position = Vector2.MoveTowards(transform.position, hitCollider.transform.position, moveSpeed * Time.deltaTime);
    //         moveSpeed = 0.55f;
    //         anim.SetBool("Attack", true);
    //         isAttack = true;


    //     }
    //     else
    //     {
    //         moveSpeed = abilityData.MoveSpeed;
    //         anim.SetBool("Attack", false);
    //         isAttack = false;


    //     }
    // }


}
