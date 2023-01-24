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
            default:

                break;
        }
        mainWeaponAnimator.SetInteger("WeaponType", (int)mainWeaponChoice);
    }
}
