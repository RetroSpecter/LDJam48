using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapController : MonoBehaviour
{

    public List<doorScript> doorWalls;
    public static List<mapController> peers;

    public int ID;

    public static GameObject player;
    public float radius;

    void Awake()
    {
        if (peers == null)
        {
            peers = new List<mapController>();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Use this for initialization
    void Start()
    {
        doorWalls = new List<doorScript>();
        //doorScript.doorOpen += generateDoor;
        doorScript[] temp = transform.GetComponentsInChildren<doorScript>();
        foreach (doorScript t in temp)
        {
            doorWalls.Add(t);
        }


        ID = peers.Count;
        peers.Add(this);

    }

    private void rotateTowardsPlayer()
    {
        float angleDeg = Vector3.Angle(transform.forward, player.transform.position - transform.position); //non-90 degree amount

        transform.Rotate(0f, roundTo90(angleDeg, 0), 0f);
    }
    private float roundTo90(float unRounded, float bottomGuess)
    {
        if (unRounded - bottomGuess <= 45)
        {
            return bottomGuess;
        }
        else
        {
            return roundTo90(unRounded, bottomGuess + 90);
        }
    }

    private bool isMyDoor(doorScript d)
    {
        return doorWalls.Contains(d);
    }

    public bool generateDoor(doorScript d) //the given door gameobject has been broken
    {
        if (!isMyDoor(d))
        {
            Debug.Log("too many");
            return false;
        }
        Vector3 oldRoomPos = d.transform.parent.transform.position;
        Vector3 openedDoorPos = d.transform.position;
        Vector3 roomCenterToDoorPos = openedDoorPos - oldRoomPos;
        roomCenterToDoorPos.y = transform.position.y;

        mapController selectedRoom = getRoomType();

        GameObject newRoom = GameObject.Instantiate(selectedRoom.gameObject, transform.position + (selectedRoom.radius * roomCenterToDoorPos.normalized + roomCenterToDoorPos) + variation(), new Quaternion());
        newRoom.GetComponent<mapController>().rotateTowardsPlayer();
        doorScript closestNewWall = getClosestDoor(newRoom, openedDoorPos);
        closestNewWall.gameObject.transform.DetachChildren();
        newRoom.GetComponent<mapController>().doorWalls.Remove(closestNewWall);
        closestNewWall.transform.DetachChildren();
        Destroy(closestNewWall.gameObject);
        return true;
    }

    private Vector3 variation()
    {
        return new Vector3(0, Random.Range(-.0001f, 0.0001f), 0);
    }

    private mapController getRoomType()
    {
        List<GameObject> potentialRooms = GameObject.FindObjectOfType<PlayerController>().roomPrefabs;
        return potentialRooms[Random.Range(0, potentialRooms.Count)].GetComponent<mapController>();
    }

    private doorScript getClosestDoor(GameObject o, Vector3 towardsThis)
    {
        doorScript[] doors = o.GetComponentsInChildren<doorScript>();
        doorScript outty = doors[0];
        float closest = Vector3.Distance(towardsThis, outty.transform.position);

        foreach (doorScript ds in doors)
        {
            float dd = Vector3.Distance(ds.transform.position, towardsThis);
            if (dd < closest)
            {
                closest = dd;
                outty = ds;
            }
        }
        return outty;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        for(int i = peers.Count - 1; i >= 0; i--)
        {
            if (peers[i].ID != ID && peers[i].transform.position == transform.position)
            {
                Destroy(peers[i].gameObject);
                peers.RemoveAt(i);
                
            }
        } */
    }

}
