using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*
     * Use this only for in case I carry on with my own A Star Algorithm
    [SerializeField]
    Rigidbody2D enemyRigidBody;

    public void MoveEnemy(float directionX, float directionY, float enemySpeed)
    {
        enemyRigidBody.MovePosition(transform.position + new Vector3(directionX * enemySpeed, directionY * enemySpeed, 0));
    }
    */

    void faceDirection()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        faceDirection();
    }

}