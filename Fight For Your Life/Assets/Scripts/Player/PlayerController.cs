using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float inputX;
    public float inputY;

    public float playerSpeed;

    public Vector3 mousePosition;

    
    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        MovePlayer();

        FlipPlayer();
    }

    void MovePlayer()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(inputX * playerSpeed, inputY * playerSpeed, 0) * Time.deltaTime);
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
