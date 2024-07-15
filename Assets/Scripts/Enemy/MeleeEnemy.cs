using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float moveSpeed = 1f;
    public float meleeAttackDistance = 1f;
    private bool canAttack = false;


    public MeleeState meleeState;
    public enum MeleeState
    {
        Idle,
        Moving,
        Attack,
        Damaged
    }

    public void Awake()
    {
        base.Awake();
        meleeState = MeleeState.Idle;

    }

    void Update()
    {
        //healthBar.SetHealth(currentHealth);
        CheckMeleeState();
        CheckDistancePlayer();
    }
    public void CheckMeleeState()
    {
        if (meleeState == MeleeState.Damaged) return;
        switch (meleeState)
        {
            case MeleeState.Idle:
                MeleeIdle();
                break;
            case MeleeState.Moving:
                MeleeMoving();
                break;
            case MeleeState.Damaged:
                MeleeDamaged();
                break;
            case MeleeState.Attack:
                MeleeAttack();
                break;
        }
    }
 
    #region MeleeState
    void MeleeIdle()
    {
        animator.SetTrigger(Const.meleeIdle);
    }
    void MeleeMoving()
    {
        animator.SetTrigger(Const.meleeMove);
    }
    void MeleeDamaged()
    {
        animator.SetTrigger(Const.meleeDamaged);

    }
    void MeleeAttack()
    {
        TakeDamagePlayer();
        animator.SetTrigger(Const.meleeAttack);
    }
    #endregion

    void TakeDamagePlayer() // khi chay ham nay thi Player se mat mau
    {
        player.TakeDamageEnemy(attack);
    }
    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        MeleeMoving();
    }
    public void CheckDistancePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= 5f) MoveToPlayer();
        if (distanceToPlayer <= meleeAttackDistance && !canAttack)
        {
            Damaged();
            canAttack = true;
            MeleeAttack();
        }
        else if (distanceToPlayer > meleeAttackDistance) canAttack = false;
    }
    void Damaged()
    {
        TakeDamage(10);
        if (currentHealth > 0 )
        {
            MeleeDamaged();
            Move();

        }
    }
}
