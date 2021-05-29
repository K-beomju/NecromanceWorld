using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public Player player;
    public Enemy enemy;




    void Update()
    {
        base.Attack("Player");
    }


    protected override void Die()
    {
        dead = true;
        gameObject.layer = 3;
        player.enabled = true;
        enemy.enabled = false;
        sprite.color = Color.white;

       GameManager.instance.Dead(this);
       Destroy(enemy);
    }


}

