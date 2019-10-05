
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening
{
    public class DoActiveTween : MonoBehaviour
    {
        public Action<DoPlayer> onEndActiveTween;
        public Action<DoPlayer> onEndDisableTween;
        

        [SerializeField] private DoPlayer ActiveTween;
        [SerializeField] private DoPlayer DisableTween; 
        [SerializeField] private bool bReversePlayOnStart;
        [SerializeField] private bool bReversePlayOnEnd;
        [SerializeField] private bool bDisableToOrigin = true;
        [SerializeField]  bool isControlActiveGameObject = false;

        bool bActived = false;
        public bool IsActived {  get { return bActived; } }
         

        public void PlayActiveTween()
        {
            bActived = true;
            if (bReversePlayOnStart)
                ActiveTween.PlayTweenReverse();
            else
                ActiveTween.PlayTween();
            if (isControlActiveGameObject)
            {
                gameObject.SetActive(true);
            }

            if (ActiveTween.OnComplate != OnFinishedActive)
                ActiveTween.OnComplate += OnFinishedActive;
        }

        public void PlayDisableTween()
        {
            bActived = false;
            if (bDisableToOrigin)
            {
                DisableTween.PlayToOrigin();
                return;
            }
            if (bReversePlayOnEnd)
                DisableTween.PlayTweenReverse();
            else
                DisableTween.PlayTween();

            if (DisableTween.OnComplate != OnFinishedDisable)
                DisableTween.OnComplate += OnFinishedDisable;
        }

        public void Clear()
        {
            if(ActiveTween != null)
                ActiveTween.StopTween();
            if(DisableTween != null)
                DisableTween.StopTween();
        }
         
         
        void OnFinishedDisable(DoPlayer tweener)
        {
            DisableTween.OnComplate -= OnFinishedDisable;
            if (isControlActiveGameObject)
            {
                gameObject.SetActive(false);
            }
            if (onEndDisableTween != null)
                onEndDisableTween(tweener);
        }

        void OnFinishedActive(DoPlayer tweener)
        {
            ActiveTween.OnComplate -= OnFinishedActive; 
            if (onEndActiveTween != null)
                onEndActiveTween(tweener);
        }
    }
}


