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
            GameManager.instance.crossTxt.gameObject.SetActive(false);
        }


        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameManager.instance.crossHair.transform.position = new Vector2(targetPosition.x, targetPosition.y);

        anim.SetBool("Attack", isAttack);
        SensingAttack();

    }

    private void SensingAttack()
    {
        if (!isDead)
        {
            hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, whatIsLayer);
            if (hitCollider)
            {
                transform.position = Vector2.MoveTowards(transform.position, hitCollider.transform.position, moveSpeed * Time.deltaTime);
                moveSpeed = 0.55f;
                isAttack = true;
            }
            else
            {
                moveSpeed = 3;
                isAttack = false;
            }
        }
    }






    public void Rotate()
    {
        if(Vector3.Distance(targetPosition , transform.position) > 4)
        {
              anim.SetBool("isIdle", true);
        //  transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);
          transform.position = Vector2.MoveTowards(transform.position, targetPosition , moveSpeed * Time.deltaTime);

        }
        else
        {
              anim.SetBool("isIdle", false);
        }



        normalizedDirection = new Vector2(direction.x, direction.y);
        direction = (targetPosition - transform.position).normalized;

        if (normalizedDirection.x > 0)
            transform.localScale = new Vector2(ChangePos.x, ChangePos.y);
        else
            transform.localScale = new Vector2(-ChangePos.x,ChangePos.y);

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
        GameManager.instance.cinemachine.RemoveMember(this.gameObject.transform);

    }





      public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }


}
