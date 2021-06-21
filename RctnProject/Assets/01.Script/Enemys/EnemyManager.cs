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
    [HideInInspector]
    public Uipanel panel;



    public int enemyGroupCount; // 적 그룹
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
            yield return new WaitForSeconds(1f);
            if (enemyGroupCount != 8  )
            {
                int randCount = UnityEngine.Random.Range(2,5);
                enemyCount = randCount;
                enemyStructs[enemyGroupCount].enemy = new Enemy[enemyCount];

                enemyGroup = GameManager.GetCreateGroup();


                 for (int i = 0; i < enemyCount; i++)
                 {
                     int t = UnityEngine.Random.Range(0, 361);
                    enemyStructs[enemyGroupCount].list.Add(enemyStructs[enemyGroupCount].enemy[i] = GameManager.GetCreateEnemy(0));
                     enemyStructs[enemyGroupCount].enemy[i].gameObject.transform.parent = enemyGroup.transform;
                    enemyStructs[enemyGroupCount].enemy[i].transform.position +=  new Vector3( Mathf.Cos(t) /2 ,  Mathf.Sin(t) /2);
                 }

                enemyGroup.SetPositionData( new Vector2(spawnPoint[spawn].transform.position.x ,spawnPoint[spawn].transform.position.y ),Quaternion.identity);



            enemyGroupCount = (enemyGroupCount + 1) % 9;
               spawn = (spawn + 1) % spawnPoint.Length;


            }


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
