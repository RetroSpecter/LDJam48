using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRoomManager : MonoBehaviour {

    public List<GameObject> roomPrefabs;
    public static List<GameObject> InstanceRoomPrefabs;

    void Start () {
        InstanceRoomPrefabs = new List<GameObject>(roomPrefabs);
	}
}
