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
        Exit
    }


    [SerializeField] private GameController gameController;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowExitPopup();
        }
    }

    public void SwitchMenu(MenuType newMenu)
    {
        foreach(var root in MenuRoots)
        {
            root.gameObject.SetActive(false);
        }
        MenuRoots[(int)newMenu].gameObject.SetActive(true); 

        switch(newMenu)
        {
            case MenuType.Title:
                AudioManager.Instance.PlayBGM(AudioManager.BGMType.Title);
                break;
            case MenuType.Main:
                gameController.Initalize();
                break;
        }
    }

    public void OnClickTitleImage()
    {
        if (fadeTween!= null && fadeTween.IsPlaying())
            return;

        if (MenuRoots[(int)MenuType.Exit].gameObject.activeSelf)
            return;

        GameObject uiManager = GameObject.Find("RoomManager");
        MakeRooms makeRooms = uiManager.GetComponent<MakeRooms>();
        makeRooms.resetRoom();  
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayEffectAudio(AudioManager.EffectType.ClickTitle);

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
            var newBgmType = (int)AudioManager.BGMType.Ending1 + (int)doorType;
            AudioManager.Instance.PlayBGM((AudioManager.BGMType)newBgmType);
        });
    }

    public void OnClickEnding()
    {
        if (MenuRoots[(int)MenuType.Exit].gameObject.activeSelf)
            return;

        SwitchMenu(MenuType.Title);
    }

    private void ShowExitPopup()
    {
        MenuRoots[(int)MenuType.Exit].gameObject.SetActive(true);

        GameObject room = GameObject.Find("RoomManager");
        MakeRooms roomManager = room.GetComponent<MakeRooms>();
        roomManager.enableRoomBtn(false);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void OnClickCancel()
    {
        MenuRoots[(int)MenuType.Exit].gameObject.SetActive(false);

        GameObject room = GameObject.Find("RoomManager");
        MakeRooms roomManager = room.GetComponent<MakeRooms>();
        roomManager.enableRoomBtn(true);
    }

}
