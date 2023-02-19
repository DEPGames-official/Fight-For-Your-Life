using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Unity.Burst;
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
    Vector3 playerDirection;
    [SerializeField]
    LayerMask playerLayer;

    public float enemySpeed;
    public float enemyAttackRange;
    [SerializeField]
    float enemyAttackSize = 0.5f;
    [SerializeField]
    Transform enemyAttackPoint;

    EnemyAnimationController enemyAnimation;

    void Start()
    {
        playerTransform = playerGameObject.GetComponent<Transform>();
        enemyAnimation = GetComponent<EnemyAnimationController>();
    }

    private void Update()
    {
        
        MoveEnemy(transform);
        FlipEnemy(transform, playerDirection);
    }

    void MoveEnemy(Transform targetTransform)
    {
        
        if (OutOfAttackRange(targetTransform) == true)
        {
            enemyAnimation.UpdateEnemyAnimations(true);
            playerDirection = (playerTransform.position - targetTransform.position).normalized;
            targetTransform.Translate(playerDirection * enemySpeed * Time.deltaTime);
        }
        else
        {
            enemyAnimation.UpdateEnemyAnimations(false);
            
        }

        
    }

    void FlipEnemy(Transform targetTransform, Vector2 direction)
    {
        //print(direction);
        if (direction.x > 0.1f)
        {
            targetTransform.localScale = new Vector3(1f, targetTransform.localScale.y, targetTransform.localScale.z);
        }
        else if (direction.x < -0.1f)
        {
            targetTransform.localScale = new Vector3(-1f, targetTransform.localScale.y, targetTransform.localScale.z);
        }
    }

    void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackSize, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            //print(player);
            var playerHealth = player.gameObject.GetComponent<PlayerHealth>();
            playerHealth.currHealth -= 25;
        }
    }

    bool OutOfAttackRange(Transform targetTransform)
    {
        var distanceToPlayer = Vector2.Distance(targetTransform.position, playerTransform.position);

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