using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public Player player;
    public Enemy enemy;
    private Uipanel panel;
    private float Speed = 0.55f;

    protected override void Start()
    {
       // StartCoroutine(MoveEnemys());
       // StartCoroutine(randomPosition());
    }


    public IEnumerator MoveEnemys()
    {
        while (!isDead && this.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            if (!isAttack)
            {
                for (int i = 0; i < GameManager.instance.enemyGroup; i++) // 몇번째 그룹인지 검사
                {
                    for (int j = 0; j < GameManager.instance.enemyStructs[i].list.Count; j++)
                    {

                        if (GameManager.instance.enemyStructs[i].enemy[j] != null)
                        {
                            if (GameManager.instance.moveVector == new Vector2(0, 0))
                            {
                                anim.SetFloat("Speed", 0);
                            }
                            if (GameManager.instance.x < 0)
                            {
                                sprite.flipX = true;
                            }
                            else
                            {
                                sprite.flipX = false;
                            }
                            GameManager.instance.isRun = true;

                            GameManager.instance.enemyStructs[i].enemy[j].transform.Translate((GameManager.instance.moveVector).normalized * Speed * Time.deltaTime);
                        }
                    }

                }
            }
            else{

            }


            yield return null;

        }

    }



    void Update()
    {


        base.Attack("Player");

        if(hitCollider)
        {
            transform.position = Vector2.MoveTowards(transform.position , hitCollider.transform.position ,Speed * Time.deltaTime);
        }


        if (Input.GetMouseButton(1)) // 우클릭
        {
            if (isDead)
            {
                for (int i = 0; i < GameManager.instance.enemyGroup; i++) // 몇번째 그룹인지 검사
                {
                    if (GameManager.instance.enemyStructs[i].list.Count == 0) // 그 그룹의 리스트가 비었다면
                    {
                        for (int j = 0; j < GameManager.instance.enemyCount - 1; j++) // 그 그룹의 적 갯수만큼
                        {
                            if (GameManager.instance.enemyStructs[i].enemy[j] == isDead)
                            {

                                anim.SetTrigger("Idle");

                                sprite.color = Color.white; // 플레리어 색상변환
                                OnNecroEffect(1);
                                // panel = GameManager.GetDeadText();
                                // panel.SetPosition(new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z));
                                player.enabled = true; // 플레이어 스크립트 활성화
                                circle.enabled = true;
                                isDead = false;
                                Destroy(enemy);

                                GameManager.CamShake(2f, 0.5f);
                            }


                        }
                    }
                }
            }
        }




        if (GameManager.instance.isRun)
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
        StopAllCoroutines();
        isDead = true;
        anim.SetBool("isAttack", false);
        anim.SetTrigger("Dead");
        Speed = 0;
        gameObject.layer = 3; // 플레이어 레이어
        GameManager.instance.playerGroup++;  // 플레이어 그룹 증가
        OnNecroEffect(2);

        circle.enabled = false;
        GameManager.instance.isRun = false;
        GameManager.instance.Dead(this); // 적 그룹 리스트에서 삭제
        GameManager.instance.isRun = false;

    }







}

