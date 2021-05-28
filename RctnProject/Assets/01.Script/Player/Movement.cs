using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : LivingEntity
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TargetMove(); // 마우스 좌표로 움직이고 정규화 위치에 따라 방향전환
            anim.SetFloat("Speed",1);
        }
        else
        {
            anim.SetFloat("Speed",0);
        }
    }


    public void TargetMove()
    {
        base.Rotate();
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (targetPosition - transform.position).normalized; // 마우스 좌표와 자기 위치의 거리를 정규화


    }


}
