using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    public RectTransform pointer;
    public Transform target;

    


    private void Update()
    {
        PointDirection();
    }

    void PointDirection()
    {
        Vector3 direction = pointer.transform.InverseTransformPoint(target.position);
        print(direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        print(angle);

        pointer.Rotate(0, 0, angle);
        //Convert rotation or angle to circle around player and use that yes
        
    }

}
