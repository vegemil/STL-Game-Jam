using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MakeRooms : MonoBehaviour
{
    public Action onCreateRoom;
    public int MAX_FLOOR = 2;

    [SerializeField] private GameObject spawnRoot;


    private string prefabPath = "Prefabs/";
    private string texturePath = "Texture/";
    private int layer = 0;

    private GameObject room;
     
    public Room CurrentRoom => room.GetComponent<Room>();


    private void makeRoom()
    {
        GameObject leftWall = room.transform.Find("leftWall").gameObject;
        GameObject rightWall = room.transform.Find("rightWall").gameObject;
        GameObject floor = room.transform.Find("floor").gameObject;

        // 왼쪽 벽
        setSprite(leftWall, "L");
        setSprite(rightWall, "R");
        setSprite(floor, "F");
    }

    private string getObjectName(string prefix)
    {
        int index = UnityEngine.Random.Range(0, 5);

        return prefix + layer.ToString() + index.ToString();
    }

    private void setSprite(GameObject obj, string prefix)
    {
        string objName = getObjectName(prefix);
        var render = obj.GetComponent<SpriteRenderer>();
        if (render)
        {
            //Debug.Log("FILE NAME : " + objName);
            Sprite sprite = Resources.Load(texturePath + objName, typeof(Sprite)) as Sprite;
            render.sprite = sprite;
        }
    }

    public void moveNextFloor()
    {
        layer++;

        if(layer >= MAX_FLOOR)
        {
            layer = 0;
        }
        makeRoom();
    }

    public void moveSameFloor()
    {
        makeRoom();
    }

    // Start is called before the first frame update
    void Start()
    {
        room = Resources.Load(prefabPath + "Room", typeof(GameObject)) as GameObject;

        room = Instantiate(room);
        makeRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
