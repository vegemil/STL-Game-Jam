using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIRoot : Singleton<UIRoot>
{
    public enum MenuType
    {
        Title,
        Main,
        Ending,
    }


    [SerializeField] private GameObject[] MenuRoots;
    [SerializeField] private RawImage titleImage;
    [SerializeField] private float tweenDelay;

    Tween fadeTween;


    private void Start()
    {
        SwitchMenu(MenuType.Title);
    }

    public void SwitchMenu(MenuType newMenu)
    {
        foreach(var root in MenuRoots)
        {
            root.gameObject.SetActive(false);
        }
        MenuRoots[(int)newMenu].gameObject.SetActive(true); 
    }

    public void OnClickTitleImage()
    {
        if (fadeTween!= null && fadeTween.IsPlaying())
            return;

        fadeTween = titleImage.DOFade(0, tweenDelay).OnComplete(() =>
        {
            SwitchMenu(MenuType.Main); 
        }); 
    }


    public void OnClickEnding()
    {

    }


}
