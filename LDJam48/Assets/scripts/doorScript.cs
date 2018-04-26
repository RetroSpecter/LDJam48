using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{

    private float health;

    //public delegate bool transition(doorScript d);
    //public static transition doorOpen;

    // Use this for initialization
    void Start()
    {
        health = 10;
    }

    public void damage(float amount)
    {
        health -= amount;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            transform.parent.GetComponent<mapController>().generateDoor(this);
            //doorOpen.Invoke(this);
            mapController parentCont = transform.parent.GetComponent<mapController>();

            foreach (mapController mc in mapController.peers)
            {
                if (mc.ID != parentCont.ID)
                {
                    for(int i = mc.doorWalls.Count - 1; i >= 0; i--)
                    {
                        if (mc.doorWalls[i] != null)
                        {
                            if (Vector3.Distance(transform.position, mc.doorWalls[i].transform.position) < 1f)
                            {
                                mc.doorWalls.RemoveAt(i);
                                Destroy(mc.doorWalls[i].gameObject);
                            }
                        }
                    }
                }
            }

            parentCont.doorWalls.Remove(this);
            Destroy(this.gameObject);

        }
    }
}
