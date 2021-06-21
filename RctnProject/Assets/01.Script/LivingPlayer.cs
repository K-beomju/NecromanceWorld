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
        base.Attack("Enemy");
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameManager.instance.crossHair.transform.position = new Vector2(targetPosition.x,targetPosition.y);



    }




     public void Rotate()
    {

        normalizedDirection = new Vector2(direction.x, direction.y);
        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);
        direction = (targetPosition - transform.position).normalized;



        if (normalizedDirection.x > 0)
           transform.localScale = new Vector3(1,1,1);
        else
            transform.localScale = new Vector3(-1,1,1);

    }


    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }


    protected override void Die()
    {
        GameManager.instance.deadAudio.Play();
          OnNecroEffect(3);
        GameManager.CamShake(1f, 0.5f);
        gameObject.SetActive(false);

        // if(GameManager.instance.playerGroup <= 0)
        // {
        // GameManager.instance.crossHair.SetActive(false);
        //  Cursor.visible = true;
        // }


        GameManager.instance.cinemachine.RemoveMember(this.gameObject.transform);

    }

}
