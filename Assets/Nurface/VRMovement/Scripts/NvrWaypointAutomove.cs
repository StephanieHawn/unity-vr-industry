using UnityEngine;
using System.Collections;

public class NvrWaypointAutomove : MonoBehaviour {

    public Transform[] waypoints;
    public int rate = 5;
    private int currentWaypoint = 0;

    void Start() {
        MoveToWaypoint();
    }
   
    void MoveToWaypoint() {
        //Time = Distance / Rate:
        float travelTime = Vector3.Distance(transform.position, waypoints[currentWaypoint].position) / rate;

        //iTween:
        iTween.MoveTo(gameObject, iTween.Hash("position", waypoints[currentWaypoint].position + waypoints[currentWaypoint].gameObject.GetComponent<NvrWaypoint>().playerOffset, "time", travelTime, "easetype", "linear", "oncomplete", "MoveToWaypoint"));

        //Move to next waypoint:
        currentWaypoint++;
        if (currentWaypoint > waypoints.Length - 2) {
            currentWaypoint = 0;
        }
    }
}
