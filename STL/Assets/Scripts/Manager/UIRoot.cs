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
    [SerializeField] private RawImage[] endingImages;
    [SerializeField] private float tweenDelay;

    Tween fadeTween;


    private void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Default);
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

        GameObject uiManager = GameObject.Find("RoomManager");
        MakeRooms makeRooms = uiManager.GetComponent<MakeRooms>();
        makeRooms.resetRoom();

        fadeTween = titleImage.DOFade(0, tweenDelay).OnComplete(() =>
        {
            SwitchMenu(MenuType.Main);
            titleImage.color = Color.white;

            DOTween.Complete(fadeTween);
        }); 
    }

    public void setEndingImage(Room.DoorTyps doorType)
    {
        if (fadeTween != null && fadeTween.IsPlaying())
            return;

        foreach (var root in endingImages)
        {
            root.gameObject.SetActive(false);
        }

        endingImages[(int)doorType].gameObject.SetActive(true);

        fadeTween = endingImages[(int)doorType].DOFade(255, 0).OnComplete(() =>
        {
            SwitchMenu(MenuType.Ending);
            DOTween.Complete(fadeTween);
        });
    }

    public void OnClickEnding()
    {
        SwitchMenu(MenuType.Title);
    }


}
