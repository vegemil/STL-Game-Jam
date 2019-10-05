using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private Camera camera;

    private void Awake()
    {
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) 
            CheckHit(); 

    }

    void CheckHit()
    {

        float maxRayDistance = 5000f;
        RaycastHit raycastHit = new RaycastHit();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        bool isCollide = Physics.Raycast(ray, out raycastHit, maxRayDistance);
        if (!isCollide)
            return;
        Debug.Log($"Hit : {raycastHit.collider.gameObject.name} ");
        var eventBinder = raycastHit.collider.gameObject.GetComponent<RaycastEventBinder>();
        if (eventBinder)
            eventBinder.OnClick();
    }
}
