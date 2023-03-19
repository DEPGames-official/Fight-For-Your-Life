using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Unity.Burst;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyAnimationController))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyController : MonoBehaviour
{
    public int difficultyLevel;

    Transform playerTransform;
    Vector3 playerDirection;
    [SerializeField]
    LayerMask playerLayer;

    public float enemySpeed;
    public float enemyAttackRange;
    public float enemyAttackPower = 25;
    [SerializeField]
    float enemyAttackSize = 0.5f;
    [SerializeField]
    Transform enemyAttackPoint;

    EnemyAnimationController enemyAnimation;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
            playerHealth.currHealth -= enemyAttackPower;
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