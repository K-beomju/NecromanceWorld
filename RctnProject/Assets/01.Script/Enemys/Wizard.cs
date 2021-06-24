using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{

    public GameObject bullet;


    public void FireBullet()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }



}
