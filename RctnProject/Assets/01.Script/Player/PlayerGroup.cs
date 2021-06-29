using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;




public class PlayerGroup : MonoBehaviour
{
    private LivingPlayer living;
    public int grade;


    void Start()
    {

           for (int i = 0; i < 5; i++)
        {
            int t = UnityEngine.Random.Range(0, 361);
            living = GameManager.GetCreatePlayer(grade);
            living.SetPosition(this.gameObject.transform.position + new Vector3(Mathf.Cos(t) , Mathf.Sin(t) ));

            GameManager.instance.cinemachine.AddMember(living.transform,1,0);
        }
    }
}
