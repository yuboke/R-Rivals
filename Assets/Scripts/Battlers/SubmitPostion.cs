using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubmitPostion : MonoBehaviour
{
    Card submitCard;

    public Card SubmitCard { get => submitCard; }

    public void Set(Card card)
    {
        submitCard = card;
        card.transform.SetParent(transform);
        card.transform.DOMove(transform.position, 0.1f);
    }

    public void DeleteCard()
    {
        //Destroy(submitCard); �͌��. submitCard�I�u�W�F�N�g���폜��������.
        Destroy(submitCard.gameObject);
        submitCard = null;
    }
}
