using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Serializable]
    public struct EnemyStruct
    {
        public Enemy[] enemy;
        public List<Enemy> list; // 몬스터의 수

    }
    public EnemyStruct[] enemyStructs = new EnemyStruct[10]; // 몬스터 그룹

    private Enemy enemyPool;
    private EnemyGroup enemyGroup;
    public Uipanel panel;



    private int enemyGroupCount; // 적 그룹
   // private int playerGroup; // 플레이어 그룹
    private int enemyCount; // 적 그룹안에 있는 적들의 갯수
    public Transform[] spawnPoint;
    private int spawn;




    void Start()
    {

        spawn = 0;
        enemyGroupCount = 0;
       // playerGroup = 5;
        enemyCount = 0;
        StartCoroutine(SpawnEnemys());
    }


    private IEnumerator SpawnEnemys()
    {
        while (true)
        {
            if (enemyGroupCount != 2)
            {
                int randCount = 4;//UnityEngine.Random.Range(playerGroup - 3, playerGroup + 1);
                enemyCount = randCount;
                enemyStructs[enemyGroupCount].enemy = new Enemy[enemyCount];
            //    enemyGroup = new EnemyGroup[enemyGroupCount];
                enemyGroup = GameManager.GetCreateGroup();
                for (int i = 0; i < enemyCount; i++)
                {
                    int t = UnityEngine.Random.Range(0, 360);
                    enemyStructs[enemyGroupCount].list.Add(enemyStructs[enemyGroupCount].enemy[i] = GameManager.GetCreateEnemy(0));
                    enemyStructs[enemyGroupCount].enemy[i].transform.parent = enemyGroup.transform;
                    enemyStructs[enemyGroupCount].enemy[i].transform.position =
                    new Vector2(spawnPoint[spawn].transform.position.x +
                     Mathf.Cos(t * 1), spawnPoint[spawn].transform.position.y +  Mathf.Sin(t * 1));
                     //   +     new Vector2(Mathf.Cos(t * 1), Mathf.Sin(t * 1));


                }
                enemyGroupCount++;
                spawn++;
            }
            yield return new WaitForSeconds(2f);

        }
    }


    public bool SearchGroup()
    {
        for (int i = 0; i < enemyGroupCount; i++) // 몇번째 그룹인지 검사
        {
            for (int j = 0; j <= enemyStructs[i].list.Count; j++) // 그 그룹의 적 갯수만큼
            {
                if (enemyStructs[i].list.Count == 0 ) // 그 그룹의 리스트가 비었다면  && enemyGroup.gameObject.transform.GetChild(j).gameObject.layer == LayerMask.NameToLayer("Player")
                {
                 //   enemyStructs[i].enemy =

                    return true;
                }
            }
        }
        return false;

    }



    public void Dead(Enemy _enemy)
    {
        for (int i = enemyGroupCount; i >= 0; i--)
        {
            if (enemyStructs[i].list.Count == 1)
            {
                panel = GameManager.GetDeadText();
                panel.SetPosition(new Vector3(_enemy.transform.position.x,_enemy.transform.position.y + 1f, _enemy.transform.position.z));

            }

            enemyStructs[i].list.Remove(_enemy);

        }

    }


}
