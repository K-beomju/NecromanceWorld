using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pWizard : MonoBehaviour
{

    public GameObject bullet;
    public WizardBullet wizardBullet;
    public LivingPlayer livingPlayer;


    void Start()
    {
        wizardBullet.targetName = "Enemy";
        wizardBullet.attackDamage = livingPlayer.attackDamage;
    }

    public void FireBullet()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }



}
