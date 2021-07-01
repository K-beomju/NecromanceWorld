using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eWizard : MonoBehaviour
{

    public GameObject bullet;
    public WizardBullet wizardBullet;
    public Enemy enemy;


    void Start()
    {
        wizardBullet.targetName = "Player";
        wizardBullet.attackDamage = enemy.attackDamage;
    }

    public void FireBullet()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }



}
