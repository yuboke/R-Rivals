using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    // ÉJÅ[ÉhUI
    // ÉQÅ[ÉÄì‡ÇÃèàóù
    [SerializeField] Text nameText;
    [SerializeField] Text numberText;
    [SerializeField] Image icon;
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject hidePanel;

    public CardBase Base { get; private set; }   
    // ä÷êîÇìoò^Ç≈Ç´ÇÈ<à¯êî>
    public UnityAction<Card> OnClickCard;

    public void Set(CardBase cardBase, bool isEnemy)
    {
        Base = cardBase;
        nameText.text = cardBase.Name;
        numberText.text = cardBase.Number.ToString();
        icon.sprite = cardBase.Icon;
        descriptionText.text = cardBase.Description;
        hidePanel.SetActive(isEnemy);

    }

    public void OnClick()
    {
        Debug.Log("OnClick()");
        OnClickCard?.Invoke(this);
    }

    public void OnPointerEnter()
    {
        Debug.Log("OnPointerEnter()");
        transform.position += Vector3.up * 0.3f;
        transform.localScale = Vector3.one * 1.2f;
        GetComponentInChildren<Canvas>().sortingLayerName = "Overlay";
    }

    public void OnPointerExit()
    {
        Debug.Log("OnPointerExit()");
        transform.position -= Vector3.up * 0.3f;
        transform.localScale = Vector3.one;
        GetComponentInChildren<Canvas>().sortingLayerName = "Default";
    }

    public void Open()
    {
        if (hidePanel.activeSelf)
        {
            StartCoroutine(OpenAnim());
        }
    }

    IEnumerator OpenAnim()
    {
        yield return transform.DORotate(new Vector3(0, 90, 0), 0.2f).WaitForCompletion();
        hidePanel.SetActive(false);
        yield return transform.DORotate(new Vector3(0, 0, 0), 0.2f).WaitForCompletion();
    }
}
