using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    int minCoins = 1;
    int maxCoins = 3;
    public Player player;
    public int currentHealth;
    public GameObject prefabCoins;
    public Transform playerTransform;
    public HealthBar healthBar;
    public Animator animator;
    public int expAmount;
    public EnemyData enemyData;
    public int attack;


    public void Awake()
    {
        expAmount = enemyData.exp;
        currentHealth = enemyData.health;
        attack = enemyData.attack;
        healthBar.SetMaxHealth(currentHealth);
        animator = GetComponent<Animator>();

    }

    public void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
        player = FindObjectOfType<Player>();
    }

    public void NewPos(Vector2 newPos)
    {
        transform.DOMove(newPos, 0.3f).SetEase(Ease.Linear);
    }

    public  void Move()
    {
        StartCoroutine(MoveBack());
    }

    public IEnumerator MoveBack()
    {
        float elapsedTime = 0f;
        float duration = 0.4f;
        Vector2 initialPosition = transform.position;
        Vector2 newPosition = (Vector2)transform.position + (Vector2)transform.right * 3f;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(initialPosition, newPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = newPosition;
    }

    public  void Die()
    {
        float duration = 0.5f;
        Vector2 targetPosition = new Vector2(transform.position.x + 20f, transform.position.y + 10f);
        transform.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
        transform.DORotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        SpawmGolds();
        ExpManager.Instance.AddExp(expAmount);

    }

    public void SpawmGolds()
    {
        int numCoins = Random.Range(minCoins, maxCoins + 1);
        for (int i = 0; i < numCoins; i++)
        {
            float randomX = transform.position.x + Random.Range(0f,3f) ;
            float randomY = transform.position.y ;
            Vector2 randomPosition = new Vector2(randomX, randomY);
            GameObject coin = Instantiate(prefabCoins, randomPosition, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            coin.transform.DOMoveY(randomY + 1f, 0.4f).SetEase(Ease.OutQuad) 
            .OnComplete(() =>   
            {
                coin.transform.DOMoveY(randomY-0.5f, 0.4f).SetEase(Ease.InQuad);
            });
        }
    }

    public  void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            healthBar.gameObject.SetActive(false);
            Die();
        }
    }
}
