using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class MouseClick : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyGroup[] enemyGroup;
    private LivingPlayer[] player;
    public GameObject deadTxtObj;

    void Start()
    {
        player = GetComponentsInChildren<LivingPlayer>(true);
        enemyGroup = GetComponentsInChildren<EnemyGroup>(true); // includeInactive 비활성 포함
        enemyManager = GetComponent<EnemyManager>();
    }


    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            for (int k = 0; k < deadTxtObj.transform.childCount; k++)
            {
                if( deadTxtObj.transform.GetChild(k).gameObject.activeSelf)
                {
                     deadTxtObj.transform.GetChild(k).gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < enemyManager.remainingEnemy; i++)
            {
                if(enemyManager.enemyStructs[i].list.Count == 0)
                {
                    for (int j = 0; j < enemyGroup[i].transform.childCount; j++)
                    {
                        if(!enemyGroup[i].transform.GetChild(j).gameObject.activeSelf)
                        {
                        enemyGroup[i].gameObject.SetActive(false);
                        enemyGroup[i].transform.GetChild(j).transform.GetComponent<Enemy>().OnNecromance();
                        GameManager.CamShake(5,0.5f);

                        }
                    }

                }

            }
        }
    }


}
