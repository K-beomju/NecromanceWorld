using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class MouseClick : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyGroup[] enemyGroup;
    private LivingPlayer[] player;

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
            if(enemyManager.SearchGroup())
            {

            }
        }
    }


}
