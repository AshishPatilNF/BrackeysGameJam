using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWayPoints : MonoBehaviour
{
    List<Transform> wayPoint = new List<Transform>();

    public void AddWayPoint(Transform newWayPoint)
    {
        wayPoint.Add(newWayPoint);
    }

    public Transform AssignWayPoint()
    {
        if (wayPoint.Count > 0)
        {
            Transform temp = wayPoint[Random.Range(0, wayPoint.Count)];
            wayPoint.Remove(temp);
            return temp;
        }
        else
        {
            return null;
        }
    }
}
