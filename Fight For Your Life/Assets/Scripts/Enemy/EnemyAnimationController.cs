using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField]
    public Animator enemyAnimator;

    public enum EnemyAnimations
    {
        None,

        Idle_Side,
        Walk_Side,
        Attack_Side
    };
    public EnemyAnimations enemyAnimations;

    private void Update()
    {

    }

    public void UpdateEnemyAnimations(bool outOfAttackRange)
    {
        if (outOfAttackRange == true)
        {
            enemyAnimations = EnemyAnimations.Walk_Side;
        }
        else if(outOfAttackRange == false)
        {
            enemyAnimations = EnemyAnimations.Attack_Side;
            
        }

        enemyAnimator.SetInteger("enemyAnimationState", (int)enemyAnimations);


    }
}

