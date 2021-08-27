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
            Transform temp = wayPoint[0];
            wayPoint.RemoveAt(0);
            return temp;
        }
        else
        {
            return null;
        }
    }
}
