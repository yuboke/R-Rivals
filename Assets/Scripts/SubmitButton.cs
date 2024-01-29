using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubmitButton : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(0.1f, 1f)
            .SetRelative(true)                  // 相対的な起点
            .SetEase(Ease.OutQuart)             // 変形方法
            .SetLoops(-1, LoopType.Restart);    // ループ設定
    }
}
