using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float inputX;
    public float inputY;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    Rigidbody2D playerRb;
    public float playerSpeed;
    [SerializeField]
    Transform playerSlashAttackPoint;
    [SerializeField]
    float playerAttackSize;

    XPBarController xpController;
    [SerializeField]
    GameObject xpBar;





    private void Start()
    {
        xpController = xpBar.GetComponent<XPBarController>();
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
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else if (inputX > 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }

    void SlashAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerSlashAttackPoint.position, playerAttackSize, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            //Check what enemy tag it is and change values accordingly
            print(enemy);
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.currHealth -= 50;
            xpController.AddXP(50f);
        }
        
    }

}
