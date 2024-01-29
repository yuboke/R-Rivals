using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPostion submitPostion;
    [SerializeField] GameObject submitButton;

    public UnityAction OnSubmitAction;

    public BattlerHand Hand { get => hand; }
    public bool IsSubmitted { get; private set; }
    public bool IsFirstSubmit { get; set; }
    public bool IsBuffNumber { get; set; }
    public int buffNumber { get; set; }
    public Card SubmitCard { get => submitPostion.SubmitCard; }
    public int Life { get; set; }


    public void SetCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    void SelectedCard(Card card)
    {
        if (IsSubmitted)
        {
            return;
        }
        if (submitPostion.SubmitCard)
        {
            hand.Add(submitPostion.SubmitCard);
        }
        hand.Remove(card);
        submitPostion.Set(card);
        hand.ResetPositions();
        submitButton?.SetActive(true);
    }

    public void OnSubmitButton()
    {
        if (submitPostion.SubmitCard)
        {
            IsSubmitted = true;
            OnSubmitAction?.Invoke();
            submitButton?.SetActive(false);
        }
    }

    public void RandomSubmit()
    {
        // ŽèŽD‚©‚çƒ‰ƒ“ƒ_ƒ€‚Å‘I‘ð
        Card card = hand.RandomRemove();
        submitPostion.Set(card);
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
        hand.ResetPositions();
    }    
    
    public void SetSubmitCard(int number)
    {
        // ŽèŽD‚©‚çƒ‰ƒ“ƒ_ƒ€‚Å‘I‘ð
        Card card = hand.Remove(number);
        submitPostion.Set(card);
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
        hand.ResetPositions();
    }

    public void SetupNextTurn()
    {
        IsSubmitted = false;
        submitPostion.DeleteCard();
        if (IsBuffNumber)
        {
            IsBuffNumber = false;
            buffNumber = 2;
        }
        else
        {
            buffNumber = 0;
        }
    }

}
