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

    [SerializeField]
    GameObject playerGameObject;
    Transform playerTransform;

    public float enemySpeed;
    public float enemyAttackRange;

    void Start()
    {
        playerTransform = playerGameObject.GetComponent<Transform>();    
    }

    private void FixedUpdate()
    {
        MoveEnemy();

    }

    void MoveEnemy()
    {
        
        if (OutOfAttackRange() == true)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * enemySpeed * Time.deltaTime);
        }
        
    }

    bool OutOfAttackRange()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > enemyAttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
}