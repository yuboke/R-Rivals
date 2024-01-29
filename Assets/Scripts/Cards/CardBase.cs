using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    // �J�[�h�̊�b�f�[�^
    // �C���X�y�N�^�[�Ɍ��J���郂�m
    [SerializeField] new string name;
    [SerializeField] int number;
    [SerializeField] CardType type;
    [SerializeField] Sprite icon;
    [TextArea]
    [SerializeField] string description;

    // �v���p�e�B
    // �ق��̃t�@�C���Ɍ��J���郂�m
    public string Name { get => name; }
    public int Number { get => number; }
    public CardType Type { get => type; }
    public Sprite Icon { get => icon; }
    public string Description { get => description; }
    
}

public enum CardType
{
    Clown,
    Princess,
    Spy,
    Assassin,
    Minister,
    Magician,
    General,
    Prince,

}
