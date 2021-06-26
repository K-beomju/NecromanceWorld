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
        else
        {
            anim.SetBool("Idle", false);
        }


        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameManager.instance.crossHair.transform.position = new Vector2(targetPosition.x, targetPosition.y);

        anim.SetBool("Attack", isAttack);
        SensingAttack();

    }







    public void Rotate()
    {
        if(Vector3.Distance(targetPosition , transform.position) > 4)
        {
            anim.SetBool("Idle", true);
          transform.position = Vector2.MoveTowards(transform.position, targetPosition , moveSpeed * Time.deltaTime);

        }
        else
        {
              anim.SetBool("Idle", false);
        }

        normalizedDirection = new Vector2(direction.x, direction.y);
        direction = (targetPosition - transform.position).normalized;

    }


    public void DelayPos()
    {
        if (normalizedDirection.x > 0)
            transform.localScale = new Vector2(1, 1);
        else
            transform.localScale = new Vector2(-1,1);
    }

    protected override void Attack()
    {
        base.Attack();
              GameManager.instance.attackAudio[UnityEngine.Random.Range(0,GameManager.instance.attackAudio.Length)].Play();
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
