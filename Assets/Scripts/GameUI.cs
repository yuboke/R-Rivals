using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text turnResultText;
    [SerializeField] Text playerLifeText;
    [SerializeField] Text enemyLifeText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject resultPanelRetryButton;
    [SerializeField] Text resultText;
    [SerializeField] GameObject playerBuffNumber;
    [SerializeField] GameObject enemyBuffNumber;
    [SerializeField] GameObject rulePanel;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject retryMessage;
    [SerializeField] GameObject retryButton;
    public void Setup()
    {
        turnResultText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
        leavePanel.SetActive(false);
        retryMessage.SetActive(false);
        resultPanelRetryButton.SetActive(true);
        if (GameDataManager.Instance.IsOnlineBattle)
        {
            retryButton.SetActive(false);
        }
    }
    public void UpdateBuffNumber(int playerBuff, int enemyBuff)
    {
        if (playerBuff == 2)
        {
            playerBuffNumber.SetActive(true);
        }
        else
        {
            playerBuffNumber.SetActive(false);
        }
        if (enemyBuff == 2)
        {
            enemyBuffNumber.SetActive(true);
        }
        else
        {
            enemyBuffNumber.SetActive(false);
        }

    }

    public void ShowLifes(int playerLife, int enemyLife)
    {
        playerLifeText.text = $"x {playerLife}";
        enemyLifeText.text = $"x {enemyLife}";
    }

    public void ShowTurnResult(string result)
    {
        turnResultText.gameObject.SetActive(true);
        turnResultText.text = result;
    }

    public void ShowGameResult(string result)
    {
        playerBuffNumber.SetActive(false);
        enemyBuffNumber.SetActive(false);
        resultPanel.SetActive(true);
        resultText.text = result;
    }

    public void SetupNextTurn()
    {
        turnResultText.gameObject.SetActive(false);
    }

    public void OnRuleButton()
    {
        rulePanel.SetActive(true);
        rulePanel.transform.localScale = Vector3.zero;
        rulePanel.transform.DOScale(1, 0.1f);
    }

    public void OnCloseButton()
    {
        rulePanel.transform.DOScale(0, 0.1f);
    }

    public void ShowLeavePanel()
    {
        leavePanel.SetActive(true);
    }
    public void ShowRetryMessage()
    {
        retryMessage.SetActive(true);
    }

    public void HideRetryButton()
    {
        resultPanelRetryButton.SetActive(false);
    }
}
