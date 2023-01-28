using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    Animator playerAnimator;
    public float animationSpeed;


    [SerializeField]
    AnimatorController skeletonAnimatorController;
    [SerializeField]
    AnimatorController humanAnimatorController;

    [SerializeField]
    Animator headArmourAnimator;
    [SerializeField]
    Animator feetArmourAnimator;

    [SerializeField]
    AnimatorController leatherHeadArmourAnimatorController;
    [SerializeField]
    AnimatorController plateHeadArmourAnimatorController;

    [SerializeField]
    AnimatorController leatherFeetArmourAnimatorController;
    [SerializeField]
    AnimatorController plateFeetArmourAnimatorController;

    [SerializeField]
    Animator mainWeaponAnimator;

    enum CharacterChoice
    {
        Skeleton,
        Human
    };
    [SerializeField]
    CharacterChoice characterChoice;

    enum SkeletonAnimations
    {
        None,

        Idle_Side,
        Walk_Side,
        Slash_Side,
        Bow_Side
    };
    SkeletonAnimations skeletonAnimations;

    enum HumanAnimations
    {
        None,

        Idle_Side,
        Walk_Side,
        Slash_Side,
        Bow_Side
    };
    HumanAnimations humanAnimations;

    enum HeadArmour
    {
        None,
        Leather,
        Plate
    };
    [SerializeField]
    HeadArmour headArmourChoice;

    enum FeetArmour
    {
        None,
        Leather,
        Plate
    };
    [SerializeField]
    FeetArmour feetArmourChoice;

    enum LeatherHeadArmourAnimations
    {
        None,

        Leather_Idle_Side,
        Leather_Walk_Side,
        Leather_Slash_Side,
        Leather_Bow_Side,
    };
    LeatherHeadArmourAnimations leatherHeadArmourAnimations;

    enum PlateHeadArmourAnimations
    {
        None,

        Plate_Idle_Side,
        Plate_Walk_Side,
        Plate_Slash_Side,
        Plate_Bow_Side,
    };
    PlateHeadArmourAnimations plateHeadArmourAnimations;

    enum LeatherFeetArmourAnimations
    {
        None,

        Leather_Idle_Side,
        Leather_Walk_Side,
        Leather_Slash_Side,
        Leather_Bow_Side,
    };
    LeatherFeetArmourAnimations leatherFeetArmourAnimations;

    enum PlateFeetArmourAnimations
    {
        None,

        Plate_Idle_Side,
        Plate_Walk_Side,
        Plate_Slash_Side,
        Plate_Bow_Side,
    };
    PlateFeetArmourAnimations plateFeetArmourAnimations;

    enum MainWeaponChoice
    {
        None,
        Dagger,
        Rapier,
        Longsword,
        Bow
    };
    [SerializeField]
    MainWeaponChoice mainWeaponChoice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimatorSpeed();

        CheckCharacterChoice();
        HeadArmourAnimate();
    }

    /*public void MainWeaponAttack()
    {
        mainWeaponAnimator.SetInteger("WeaponType", (int)mainWeaponChoice);
    }*/

    void UpdateAnimatorSpeed()
    {
        playerAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
        mainWeaponAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
        headArmourAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
        feetArmourAnimator.SetFloat("animSpeedMultiplier", animationSpeed);
    }

    void CheckCharacterChoice()
    {
        mainWeaponAnimator.SetInteger("WeaponType", 0);

        //check the race and if race 0 do skeleton animations, if race 1 do human animations
        switch (characterChoice)
        {
            case CharacterChoice.Skeleton:
                playerAnimator.runtimeAnimatorController = skeletonAnimatorController;
                humanAnimations = HumanAnimations.None;
                UpdateSkeletonAnimations();
                break;

            case CharacterChoice.Human:
                playerAnimator.runtimeAnimatorController = humanAnimatorController;
                skeletonAnimations = SkeletonAnimations.None;
                UpdateHumanAnimations();
                break;
        }
    }

    void HeadArmourAnimate()
    {
        switch (headArmourChoice)
        {
            //animations for leather armour
            case HeadArmour.Leather:
                headArmourAnimator.runtimeAnimatorController = leatherHeadArmourAnimatorController;
                //Skeleton animations for leather armour
                switch (skeletonAnimations)
                {
                    case SkeletonAnimations.Idle_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Idle_Side;
                        break;
                    case SkeletonAnimations.Walk_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Walk_Side;
                        break;
                    case SkeletonAnimations.Slash_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Slash_Side;
                        break;
                    case SkeletonAnimations.Bow_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Bow_Side;
                        break;
                }
                //Human animations for leather armour
                switch (humanAnimations)
                {
                    
                    case HumanAnimations.Idle_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Idle_Side;
                        break;
                    case HumanAnimations.Walk_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Walk_Side;
                        break;
                    case HumanAnimations.Slash_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Slash_Side;
                        break;
                    case HumanAnimations.Bow_Side:
                        leatherHeadArmourAnimations = LeatherHeadArmourAnimations.Leather_Bow_Side;
                        break;
                }
                headArmourAnimator.SetInteger("headArmourAnimations", (int)leatherHeadArmourAnimations);
                break;
                
            case HeadArmour.Plate:
                //Skeleton animations for plate armour
                headArmourAnimator.runtimeAnimatorController = plateHeadArmourAnimatorController;
                switch (skeletonAnimations)
                {
                    case SkeletonAnimations.Idle_Side:
                        plateHeadArmourAnimations = PlateHeadArmourAnimations.Plate_Idle_Side;
                        break;
                    case SkeletonAnimations.Walk_Side:
                        plateHeadArmourAnimations = PlateHeadArmourAnimations.Plate_Walk_Side;
                        break;
                    case SkeletonAnimations.Slash_Side:
                        plateHeadArmourAnimations = PlateHeadArmourAnimations.Plate_Slash_Side;
                        break;
                    case SkeletonAnimations.Bow_Side:
                        plateHeadArmourAnimations = PlateHeadArmourAnimations.Plate_Bow_Side;
                        break;
                }
                //Human animations for plate armour
                switch (humanAnimations)
                {

                    case HumanAnimations.Idle_Side:
                        plateHeadArmourAnimations = PlateHeadArmourAnimations.Plate_Idle_Side;
                        break;
                    case HumanAnimations.Walk_Side:
                        plateHeadArmourAnimations = PlateHeadArmourAnimations.Plate_Walk_Side;
                        break;
                    case HumanAnimations.Slash_Side:
                        plateHeadArmourAnimations= PlateHeadArmourAnimations.Plate_Slash_Side;
                        break;
                    case HumanAnimations.Bow_Side:
                        plateHeadArmourAnimations = PlateHeadArmourAnimations.Plate_Bow_Side;
                        break;
                }
                headArmourAnimator.SetInteger("headArmourAnimations", (int)plateHeadArmourAnimations);
                break;


            case HeadArmour.None:
                headArmourAnimator.runtimeAnimatorController = null;
                break;
        }
        
        
    }

    void FeetArmourAnimate()
    {
        switch (feetArmourChoice)
        {
            //animations for leather armour
            case FeetArmour.Leather:
                feetArmourAnimator.runtimeAnimatorController = leatherFeetArmourAnimatorController;
                //Skeleton animations for leather armour
                switch (skeletonAnimations)
                {
                    case SkeletonAnimations.Idle_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Idle_Side;
                        break;
                    case SkeletonAnimations.Walk_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Walk_Side;
                        break;
                    case SkeletonAnimations.Slash_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Slash_Side;
                        break;
                    case SkeletonAnimations.Bow_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Bow_Side;
                        break;
                }
                //Human animations for leather armour
                switch (humanAnimations)
                {

                    case HumanAnimations.Idle_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Idle_Side;
                        break;
                    case HumanAnimations.Walk_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Walk_Side;
                        break;
                    case HumanAnimations.Slash_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Slash_Side;
                        break;
                    case HumanAnimations.Bow_Side:
                        leatherFeetArmourAnimations = LeatherFeetArmourAnimations.Leather_Bow_Side;
                        break;
                }
                feetArmourAnimator.SetInteger("feetArmourAnimations", (int)leatherFeetArmourAnimations);
                break;

            case FeetArmour.Plate:
                //Skeleton animations for plate armour
                headArmourAnimator.runtimeAnimatorController = plateHeadArmourAnimatorController;
                switch (skeletonAnimations)
                {
                    case SkeletonAnimations.Idle_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Idle_Side;
                        break;
                    case SkeletonAnimations.Walk_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Walk_Side;
                        break;
                    case SkeletonAnimations.Slash_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Slash_Side;
                        break;
                    case SkeletonAnimations.Bow_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Bow_Side;
                        break;
                }
                //Human animations for plate armour
                switch (humanAnimations)
                {

                    case HumanAnimations.Idle_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Idle_Side;
                        break;
                    case HumanAnimations.Walk_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Walk_Side;
                        break;
                    case HumanAnimations.Slash_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Slash_Side;
                        break;
                    case HumanAnimations.Bow_Side:
                        plateFeetArmourAnimations = PlateFeetArmourAnimations.Plate_Bow_Side;
                        break;
                }
                headArmourAnimator.SetInteger("feetArmourAnimations", (int)plateFeetArmourAnimations);
                break;


            case FeetArmour.None:
                feetArmourAnimator.runtimeAnimatorController = null;
                break;
        }


    }

    void UpdateSkeletonAnimations()
    {
        
        if (playerController.inputX != 0 || playerController.inputY != 0)
        {
            skeletonAnimations = SkeletonAnimations.Walk_Side;
        }
        else
        {
            skeletonAnimations = SkeletonAnimations.Idle_Side;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            MainWeaponAttackSkeleton();
        }

        playerAnimator.SetInteger("skeletonAnimations", (int)skeletonAnimations);
    }

    void UpdateHumanAnimations()
    {
        if (playerController.inputX != 0 || playerController.inputY != 0)
        {
            humanAnimations = HumanAnimations.Walk_Side;
        }
        else
        {
            humanAnimations = HumanAnimations.Idle_Side;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            MainWeaponAttackHuman();
        }

        playerAnimator.SetInteger("humanAnimations", (int)humanAnimations);
    }

    void MainWeaponAttackHuman()
    {
        switch (mainWeaponChoice)
        {
            case MainWeaponChoice.Dagger:
                humanAnimations = HumanAnimations.Slash_Side;
                break;
            case MainWeaponChoice.Rapier:
                print("Rapier");
                break;
            case MainWeaponChoice.Longsword:
                print("Longsword");
                break;
            case MainWeaponChoice.Bow:
                humanAnimations = HumanAnimations.Bow_Side; 
                break;
            default:
                
                break;
        }
        mainWeaponAnimator.SetInteger("WeaponType", (int)mainWeaponChoice);
    }

    void MainWeaponAttackSkeleton()
    {
        switch (mainWeaponChoice)
        {
            case MainWeaponChoice.Dagger:
                skeletonAnimations = SkeletonAnimations.Slash_Side;
                break;
            case MainWeaponChoice.Rapier:
                print("Rapier");
                break;
            case MainWeaponChoice.Longsword:
                print("Longsword");
                break;
            case MainWeaponChoice.Bow:
                skeletonAnimations = SkeletonAnimations.Bow_Side;
                break;
            default:

                break;
        }
        mainWeaponAnimator.SetInteger("WeaponType", (int)mainWeaponChoice);
    }
}
