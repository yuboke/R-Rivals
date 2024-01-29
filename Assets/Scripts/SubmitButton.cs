using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubmitButton : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(0.1f, 1f)
            .SetRelative(true)                  // ���ΓI�ȋN�_
            .SetEase(Ease.OutQuart)             // �ό`���@
            .SetLoops(-1, LoopType.Restart);    // ���[�v�ݒ�
    }
}
