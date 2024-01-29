using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    // カードの基礎データ
    // インスペクターに公開するモノ
    [SerializeField] new string name;
    [SerializeField] int number;
    [SerializeField] CardType type;
    [SerializeField] Sprite icon;
    [TextArea]
    [SerializeField] string description;

    // プロパティ
    // ほかのファイルに公開するモノ
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
