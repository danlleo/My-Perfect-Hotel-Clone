using System;
using DG.Tweening;
using UnityEngine;

public class TransformsTogglerAnimator : MonoBehaviour
{
    private const float DURATION = 0.3f;
    
    public void Enable(Transform objectToAnimate, Action onComplete)
    {
        objectToAnimate.DOScale(new Vector3(1, 1, 1), DURATION)
            .SetEase(Ease.OutBack)
            .OnComplete(() => onComplete());
    }
    
    public void Disable(Transform objectToAnimate, Action onComplete)
    {
        objectToAnimate.DOScale(new Vector3(0, 0, 0), DURATION)
            .SetEase(Ease.InBack)
            .OnComplete(() => onComplete());
    }
}