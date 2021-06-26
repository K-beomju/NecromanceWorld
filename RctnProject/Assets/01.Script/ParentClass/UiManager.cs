using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public EnemyManager enemyManager;

    void Awake()
    {
        instance = this;

    }


    public Image stageImage;
    public Text stageText;
    public int countStage = 1;

    public GameObject shopPanel;
    public Button shopBtn;


    void Start()
    {
        shopBtn.onClick.AddListener( NextStage);
       Move();
    }


    public void Move()
    {
         stageText.text = ($"STAGE   {countStage}");
        stageImage.rectTransform.DOAnchorPosY(434 , 1).OnComplete(() =>  stageImage.rectTransform.DOAnchorPosY(600 , 1).SetDelay(2));
    }

    public void StageClearEvent()
    {
        countStage++;
        Move();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(shopPanel.gameObject.activeSelf)
            {

                 NextStage();

            }
        }
    }

    public void NextStage()
    {
          shopPanel.gameObject.SetActive(false);
                StageClearEvent();
                enemyManager.EnemySpawner();
    }
}
