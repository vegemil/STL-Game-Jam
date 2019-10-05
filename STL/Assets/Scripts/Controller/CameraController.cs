using System;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum MoveDirectionType
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public Action<RaycastEventBinder> onClickObject;
    public Action onEndOutMove;
    public Action onEndInMove;

    [SerializeField] private DoMovePlayer outMovePlayer;
    [SerializeField] private DoMovePlayer inMovePlayer;
    [SerializeField] private Transform[] moveDests;
    private Camera camera;
    private GameObject lastClickObject;
    private bool isMovingCamera;

    public GameObject LastClickObject => lastClickObject;
    public bool IsMovingCamera => isMovingCamera;

    private void Awake()
    {
        DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        camera = gameObject.GetComponent<Camera>();
        isMovingCamera = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) 
            CheckHit(); 

    }

    public void CleaeClickObject()
    {
        lastClickObject = null;
    }

    public void PlayMove(MoveDirectionType outMoveDirection, Action newOnEndOutMove = null, Action newOnEndInMove = null)
    {
        onEndOutMove = newOnEndOutMove;
        onEndInMove = newOnEndInMove;
        MoveDirectionType inMoveDirection = MoveDirectionType.Top;
        if (outMoveDirection == MoveDirectionType.Bottom)
            inMoveDirection = MoveDirectionType.Top; 
        if (outMoveDirection == MoveDirectionType.Right)
            inMoveDirection = MoveDirectionType.Left;
        if (outMoveDirection == MoveDirectionType.Left)
            inMoveDirection = MoveDirectionType.Right;

        isMovingCamera = true;
        Transform destOutPosition = moveDests[(int)outMoveDirection];
        Transform startInPosition = moveDests[(int)inMoveDirection];
        outMovePlayer.endTransform = destOutPosition;
        inMovePlayer.startTransform = startInPosition;



        outMovePlayer.PlayTween();
        outMovePlayer.OnComplate = (tween) =>
        {
            inMovePlayer.PlayTween();
            if (onEndOutMove != null)
                onEndOutMove();
            DOTween.Complete(outMovePlayer);
        };
        inMovePlayer.OnComplate = (tween) =>
         {
             if (onEndInMove != null)
                 onEndInMove();
             isMovingCamera = false;
             DOTween.Complete(inMovePlayer);
         };
    }

    void CheckHit()
    {

        float maxRayDistance = 5000f;
        RaycastHit raycastHit = new RaycastHit();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        bool isCollide = Physics.Raycast(ray, out raycastHit, maxRayDistance);
        if (!isCollide)
            return;
        lastClickObject = raycastHit.collider.gameObject;
        Debug.Log($"Hit : {raycastHit.collider.gameObject.name} ");
        var eventBinder = raycastHit.collider.gameObject.GetComponent<RaycastEventBinder>();
        if (eventBinder)
            eventBinder.OnClick();
        if (onClickObject != null)
            onClickObject(eventBinder);
    }
}
