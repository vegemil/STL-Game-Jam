using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {

        public class DoPlayer : MonoBehaviour
        {
            public Action<DoPlayer> OnComplate;
            public Ease ease = Ease.Linear;
            public LoopType loopType = LoopType.Incremental;
            public int loopCount = 1;
            public float duration = 1;
            public float delay = 0;
            public float restartDelay = 0;
            public Transform startTransform;
            public Transform endTransform;
            public bool bOriginOnEnable = true;
            public bool bResetValueOnStart = false;

            protected bool bInitalized = false;
            /* 현재 기준 Add한 값만큼 EndTrasform지정할지. */
            [SerializeField] protected bool bUseAddValue = false;

            public Sequence CurrentScquence;

            private void OnEnable()
            {
                CheckInitValue();
            }

            private void OnDisable()
            {
                if(CurrentScquence != null)
                    CurrentScquence.Kill();
            }

            void CheckInitValue()
            {
                if (bOriginOnEnable)
                    InitValueToOrigin();
            }

            void OnComplete()
            {
                if (OnComplate != null)
                    OnComplate(this);
            }
             
            public virtual void InitValueToOrigin() { return; }

            public void PlayTween()
            { 
                if (CurrentScquence != null)
                {
                    CurrentScquence.Kill();
                    CurrentScquence = DOTween.Sequence();
                }
                else
                    CurrentScquence = DOTween.Sequence();
                if (bResetValueOnStart)
                    ResetToStart();
                if (restartDelay == 0)
                    CurrentScquence.Append(GetPlayTween());
                else
                    CurrentScquence.Append(GetDelayLoopTween());

                CurrentScquence.onComplete = OnComplete;
                CurrentScquence.SetLoops(loopCount, loopType); 
                CurrentScquence.Play();

            }

            public void PlayTweenReverse()
            {
                if (CurrentScquence != null)
                {
                    CurrentScquence.Kill();
                    CurrentScquence = DOTween.Sequence();
                }
                else
                    CurrentScquence = DOTween.Sequence();
                if (bResetValueOnStart)
                    ResetToEnd();
                if (restartDelay == 0)
                    CurrentScquence.Append(GetReversePlayTween());
                else
                    CurrentScquence.Append(GetReverseDelayLoopTween());

                CurrentScquence.onComplete = OnComplete; 
                CurrentScquence.SetLoops(loopCount, loopType);
                CurrentScquence.Play();

            }

            public void StopTween()
            {
                if (CurrentScquence == null)
                    return;
                CurrentScquence.Kill();
            }

            public void PlayToOrigin()
            {
                if (CurrentScquence != null)
                {
                    CurrentScquence.Kill();
                    CurrentScquence = DOTween.Sequence();
                }
                else
                    CurrentScquence = DOTween.Sequence();
                if (bResetValueOnStart)
                    ResetToEnd();
                CurrentScquence.onComplete = OnComplete; 
                CurrentScquence.Append(GetToOriginTween()); 
                CurrentScquence.Play();
            }

            protected virtual Tween GetPlayTween()
            {
                return null;
            }
            protected virtual Tween GetDelayLoopTween()
            {
                return null;
            }

            protected virtual Tween GetReversePlayTween()
            {
                return null;
            }
            protected virtual Tween GetReverseDelayLoopTween()
            {
                return null;
            }

            protected virtual Tween GetToOriginTween()
            {
                return null;
            }

            protected virtual void CheckInitalize()
            {
                if (bInitalized)
                    return;
                bInitalized = true;
            }

            protected virtual void ResetToStart()
            {
                return;
            }

            protected virtual void ResetToEnd()
            {
                return;
            }

        }

    }
}

