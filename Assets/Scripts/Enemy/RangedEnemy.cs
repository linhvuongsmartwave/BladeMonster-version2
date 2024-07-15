using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    bool canDamaged=false;
    public GameObject bullet;
    public float bulletForce;
    public float rangedAttackDistance = 5f;
    float rangedAttackTimer = 0f;
    public float rangedAttackInterval = 3f;
    public RangedState rangedState;
    public enum RangedState
    {
        Idle,
        Attack,
        Damaged
    }
    public void Awake()
    {
        base.Awake();
        rangedState = RangedState.Idle;

    }
    void Update()
    {
        //healthBar.SetHealth(currentHealth);

        CheckRangedState();
        CheckDistancePlayer();

    }
    public void CheckDistancePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= 1.2f && !canDamaged)
        {
            canDamaged = true;
            Damaged();
        }
        else if (distanceToPlayer > 1.2f) canDamaged = false;
        if (distanceToPlayer <= rangedAttackDistance)
        {
            rangedAttackTimer += Time.deltaTime;
            if (rangedAttackTimer >= rangedAttackInterval)
            {
                RangedAttack(); 
                rangedAttackTimer = 0f;
            }
        }
    }
    public void CheckRangedState()
    {
        switch (rangedState)
        {
            case RangedState.Idle:
                RangedIdle();
                break;
            case RangedState.Damaged:
                RangedDamaged();
                break;
            case RangedState.Attack:
                RangedAttack();
                break;
        }
    }
    #region RangedState
    void RangedIdle()
    {
        animator.SetTrigger(Const.rangedIdle);
    }
    void RangedDamaged()
    {
        animator.SetTrigger(Const.rangedDamaged);

    }
    void RangedAttack()
    {
        animator.SetTrigger(Const.rangedAttack);
    }

    void Shoot()
    {
        GameObject fireBullet = Instantiate(bullet, new Vector2(transform.position.x - 0.5f, 0f), transform.rotation);
        Bullet bulletScript = fireBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetRangedEnemy(this);
        }
        Rigidbody2D rb = fireBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
    }
    #endregion

    public void TakeDamagePlayer()
    {
        player.TakeDamageEnemy(attack);
    }

    void Damaged()
    {
        TakeDamage(10);
        if (currentHealth > 0)
        {
            RangedDamaged();
            Move();
        }
    }
}
