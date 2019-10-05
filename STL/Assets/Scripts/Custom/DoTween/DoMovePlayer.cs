using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {

        public class DoMovePlayer : DoPlayer
        {
            [SerializeField] private Vector3 AddTweenValue;
            Vector3 startPosition; 
            Vector3 endPosition;

            private void Start()
            {
                CheckInitalize();
            }

            protected override Tween GetPlayTween()
            {
                CheckInitalize();
                return transform.DOLocalMove(endPosition, duration).SetDelay(delay).SetEase(ease);
            }

            protected override Tween GetReversePlayTween()
            {
                CheckInitalize();
                return transform.DOLocalMove(startPosition, duration).SetDelay(delay).SetEase(ease);
            }

            protected override Tween GetDelayLoopTween()
            {
                CheckInitalize();
                return base.transform.DOLocalMove(endPosition, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);
            }

            protected override Tween GetReverseDelayLoopTween()
            {
                CheckInitalize();
                return base.transform.DOLocalMove(startPosition, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);
            }

            protected override void ResetToStart()
            {
                base.ResetToStart();
                CheckInitalize();
                transform.localPosition = startPosition;
            }

            protected override void ResetToEnd()
            {
                base.ResetToEnd();
                transform.localPosition = endPosition;
            }

            protected override void CheckInitalize()
            {
                 
                if (bUseAddValue)
                {
                    if (bInitalized)
                        return;
                    bInitalized = true;
                    startPosition = gameObject.transform.localPosition;
                    endPosition = startPosition + AddTweenValue;
                }
                else
                {
                    startPosition = startTransform.localPosition;
                    endPosition = endTransform.localPosition;
                }
            }
        }

    }
}

