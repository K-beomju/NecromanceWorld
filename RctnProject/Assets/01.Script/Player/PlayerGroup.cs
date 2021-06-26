using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    [HideInInspector] public Vector2 normalizedDirection;
    public Vector3 targetPosition;
    private Vector2 direction;



    void Update()
    {
        if(Input.GetMouseButton(0))
        {
             Rotate();

        }
        targetPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
        GameManager.instance.crossHair.transform.position = new Vector2(targetPosition.x, targetPosition.y);
    }


     public void Rotate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
             transform.GetChild(i).localPosition = Vector2.MoveTowards( transform.GetChild(i).localPosition,  targetPosition,  3 * Time.deltaTime);
        }
        direction = (targetPosition - transform.position).normalized;
        normalizedDirection = new Vector2(direction.x, direction.y);

    }
}
