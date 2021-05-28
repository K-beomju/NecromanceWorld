using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    protected Animator anim;
    protected Vector2 normalizedDirection;
    protected Vector2 direction;

    public float playerSpeed;
    public float attackRange;

    void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public void Rotate()
    {
        transform.Translate(normalizedDirection  * playerSpeed * Time.deltaTime);
        normalizedDirection = new Vector2(direction.x,direction.y);
        if(normalizedDirection.x > 0)
        {
            //SpriteRender가져와서 FlipX해도 가능
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
             transform.localScale = new Vector3(-1,1,1);
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
