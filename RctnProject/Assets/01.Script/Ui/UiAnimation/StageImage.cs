using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageImage : MonoBehaviour
{
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        Move();
    }



    public void Move()
    {
        rect.DOAnchorPosY(434 , 1).OnComplete(() => rect.DOAnchorPosY(600 , 1).SetDelay(3));
    }
}
