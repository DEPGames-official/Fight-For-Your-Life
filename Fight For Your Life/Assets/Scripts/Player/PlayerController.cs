using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float inputX;
    float inputY;

    public float playerSpeed;

    public Vector3 mousePosition;

    Animator playerAnimator;
    [SerializeField]
    Animator weaponAnimator;

    enum AnimationState{idle_side, walk_side, attack_side}
    AnimationState state;

    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        MovePlayer();

        FlipPlayer();

        UpdateAnimationState();
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

    void UpdateAnimationState()
    {
        if (inputX != 0 || inputY != 0)
        {
            state = AnimationState.walk_side;
        }
        else
        {
            state = AnimationState.idle_side;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        
        playerAnimator.SetInteger("animationState", (int)state);
    }

    void Attack()
    {
        if (!weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            state = AnimationState.attack_side;
            print(state);
            weaponAnimator.SetInteger("attackState", 1);
        }
    }

    

    
}
