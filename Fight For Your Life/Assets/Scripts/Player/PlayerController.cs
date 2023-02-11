using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float inputX;
    public float inputY;

    public float playerSpeed;

    public Vector3 mousePosition;

    public Rigidbody2D playerRb;

    private void Start()
    {
        //playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePosition = Input.mousePosition;

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

}
