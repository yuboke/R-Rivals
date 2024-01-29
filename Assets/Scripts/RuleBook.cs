using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{

    // カード効果
    // ・マジシャン（カード効果無効）がいれば数字対決
    // ・密偵（次のカードを先に出す）がいるなら追加効果
    // ・道化（このターン引き分け）がいるなら引き分け
    // ・暗殺者がいて、王子がいないなら数字効果反転
    // ・王子と姫がいるならゲームそのものの勝敗判定
    // ・ここまで来たら数字対決（大臣なら２倍）
    // ・将軍（次のカード+2）

    public Result GetResult(Battler player, Battler enemy)
    {
        // 大臣判定
        bool existMinister = false;
        if (player.SubmitCard.Base.Type == CardType.Minister || enemy.SubmitCard.Base.Type == CardType.Minister)
        {
            existMinister = true;
        }

        // マジシャン
        if (player.SubmitCard.Base.Type == CardType.Magician || enemy.SubmitCard.Base.Type == CardType.Magician)
        {
            // 大臣効果も無効
            return NumberBattle(player.SubmitCard.Base, enemy.SubmitCard.Base , false, player.buffNumber, enemy.buffNumber);
        }

        // 密偵
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

        // 将軍
        if (player.SubmitCard.Base.Type == CardType.General)
        {
            player.IsBuffNumber = true;
        }
        if (enemy.SubmitCard.Base.Type == CardType.General)
        {
            enemy.IsBuffNumber = true;
        }

        // 道化
        if (player.SubmitCard.Base.Type == CardType.Clown || enemy.SubmitCard.Base.Type == CardType.Clown)
        {
            return Result.TurnDraw;
        }

        // 暗殺者
        if (player.SubmitCard.Base.Type == CardType.Assassin || enemy.SubmitCard.Base.Type == CardType.Assassin)
        {
            if (player.SubmitCard.Base.Type != CardType.Prince && enemy.SubmitCard.Base.Type != CardType.Prince)
            {
                return NumberReverseBattle(player.SubmitCard.Base, enemy.SubmitCard.Base, existMinister, player.buffNumber, enemy.buffNumber);
            }
        }

        // 王子と姫
        if (player.SubmitCard.Base.Type == CardType.Prince && enemy.SubmitCard.Base.Type == CardType.Princess)
        {
            return Result.GameLose;
        }
        else if (player.SubmitCard.Base.Type == CardType.Princess && enemy.SubmitCard.Base.Type == CardType.Prince)
        {
            return Result.GameWin;
        }

        // 数字対決
        return NumberBattle(player.SubmitCard.Base, enemy.SubmitCard.Base, existMinister, player.buffNumber, enemy.buffNumber);


    }

    Result NumberBattle(CardBase playerCard, CardBase enemyCard , bool existMinister, int playerBuff, int enemyBuff)
    {
        // 大臣効果
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
        // 大臣効果
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
