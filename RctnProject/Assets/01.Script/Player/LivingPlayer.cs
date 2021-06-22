using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingPlayer : LivingEntity
{

    private Vector2 normalizedDirection;
    private Vector3 targetPosition;
    private Vector2 direction;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Rotate();
              anim.SetFloat("Speed", 1);
            GameManager.instance.crossTxt.gameObject.SetActive(false);
        }
        else
        {
             anim.SetFloat("Speed", 0);
        }

        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameManager.instance.crossHair.transform.position = new Vector2(targetPosition.x, targetPosition.y);

        anim.SetBool("Attack", isAttack);
        if (isAttack)
        {
            ChaseTarget(hitCollider);
        }
        SensingAttack();

    }

    private void SensingAttack()
    {
        if (!isDead)
        {
            hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, whatIsLayer);
            if (hitCollider)
            {
                moveSpeed = 1;
                isAttack = true;
            }
            else
            {
                moveSpeed = 4;
                isAttack = false;
            }
        }
    }


    protected override void Attack()
    {
        if (isAttack)
        {
            LivingEntity target = hitCollider.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                target.OnDamage(attackDamage);
            }

        }
    }

    private void ChaseTarget(Collider2D hitCollider)
    {
        if (hitCollider != null)
            transform.position = Vector2.MoveTowards(transform.position, hitCollider.transform.position, moveSpeed * Time.deltaTime);
    }



    public void Rotate()
    {

        normalizedDirection = new Vector2(direction.x, direction.y);
        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);
        direction = (targetPosition - transform.position).normalized;

        if (normalizedDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

    }

     public override void OnDamage(float damage)
    {
        if(this.gameObject.activeInHierarchy)
        {

        StartCoroutine(ChangeColor(myColor));
        }
        base.OnDamage(damage);
    }

    protected override void Die()
    {
        isDead = true;


        GameManager.instance.deadAudio.Play();
        OnNecroEffect(3);
        GameManager.CamShake(2f, 0.5f);
        gameObject.SetActive(false);

        // GameManager.instance.playerGroup--;
        // if (GameManager.instance.playerGroup <= 0)
        // {
        //     GameManager.instance.crossHair.SetActive(false);
        //     GameManager.instance.endPanel.SetActive(true);
        //     Cursor.visible = true;
        // }


        GameManager.instance.cinemachine.RemoveMember(this.gameObject.transform);

    }





      public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }


}
