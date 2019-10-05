using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
    namespace Tweening
    {


        public class DoFadePlayer : DoPlayer
        {

            [SerializeField] float startValue;
            [SerializeField] float endValue;
            CanvasGroup targetGraphic;

            private void Start()
            {
                CheckInitalize();
            }

            protected override Tween GetPlayTween()
            {
                CheckInitalize();
                
                return targetGraphic.DOFade(endValue, duration).SetDelay(delay).SetEase(ease);

            }

            protected override Tween GetReversePlayTween()
            {
                CheckInitalize();
                return targetGraphic.DOFade(startValue, duration).SetDelay(delay).SetEase(ease);
            }

            protected override Tween GetDelayLoopTween()
            {
                CheckInitalize();
                return targetGraphic.DOFade(endValue, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);
            }

            protected override Tween GetReverseDelayLoopTween()
            {
                CheckInitalize();
                return targetGraphic.DOFade(startValue, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);

            }
            protected override void CheckInitalize()
            {
                if (bInitalized)
                    return;
                
                targetGraphic = transform.GetComponent<CanvasGroup>();
                bInitalized = true; 
            }

            protected override void ResetToStart()
            {
                CheckInitalize();
                targetGraphic.alpha = startValue;
            }

            protected override void ResetToEnd()
            {
                CheckInitalize();
                targetGraphic.alpha = endValue;
            }
        }

    }
}

