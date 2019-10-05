using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {


        public class DoScalePlayer : DoPlayer
        {

            [SerializeField] private Vector3 AddTweenValue;
            Vector3 startScale;
            Vector3 endScale;

            private void Start()
            {
                CheckInitalize();
            } 

            public override void InitValueToOrigin()
            {
                base.InitValueToOrigin();
                if (!bOriginOnEnable)
                    return;
                CheckInitalize();
                gameObject.transform.localScale = startScale; 
            }

            protected override Tween GetPlayTween()
            {
                CheckInitalize();
                return transform.DOScale(endScale, duration).SetDelay(delay).SetEase(ease);
            }

            protected override Tween GetReversePlayTween()
            {
                CheckInitalize();
                return transform.DOScale(startScale, duration).SetDelay(delay).SetEase(ease).SetLoops(1);
            }

            protected override Tween GetDelayLoopTween()
            {
                CheckInitalize();
                return base.transform.DOScale(endScale, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);
            }

            protected override Tween GetReverseDelayLoopTween()
            {
                CheckInitalize();
                return base.transform.DOScale(startScale, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);
            }

            protected override Tween GetToOriginTween()
            {
                CheckInitalize();
                return base.transform.DOScale(startScale, duration).SetDelay(delay).SetEase(ease).SetLoops(1);
            }

            protected override void ResetToStart()
            {
                base.ResetToStart();
                CheckInitalize();
                transform.localScale = startScale;
            }

            protected override void ResetToEnd()
            {
                base.ResetToEnd();
                CheckInitalize();
                transform.localScale = endScale;
            }

            protected override void CheckInitalize()
            {
                if (bUseAddValue)
                {
                    if (bInitalized)
                        return;
                    bInitalized = true;
                    startScale = gameObject.transform.localScale;
                    endScale = startScale + AddTweenValue;
                }
                else
                {
                    startScale = startTransform.localScale;
                    endScale = endTransform.localScale;
                }
            }
        }

    }
}

