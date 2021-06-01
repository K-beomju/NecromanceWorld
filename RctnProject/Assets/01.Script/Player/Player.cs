using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : LivingEntity
{


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TargetMove(); // 마우스 좌표로 움직이고 정규화 위치에 따라 방향전환
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }



        base.Attack("Enemy");

        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameManager.instance.crossHair.transform.position = new Vector2(targetPosition.x,targetPosition.y);


    }



    public void TargetMove()
    {
        base.Rotate();
        direction = (targetPosition - transform.position).normalized; // 마우스 좌표와 자기 위치의 거리를 정규화

    }



    protected override void Die()
    {
        //GameManager.CamShake(1f, 1f);
        gameObject.SetActive(false);
        GameManager.instance.playerGroup--;
        if(GameManager.instance.playerGroup <= 0)
        {
        GameManager.instance.crossHair.SetActive(false);
         Cursor.visible = true;
        }

    }






}
