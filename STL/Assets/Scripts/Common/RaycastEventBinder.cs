using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastEventBinder : MonoBehaviour
{
    public Action<RaycastEventBinder> onClick; 
     

    public void OnClick()
    {
        if (onClick != null)
            onClick(this);
    }
}
