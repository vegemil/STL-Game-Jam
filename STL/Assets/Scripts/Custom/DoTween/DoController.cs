using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {
        public class DoController : MonoBehaviour
        {
            public Action onComplatePlayer;
            public Action onComplateAllPlayers;
            [SerializeField] private DoPlayer[] DoTweends;
            public bool bEnableAutoPlay;

            int maxCount;
            int countIndex;

            private void OnEnable()
            {
                maxCount = DoTweends.Length;
                foreach (var tweens in DoTweends)
                {
                    tweens.OnComplate = OnComplate;
                }
                if (bEnableAutoPlay)
                    Play();
            } 

            public void Play()
            {
                countIndex = 0;
                DoTweends.ToList().ForEach((iter) => iter.PlayTween());
            }

            public void PlayReverse()
            { 
                countIndex = 0;
                DoTweends.ToList().ForEach((iter) => iter.PlayTweenReverse());
            }

            public void Stop()
            {
                DoTweends.ToList().ForEach((iter) => iter.StopTween());
            }

            public void OnComplate(DoPlayer doPlayer)
            {
                countIndex++;
                if (onComplatePlayer != null)
                    onComplatePlayer();
                if (countIndex >= maxCount)
                    onComplateAllPlayers();
            }

            void OnComplteAllPlayer()
            {
                countIndex = 0;
                if (onComplateAllPlayers != null)
                    onComplateAllPlayers();
            }
        }


    }
}

