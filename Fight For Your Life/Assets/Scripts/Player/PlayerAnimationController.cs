using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    
    PlayerController playerController;

    [Space]

    [SerializeField]
    Animator playerAnimator;
    public float animationSpeed;

    [Space]

    [SerializeField]
    AnimatorController skeletonAnimatorController;
    [SerializeField]
    AnimatorController humanAnimatorController;

    [Space]

    [SerializeField]
    Animator headArmourAnimator;
    [SerializeField]
    Animator torsoArmourAnimator;
    [SerializeField]
    Animator legsArmourAnimator;
    [SerializeField]
    Animator feetArmourAnimator;

    [Space]

    [SerializeField]
    AnimatorController leatherHeadArmourAnimatorController;
    [SerializeField]
    AnimatorController plateHeadArmourAnimatorController;

    [Space]

    [SerializeField]
    AnimatorController leatherTorsoArmourAnimatorController;
    [SerializeField]
    AnimatorController plateTorsoArmourAnimatorController;

    [Space]

    [SerializeField]
    AnimatorController robeLegsArmourAnimatorController;
    [SerializeField]
    AnimatorController plateLegsArmourAnimatorController;

    [Space]

    [SerializeField]
    AnimatorController leatherFeetArmourAnimatorController;
    [SerializeField]
    AnimatorController plateFeetArmourAnimatorController;

    [Space]

    [SerializeField]
    Animator mainWeaponAnimator;

    enum CharacterChoice
    {
        Skeleton,
        Human
    };
    [Space]
    [SerializeField]
    CharacterChoice characterChoice;


    enum PlayerAnimations
    {
        None,

        Idle_Side,
        Walk_Side,
        Slash_Side,
        Bow_Side
    };
    PlayerAnimations playerAnimations;

    enum HeadArmour
    {
        None,
        Leather,
        Plate
    };
    [Space]
    [SerializeField]
    HeadArmour headArmourChoice;

    enum TorsoArmour
    {
        None,
        Leather,
        Plate
    };
    [Space]
    [SerializeField]
    TorsoArmour torsoArmourChoice;

    enum LegsArmour
    {
        None,
        Robe,
        Plate
    };
    [Space]
    [SerializeField]
    LegsArmour legsArmourChoice;

    enum FeetArmour
    {
        None,
        Leather,
        Plate
    };
    [Space]
    [SerializeField]
    FeetArmour feetArmourChoice;

    enum MainWeaponChoice
    {
        None,
        Dagger,
        Rapier,
        Longsword,
        Bow
    };
    [Space]
    [SerializeField]
    MainWeaponChoice mainWeaponChoice;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimatorSpeed();

        CheckCharacterCustomization();
        //HeadArmourAnimate();
        //FeetArmourAnimate();
    }

    void UpdateAnimatorSpeed()
    {
        playerAnimator.SetFloat("animSpeedMultiplier", animationSpeed);

        headArmourAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
        torsoArmourAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
        legsArmourAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
        feetArmourAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
        mainWeaponAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
    }

    void CheckCharacterCustomization()
    {
        mainWeaponAnimator.SetInteger("WeaponType", 0);

        //check the race and if race 0 do skeleton animations, if race 1 do human animations
        switch (characterChoice)
        {
            case CharacterChoice.Skeleton:
                playerAnimator.runtimeAnimatorController = skeletonAnimatorController;
                playerAnimations = PlayerAnimations.None;
                UpdatePlayerAnimations();
                break;

            case CharacterChoice.Human:
                playerAnimator.runtimeAnimatorController = humanAnimatorController;
                playerAnimations = PlayerAnimations.None;
                UpdatePlayerAnimations();
                break;
        }

        switch (headArmourChoice)
        {
            case HeadArmour.Leather:
                headArmourAnimator.runtimeAnimatorController = leatherHeadArmourAnimatorController;
                break;

            case HeadArmour.Plate:
                headArmourAnimator.runtimeAnimatorController = plateHeadArmourAnimatorController;
                break;

            case HeadArmour.None:
                headArmourAnimator.runtimeAnimatorController = null;
                break;
        }

        switch (torsoArmourChoice)
        {
            case TorsoArmour.Leather:
                torsoArmourAnimator.runtimeAnimatorController = leatherTorsoArmourAnimatorController;
                break;

            case TorsoArmour.Plate:
                torsoArmourAnimator.runtimeAnimatorController = plateTorsoArmourAnimatorController;
                break;

            case TorsoArmour.None:
                torsoArmourAnimator.runtimeAnimatorController = null;
                break;
        }

        switch (legsArmourChoice)
        {
            case LegsArmour.Robe:
                legsArmourAnimator.runtimeAnimatorController = robeLegsArmourAnimatorController;
                break;

            case LegsArmour.Plate:
                legsArmourAnimator.runtimeAnimatorController = plateLegsArmourAnimatorController;
                break;

            case LegsArmour.None:
                legsArmourAnimator.runtimeAnimatorController = null;
                break;
        }

        switch (feetArmourChoice)
        {
            case FeetArmour.Leather:
                feetArmourAnimator.runtimeAnimatorController = leatherFeetArmourAnimatorController;
                break;

            case FeetArmour.Plate:
                feetArmourAnimator.runtimeAnimatorController = plateFeetArmourAnimatorController;
                break;

            case FeetArmour.None:
                feetArmourAnimator.runtimeAnimatorController = null;
                break;
        }

        PlayerArmourAnimate();
    }

    void UpdatePlayerAnimations()
    {

        if (playerController.inputX != 0 || playerController.inputY != 0)
        {
            playerAnimations = PlayerAnimations.Walk_Side;
        }
        else
        {
            playerAnimations = PlayerAnimations.Idle_Side;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            MainWeaponAttackPlayer();
        }

        playerAnimator.SetInteger("playerAnimationState", (int)playerAnimations);
    }

    void MainWeaponAttackPlayer()
    {
        switch (mainWeaponChoice)
        {
            case MainWeaponChoice.Dagger:
                playerAnimations = PlayerAnimations.Slash_Side;
                break;
            case MainWeaponChoice.Rapier:
                print("Rapier");
                break;
            case MainWeaponChoice.Longsword:
                print("Longsword");
                break;
            case MainWeaponChoice.Bow:
                playerAnimations = PlayerAnimations.Bow_Side;
                break;
            default:

                break;
        }
        mainWeaponAnimator.SetInteger("WeaponType", (int)mainWeaponChoice);
    }

    

    void PlayerArmourAnimate()
    {
        headArmourAnimator.SetInteger("headArmourAnimations", (int)playerAnimations);
        torsoArmourAnimator.SetInteger("torsoArmourAnimations", (int)playerAnimations);
        legsArmourAnimator.SetInteger("legsArmourAnimations", (int)playerAnimations);
        feetArmourAnimator.SetInteger("feetArmourAnimations", (int)playerAnimations);
    }




}
