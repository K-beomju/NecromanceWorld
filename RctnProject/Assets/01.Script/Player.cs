using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : LivingEntity
{


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Rotate();
        }
        base.Attack("Enemy");
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameManager.instance.crossHair.transform.position = new Vector2(targetPosition.x,targetPosition.y);


    }

   public void Rotate()
    {

        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);
        direction = (targetPosition - transform.position).normalized;
        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);
        normalizedDirection = new Vector2(direction.x, direction.y);
        if (normalizedDirection.x > 0)
        {

            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;

        }
    }

    protected override void Die()
    {
          OnNecroEffect(3);
        GameManager.CamShake(2f, 0.5f);
        gameObject.SetActive(false);
        GameManager.instance.playerGroup--;
        if(GameManager.instance.playerGroup <= 0)
        {
        GameManager.instance.crossHair.SetActive(false);
         Cursor.visible = true;
        }

    }









     // public void AddExplosion(Vector3 pos , float power)
    // {
    //     Vector3 dir = transform.position - pos;
    //     power *= 1/dir.sqrMagnitude;
    //     rig.AddForce(dir.normalized * power, ForceMode2D.Impulse);
    // }


}
