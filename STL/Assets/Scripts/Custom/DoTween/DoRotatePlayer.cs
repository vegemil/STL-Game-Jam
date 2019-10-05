using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {

        public class DoRotatePlayer : DoPlayer
        {

            [SerializeField] private Vector3 AddTweenEulerValue;
            [SerializeField] RotateMode rotateMode = RotateMode.Fast;
            Quaternion startRotation;
            Vector3 StartEulerRotation;
            Vector3 endEulerRotation;


            private void Start()
            {
                CheckInitalize();
            }


            protected override Tween GetPlayTween()
            {
                CheckInitalize();
                return transform.DOLocalRotate(endEulerRotation, duration, rotateMode).SetDelay(delay).SetEase(ease);
            }

            protected override Tween GetReversePlayTween()
            {
                CheckInitalize();
                return transform.DOLocalRotate(StartEulerRotation, duration, rotateMode).SetDelay(delay).SetEase(ease);
            }

            protected override Tween GetDelayLoopTween()
            {
                CheckInitalize();
                return transform.DOLocalRotate(endEulerRotation, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);
            }

            protected override Tween GetReverseDelayLoopTween()
            {
                CheckInitalize();
                return transform.DOLocalRotate(StartEulerRotation, duration).SetDelay(restartDelay).SetEase(ease).SetLoops(1, loopType);
            }

            protected override void CheckInitalize()
            {
                if (bInitalized)
                    return;
                bInitalized = true;
                if (bUseAddValue)
                {
                    startRotation = gameObject.transform.localRotation;
                    StartEulerRotation = startRotation.eulerAngles;
                    endEulerRotation = startRotation.eulerAngles + AddTweenEulerValue;
                }
                else
                {
                    startRotation = gameObject.transform.localRotation;
                    StartEulerRotation = startRotation.eulerAngles;
                    endEulerRotation = endTransform.localRotation.eulerAngles;
                }
            }

        }

    }
}

