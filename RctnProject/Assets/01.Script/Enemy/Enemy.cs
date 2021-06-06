using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public Player player;
    public Enemy enemy;

    void OnEnable()
    {
      // StartCoroutine(MoveEnemys());
    }

      public IEnumerator MoveEnemys()
    {
        while (!dead)
        {


            for (int i = 0; i < GameManager.instance.enemyGroup; i++) // 몇번째 그룹인지 검사
            {
                for (int j = 0; j < GameManager.instance.enemyStructs[i].list.Count; j++)
                {

                    if (GameManager.instance.enemyStructs[i].enemy[j] != null)
                    {
                        GameManager.instance.isRun = true;

                        GameManager.instance.enemyStructs[i].enemy[j].rig.velocity = (new Vector2(1, 1) * 1);




                    }
                }

            }


            yield return null;

        }

    }



    void Update()
    {
        base.Attack("Player");


        if (Input.GetMouseButton(1)) // 우클릭
        {
            if (dead)
            {
                for (int i = 0; i < GameManager.instance.enemyGroup; i++) // 몇번째 그룹인지 검사
                {
                    if (GameManager.instance.enemyStructs[i].list.Count == 0) // 그 그룹의 리스트가 비었다면
                    {

                        for (int j = 0; j <= GameManager.instance.enemyCount - 1; j++) // 그 그룹의 적 갯수만큼
                        {


                            anim.SetTrigger("Idle");

                            sprite.color = Color.white; // 플레리어 색상변환

                            player.enabled = true; // 플레이어 스크립트 활성화
                            capsule.enabled = true;
                            dead = false;
                            Destroy(enemy);

                            GameManager.CamShake(1f, 1f);

                        }
                    }
                }
            }
        }



        if(GameManager.instance.isRun)
        {
            anim.SetFloat("Speed", 1);
        }
         else
        {
            anim.SetFloat("Speed", 0);
        }




    }


    protected override void Die()
    {

        dead = true;
        anim.SetBool("isAttack", false);
        anim.SetTrigger("Dead");

        GameManager.instance.Dead(this); // 적 그룹 리스트에서 삭제
        gameObject.layer = 3; // 플레이어 레이어
        GameManager.instance.playerGroup++;  // 플레이어 그룹 증가

        capsule.enabled = false;
        GameManager.instance.isRun = false;

    }





}

