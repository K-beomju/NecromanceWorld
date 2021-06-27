using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public EnemyManager enemyManager;

    [Header("Stage Panel")]
    public Image stageImage;
    public Text stageText;
    private int countStage = 0;


    [Header("Shop Panel")]
    public GameObject shopPanel;
    private bool isShop;
    public Button shopBtn;

    [Serializable]
    public struct Upgrade
    {
        [Header("업그레이드")]
        public Button upgradeBtn; // 업그레이드 버튼
        public Text upgradeTxt; // A / MOBMAXPIECE
        public int mobMaxPiece;
        [Header("능력치")]
        [Space(20)]
        public Text abilityText; // 능력치 텍스트
        public string mobName; // 몹의 이름
        public AbilityData abilityData; // 몹의 데이터
        [Header("몹의 레벨")]
        [Space(20)]
        public Text mobLevelText;
        public int mobLevel;
    }
    public Upgrade[] upgrade;
    public int[] mobPiece; // 현재의 몹 조각갯수

    private float[] mobHealth; // 몹의 생명력
    public float[] currentMobHealth; // 현재 몹의 생명력

    private float[] mobAttackDamage;
    public float[] currentMobAttackDamage;






    [Header("Setting Panel")]
    public GameObject settingPanel;
    private bool isSetting;
    public Button musicBtn;
    public Text musicTxt;
    public Button exitBtn;




    void Awake()
    {
        instance = this;
        isSetting = false;
        isShop = false;
    }

    void Start()
    {
        mobHealth = new float[upgrade.Length];
        mobAttackDamage = new float[upgrade.Length];

        for (int i = 0; i < upgrade.Length; i++)
        {
            int index = i;
            upgrade[i].upgradeBtn.onClick.AddListener(() => this.UpGradeMobAbility(index));
            mobHealth[i] = upgrade[i].abilityData.Health; // 능력치의 체력을 가져옴
            mobAttackDamage[i] = upgrade[i].abilityData.AttackDamage;
        }


        shopBtn.onClick.AddListener(ShopBtn);
        exitBtn.onClick.AddListener(ExitGame);
        StageClearEvent();
        ShowMobPieces();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isShop)
            {
                Cursor.visible = true;
                shopPanel.SetActive(true);
                isShop = true;
            }
            else
            {
                Cursor.visible = false;
                shopPanel.SetActive(false);
                isShop = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isSetting)
            {
                Cursor.visible = true;
                settingPanel.SetActive(true);
                isSetting = true;
            }
            else
            {
                Cursor.visible = false;
                settingPanel.SetActive(false);
                isSetting = false;
            }
        }
    }

    private void ShopBtn()
    {
        shopPanel.SetActive(false);

        // StageClearEvent();
        // enemyManager.EnemySpawner();
    }

    private void StageClearEvent()
    {
        countStage++;
        stageText.text = ($"  STAGE  {countStage}");
        stageImage.rectTransform.DOAnchorPosY(434, 1).OnComplete(() => stageImage.rectTransform.DOAnchorPosY(600, 1).SetDelay(2));

    }

    private void ExitGame() // 환경 설정 나가기
    {
        Application.Quit();
    }


    public void ShowMobPieces()
    {
        for (int i = 0; i < upgrade.Length; i++)
        {
            upgrade[i].upgradeTxt.text = ($"{mobPiece[i]} / {upgrade[i].mobMaxPiece}");

            if (upgrade[i].mobLevel % 5 == 0)
            {
                upgrade[i].abilityText.text =
          ($"이름 : {upgrade[i].mobName}\n생명력 : {mobHealth[i]}\n공격력 : {mobAttackDamage[i]}<color=#00ff00> -> {mobAttackDamage[i] + (i + 1) * 0.5f} </color>");

            }
            else
            {
                upgrade[i].abilityText.text =
                 ($"이름 : {upgrade[i].mobName}\n생명력 : {mobHealth[i]}<color=#00ff00> -> {mobHealth[i] + (i + 1) * 2}  </color>\n공격력 : {mobAttackDamage[i]}");

            }
            upgrade[i].mobLevelText.text = ($"Lv.{upgrade[i].mobLevel}");
        }
    }

    public void UpDateMobPieces(int upgrade) // 죽은 적 유닛의 등급
    {
        mobPiece[upgrade]++;
        ShowMobPieces();
    }

    public void UpGradeMobAbility(int index)
    {
        if (mobPiece[index] >= upgrade[index].mobMaxPiece)
        {
            mobPiece[index] -= upgrade[index].mobMaxPiece;
            if (upgrade[index].mobLevel % 5 == 0)
            {
                mobAttackDamage[index] += (index + 1) * 0.5f;
                currentMobAttackDamage[index] = mobAttackDamage[index] - upgrade[index].abilityData.AttackDamage;
            }
            else
            {
                mobHealth[index] += (index + 1) * 2;
                currentMobHealth[index] = mobHealth[index] - upgrade[index].abilityData.Health;
            }
              upgrade[index].mobLevel++;
            ShowMobPieces();

        }

    }
}
