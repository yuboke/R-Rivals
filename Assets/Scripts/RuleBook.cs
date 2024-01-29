using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{

    // �J�[�h����
    // �E�}�W�V�����i�J�[�h���ʖ����j������ΐ����Ό�
    // �E����i���̃J�[�h���ɏo���j������Ȃ�ǉ�����
    // �E�����i���̃^�[�����������j������Ȃ��������
    // �E�ÎE�҂����āA���q�����Ȃ��Ȃ琔�����ʔ��]
    // �E���q�ƕP������Ȃ�Q�[�����̂��̂̏��s����
    // �E�����܂ŗ����琔���Ό��i��b�Ȃ�Q�{�j
    // �E���R�i���̃J�[�h+2�j

    public Result GetResult(Battler player, Battler enemy)
    {
        // ��b����
        bool existMinister = false;
        if (player.SubmitCard.Base.Type == CardType.Minister || enemy.SubmitCard.Base.Type == CardType.Minister)
        {
            existMinister = true;
        }

        // �}�W�V����
        if (player.SubmitCard.Base.Type == CardType.Magician || enemy.SubmitCard.Base.Type == CardType.Magician)
        {
            // ��b���ʂ�����
            return NumberBattle(player.SubmitCard.Base, enemy.SubmitCard.Base , false, player.buffNumber, enemy.buffNumber);
        }

        // ����
        if (player.SubmitCard.Base.Type == CardType.Spy)
        {
            enemy.IsFirstSubmit = true;
        }
        if (enemy.SubmitCard.Base.Type == CardType.Spy)
        {
            player.IsFirstSubmit = true;
        }
        if (player.SubmitCard.Base.Type == CardType.Spy && enemy.SubmitCard.Base.Type == CardType.Spy)
        {
            player.IsFirstSubmit = false;
            enemy.IsFirstSubmit = false;
        }

        // ���R
        if (player.SubmitCard.Base.Type == CardType.General)
        {
            player.IsBuffNumber = true;
        }
        if (enemy.SubmitCard.Base.Type == CardType.General)
        {
            enemy.IsBuffNumber = true;
        }

        // ����
        if (player.SubmitCard.Base.Type == CardType.Clown || enemy.SubmitCard.Base.Type == CardType.Clown)
        {
            return Result.TurnDraw;
        }

        // �ÎE��
        if (player.SubmitCard.Base.Type == CardType.Assassin || enemy.SubmitCard.Base.Type == CardType.Assassin)
        {
            if (player.SubmitCard.Base.Type != CardType.Prince && enemy.SubmitCard.Base.Type != CardType.Prince)
            {
                return NumberReverseBattle(player.SubmitCard.Base, enemy.SubmitCard.Base, existMinister, player.buffNumber, enemy.buffNumber);
            }
        }

        // ���q�ƕP
        if (player.SubmitCard.Base.Type == CardType.Prince && enemy.SubmitCard.Base.Type == CardType.Princess)
        {
            return Result.GameLose;
        }
        else if (player.SubmitCard.Base.Type == CardType.Princess && enemy.SubmitCard.Base.Type == CardType.Prince)
        {
            return Result.GameWin;
        }

        // �����Ό�
        return NumberBattle(player.SubmitCard.Base, enemy.SubmitCard.Base, existMinister, player.buffNumber, enemy.buffNumber);


    }

    Result NumberBattle(CardBase playerCard, CardBase enemyCard , bool existMinister, int playerBuff, int enemyBuff)
    {
        // ��b����
        if (existMinister == false)
        {
            if (playerCard.Number + playerBuff > enemyCard.Number + enemyBuff)
            {
                return Result.TurnWin;
            }
            else if (playerCard.Number + playerBuff < enemyCard.Number + enemyBuff)
            {
                return Result.TurnLose;
            }
        }
        else
        {
            if (playerCard.Number + playerBuff > enemyCard.Number + enemyBuff)
            {
                return Result.TurnWin2;
            }
            else if (playerCard.Number + playerBuff < enemyCard.Number + enemyBuff)
            {
                return Result.TurnLose2;
            }
        }
        return Result.TurnDraw;
    }


    Result NumberReverseBattle(CardBase playerCard, CardBase enemyCard, bool existMinister, int playerBuff, int enemyBuff)
    {
        // ��b����
        if (existMinister == false)
        {
            if (playerCard.Number + playerBuff > enemyCard.Number + enemyBuff)
            {
                return Result.TurnLose;
            }
            else if (playerCard.Number + playerBuff < enemyCard.Number + enemyBuff)
            {
                return Result.TurnWin;
            }
        }
        else
        {
            if (playerCard.Number + playerBuff > enemyCard.Number + enemyBuff)
            {
                return Result.TurnLose2;
            }
            else if (playerCard.Number + playerBuff < enemyCard.Number + enemyBuff)
            {
                return Result.TurnWin2;
            }
        }
        return Result.TurnDraw;
    }

}

public enum Result
{
    TurnWin,
    TurnLose,
    TurnDraw,
    TurnWin2,
    TurnLose2,
    GameWin,
    GameLose,
    GameDraw,
}
