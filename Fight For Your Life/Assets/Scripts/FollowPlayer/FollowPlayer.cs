using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    Transform followPlayer;
    [SerializeField]
    Vector3 offset;
    
    void LateUpdate()
    {
        this.transform.position = followPlayer.position + offset;
    }
}
