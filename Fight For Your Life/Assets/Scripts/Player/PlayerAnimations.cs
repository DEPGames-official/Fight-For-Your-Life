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

    [SerializeField]
    AnimatorController skeletonAnimatorController;
    [SerializeField]
    AnimatorController humanAnimatorController;

    [SerializeField]
    Animator headArmourAnimator;

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
        Idle_Side,
        Walk_Side,
        Slash_Side,
        Bow_Side
    };
    SkeletonAnimations skeletonAnimations;

    enum HumanAnimations
    {
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

    enum HeadArmourAnimations
    {
        None,

        Leather_Idle_Side,
        Leather_Walk_Side,
        Leather_Slash_Side,

        Plate_Idle_Side,
        Plate_Walk_Side
    };
    HeadArmourAnimations headArmourAnimations;

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
        CheckCharacterChoice();
        HeadArmourAnimate();
    }

    /*public void MainWeaponAttack()
    {
        mainWeaponAnimator.SetInteger("WeaponType", (int)mainWeaponChoice);
    }*/

    public void CheckCharacterChoice()
    {
        mainWeaponAnimator.SetInteger("WeaponType", 0);

        //check the race and if race 0 do skeleton animations, if race 1 do human animations
        switch (characterChoice)
        {
            case CharacterChoice.Skeleton:
                playerAnimator.runtimeAnimatorController = skeletonAnimatorController;
                UpdateSkeletonAnimations();
                break;

            case CharacterChoice.Human:
                playerAnimator.runtimeAnimatorController = humanAnimatorController;
                UpdateHumanAnimations();
                break;
        }
    }

    public void HeadArmourAnimate()
    {
        
        switch (headArmourChoice)
        {
            //animations for leather armour
            case HeadArmour.Leather:
                //Skeleton animations for leather armour
                switch (skeletonAnimations)
                {
                    case SkeletonAnimations.Idle_Side:
                        headArmourAnimations = HeadArmourAnimations.Leather_Idle_Side;
                        break;
                    case SkeletonAnimations.Walk_Side:
                        headArmourAnimations = HeadArmourAnimations.Leather_Walk_Side;
                        break;
                    case SkeletonAnimations.Slash_Side:
                        headArmourAnimations = HeadArmourAnimations.Leather_Slash_Side;
                        break;
                }
                //Human animations for leather armour
                switch (humanAnimations)
                {
                    case HumanAnimations.Idle_Side:
                        headArmourAnimations = HeadArmourAnimations.Leather_Idle_Side;
                        break;
                    case HumanAnimations.Walk_Side:
                        headArmourAnimations = HeadArmourAnimations.Leather_Walk_Side;
                        break;
                    case HumanAnimations.Slash_Side:
                        break;
                }
                break;

            case HeadArmour.Plate:
                break;


            case HeadArmour.None:
                headArmourAnimations = HeadArmourAnimations.None;
                break;
        }
        
        headArmourAnimator.SetInteger("headArmourAnimations", (int)headArmourAnimations);
    }

    public void UpdateSkeletonAnimations()
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

    public void UpdateHumanAnimations()
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

    public void MainWeaponAttackHuman()
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

    public void MainWeaponAttackSkeleton()
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
