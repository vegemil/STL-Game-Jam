using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MakeRooms : MonoBehaviour
{
    //private GameObject leftWall;
    //private GameObject rightWall;
    //private GameObject floor;
    private string prefabPath = "Prefabs/";

    void makeRoom(int layer)
    {
        // 왼쪽 벽
        string name = getObjectName(layer);
        //GameObject prefab = Resources.Load("Prefabs/Bullet") as GameObject;
        // Resources/Prefabs/Bullet.prefab 로드
       //GameObject bullet = MonoBehaviour.Instantiate(prefab) as GameObject;

        //GameObject prefab = Resources.Load("Prefabs/Bullet") as GameObject;
        GameObject leftWall = Instantiate(Resources.Load(prefabPath + "leftWall", typeof(GameObject))) as GameObject;

        // 오른쪽 벽
        name = getObjectName(layer);
        //source = Resources.Load(prefabPath /*+ "R" + name + ".prefab"*/ + "rightWall.prefab");
        GameObject rightWall = Instantiate(Resources.Load(prefabPath + "rightWall", typeof(GameObject))) as GameObject;

        // 바닥
        name = getObjectName(layer);
        //source = Resources.Load(prefabPath /*+ "F" + name + ".prefab"*/ + "floor.prefab");
        GameObject floor = Instantiate(Resources.Load(prefabPath + "floor", typeof(GameObject))) as GameObject;

        //Instantiate(floor, Vector3.zero, Quaternion.identity);
        //Instantiate(rightWall, Vector3.zero, Quaternion.identity);
        //Instantiate(leftWall, Vector3.zero, Quaternion.identity);
    }

    string getObjectName(int layer)
    {
        int index = Random.Range(0, 5);

        return layer.ToString() + index.ToString() + ".prefab";
    }

    void makeRooms()
    {
        for(int i = 0; i < 4; ++i)
        {
            makeRoom(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        makeRoom(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
