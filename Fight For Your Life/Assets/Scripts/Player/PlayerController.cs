using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    public float inputX;
    public float inputY;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    Transform bodyTransform;

    Rigidbody2D playerRb;
    public float playerSpeed;
    [SerializeField]
    Transform playerSlashAttackPoint;
    [SerializeField]
    float playerAttackSize;

    


    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        MovePlayer();
        FlipPlayer();
    }

    
    void MovePlayer()
    {

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        playerRb.MovePosition(transform.position + new Vector3(inputX * playerSpeed, inputY * playerSpeed, 0));

    }
    void FlipPlayer()
    {
        if (inputX < 0)
        {
            bodyTransform.localScale = new Vector3(1f, bodyTransform.localScale.y, bodyTransform.localScale.z);
        }
        else if (inputX > 0)
        {
            bodyTransform.localScale = new Vector3(-1f, bodyTransform.localScale.y, bodyTransform.localScale.z);
        }
    }

    void SlashAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerSlashAttackPoint.position, playerAttackSize, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            print(enemy);
            if (enemy.CompareTag("EnemyImpRed") == true)
            {
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.currHealth -= 50;

                
            }
            if (enemy.CompareTag("EnemyGoblin") == true)
            {
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.currHealth -= 50;
            }
            //Check what enemy tag it is and change values accordingly
            
        }
        
    }

}
