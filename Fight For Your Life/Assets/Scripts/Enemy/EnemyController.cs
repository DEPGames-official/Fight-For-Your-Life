using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D enemyRigidBody;

    public void MoveEnemy(float directionX, float directionY, float enemySpeed)
    {
        enemyRigidBody.MovePosition(transform.position + new Vector3(directionX * enemySpeed, directionY * enemySpeed, 0));
    }

}