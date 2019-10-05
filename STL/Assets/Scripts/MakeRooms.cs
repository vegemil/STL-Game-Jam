using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MakeRooms : MonoBehaviour
{
    public Action onCreateRoom;
    public int MAX_FLOOR = 2;

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

        int index = UnityEngine.Random.Range(0, (int)Room.DoorTyps.MAX);
        string[] doorNames = { "FD", "LD", "RD" };

        int temp = 0;
        foreach(string doorName in doorNames)
        {
            GameObject door = room.transform.Find(doorName).gameObject;
            if(door)
            {
                int isShow = UnityEngine.Random.Range(0, 2);

                door.SetActive(index == temp || Convert.ToBoolean(isShow));
            }
            temp++;
        }
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

        if(layer == MAX_FLOOR)
        {
            GameObject floorDoor = room.transform.Find("FD").gameObject;
            floorDoor.SetActive(false);
        }

        makeRoom();
    }

    public void moveSameFloor()
    {
        makeRoom();
    }

    void makeItem()
    {

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
