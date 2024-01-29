using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameMaster : MonoBehaviourPunCallbacks
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameUI gameUI;
    RuleBook ruleBook;
    bool playerRetryReady;
    bool enemyRetryReady;

    private void Awake()
    {
        ruleBook = GetComponent<RuleBook>();
    }

    private void Start()
    {
        Setup();
    }

    // �J�[�h�𐶐����Ĕz��
    private void Setup()
    {
        playerRetryReady = false;
        enemyRetryReady = false;
        gameUI.Setup();
        player.Life = 4;
        enemy.Life = 4;
        gameUI.ShowLifes(player.Life, enemy.Life);
        gameUI.UpdateBuffNumber(player.buffNumber, enemy.buffNumber);
        player.OnSubmitAction = SubmittedAction;
        enemy.OnSubmitAction = SubmittedAction;
        SetupCardTo(player , isEnemy: false);
        SetupCardTo(enemy, isEnemy: true);
        if (GameDataManager.Instance.IsOnlineBattle)
        {
            player.OnSubmitAction += SendPlayerCard;
        }

    }

    void SubmittedAction()
    {
        if (player.IsSubmitted && enemy.IsSubmitted)
        {
            StartCoroutine(CardBattle());
        }
        else if (player.IsSubmitted)
        {
            if (GameDataManager.Instance.IsOnlineBattle == false)
            {
                // enemy����J�[�h���o��
                enemy.RandomSubmit();
            }
        }
        else if (enemy.IsSubmitted)
        {
            // Player�̒�o���܂�
        }

    }

    private void SetupCardTo(Battler battler, bool isEnemy)
    {
        for (int i = 0; i < 8; i++)
        {
            Card card = cardGenerator.Spawn(i, isEnemy);
            battler.SetCardToHand(card);
        }
        battler.Hand.ResetPositions();
    }

    IEnumerator CardBattle()
    {
        yield return new WaitForSeconds(0.7f);
        enemy.SubmitCard.Open();
        yield return new WaitForSeconds(0.7f);
        Result result = ruleBook.GetResult(player, enemy);
        switch (result)
        {
            case Result.TurnWin:
                gameUI.ShowTurnResult("Win");
                enemy.Life--;
                break;
            case Result.TurnWin2:
                gameUI.ShowTurnResult("Win");
                enemy.Life -= 2;
                break;
            case Result.TurnLose:
                gameUI.ShowTurnResult("Lose");
                player.Life--;
                break;
            case Result.TurnLose2:
                gameUI.ShowTurnResult("Lose");
                player.Life -= 2;
                break;
            case Result.TurnDraw:
                gameUI.ShowTurnResult("Draw");
                break;
            case Result.GameWin:
            case Result.GameLose:
                ShowResult(result);
                break;
            default:
                break;
        }
        gameUI.ShowLifes(player.Life, enemy.Life);
        yield return new WaitForSeconds(1f);
        if (player.Life <= 0 && enemy.Life <= 0)
        {
            ShowResult(Result.GameDraw);
        }
        else if (player.Life <= 0)
        {
            ShowResult(Result.GameLose);
        }
        else if (enemy.Life <= 0)
        {
            ShowResult(Result.GameWin);
        }
        else if (player.Hand.CardCount == 0 && enemy.Hand.CardCount == 0)
        {
            if (player.Life < enemy.Life)
            {
                ShowResult(Result.GameLose);
            }
            else if (player.Life > enemy.Life)
            {
                ShowResult(Result.GameWin);
            }
            else
            {
                ShowResult(Result.GameDraw);
            }
        }
        else
        {
            SetupNextTurn();
        }
    }

    void ShowResult(Result result)
    {
        switch (result)
        {
            case Result.GameWin:
                gameUI.ShowGameResult("Win"); 
                break;
            case Result.GameLose:
                gameUI.ShowGameResult("Lose"); 
                break;
            case Result.GameDraw:
                gameUI.ShowGameResult("Draw"); 
                break;
            default:
                break;
        }
    }

    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        gameUI.SetupNextTurn();
        gameUI.UpdateBuffNumber(player.buffNumber, enemy.buffNumber);

        if (enemy.IsFirstSubmit)
        {
            enemy.IsFirstSubmit = false;
            if (GameDataManager.Instance.IsOnlineBattle == false)
            {
                enemy.RandomSubmit();
                enemy.SubmitCard.Open();
            }
        }
        if (player.IsFirstSubmit)
        {
            // TODO:�I�����C���Ή�
        }
    }

    public void OnRetryButton()
    {
        playerRetryReady = true;
        if (GameDataManager.Instance.IsOnlineBattle)
        {
            gameUI.HideRetryButton();
            SendRetryMessage();
            if (playerRetryReady && enemyRetryReady)
            {
                FadeManager.Instance.LoadScene("Game", 1f);
            }
        }
        else
        {
            FadeManager.Instance.LoadScene("Game", 1f);
        }
    }

    public void OnTitleButton()
    {
        // �ޏo�ʒm�������ɍs��
        if (GameDataManager.Instance.IsOnlineBattle)
        {
            gameUI.ShowLeavePanel();
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }
        }
        FadeManager.Instance.LoadScene("Title", 1f);
    }


    /// <summary>
    /// �������I�񂾃J�[�h�𑊎�ɒʒm����
    /// </summary>
    void SendPlayerCard()
    {
        // �֐� "RPC_OnRecievedCard()" �𑊎�̊��Ŏ��s����
        photonView.RPC(nameof(RPC_OnRecievedCard), RpcTarget.Others, player.SubmitCard.Base.Number);
    }

    /// <summary>
    /// [PunRPC] ����̊��Ŏ��s����֐�
    /// </summary>
    /// <param name="number"></param>
    [PunRPC]
    void RPC_OnRecievedCard(int number)
    {
        enemy.SetSubmitCard(number);
    }

    /// <summary>
    /// ���肪�ޏo�����C�x���g
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (GameDataManager.Instance.IsOnlineBattle)
        {
            gameUI.ShowLeavePanel();
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }
        }
    }

    /// <summary>
    /// �đI��]������
    /// </summary>
    void SendRetryMessage()
    {
        photonView.RPC(nameof(RPC_OnRecieveRetryMessage), RpcTarget.Others);
    }

    /// <summary>
    /// �đI���󂯎���
    /// </summary>
    [PunRPC]
    void RPC_OnRecieveRetryMessage()
    {
        enemyRetryReady = true;
        if (playerRetryReady)
        {
            FadeManager.Instance.LoadScene("Game", 1f);
        }
        else
        {
            gameUI.ShowRetryMessage();
        }
    }
}
