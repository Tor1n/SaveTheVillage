using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimer HarvestTimer;
    public ImageTimer RaidTimer;
    public Image RaidTimerImg;
    public Image PeasantTimerImg;
    public Image WarriorTimerImg;

    public Button peasantButton;
    public Button warriorButton;

    public Text resWorkerText;
    public Text resWarriorText;
    public Text resWheatText;

    public int peasantCount;
    public int warriorCount;
    public int wheatCount;

    public int wheatPerPeasant;
    public int wheatToWarroirs;

    public int peasantCost;
    public int warriorCost;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;
    public GameObject GameOver;

    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;


    void Start()
    {
        UpdateText();
        raidTimer = raidMaxTime;
    }

    
    void Update()
    {
        raidTimer -= Time.deltaTime;
        RaidTimerImg.fillAmount = raidTimer / raidMaxTime;

        if (raidTimer <= 0)
        {
            raidTimer = raidMaxTime;
            warriorCount -= nextRaid;
            nextRaid += raidIncrease;
        }
        if (HarvestTimer.Tick)
        {
            wheatCount += peasantCount * wheatPerPeasant;
            wheatCount -= warriorCount * wheatToWarroirs;
        }

        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            PeasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if (peasantTimer > -1)
        {
            PeasantTimerImg.fillAmount = 1;
            peasantButton.interactable = true;
            peasantCount += 1;
            peasantTimer = -2;
        }

        if (warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            WarriorTimerImg.fillAmount = warriorTimer / warriorCreateTime; 
        }
        else if (warriorTimer > -1)
        {
            WarriorTimerImg.fillAmount = 1;
            warriorButton.interactable = true;
            warriorCount += 1;
            warriorTimer = -2;
        }
        UpdateText();

        if (warriorCount < 0)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
        }
    }

    public void CreatePeasant()
    {
        wheatCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        peasantButton.interactable = false;
    }

    public void CreateWarrior()
    {
        wheatCount -= warriorCost;
        warriorTimer = warriorCreateTime;
        warriorButton.interactable = false;
    }

    private void UpdateText()
    {
        resWarriorText.text = warriorCount.ToString();
        resWheatText.text = wheatCount.ToString();
        resWorkerText.text = peasantCount.ToString();
    }
}
