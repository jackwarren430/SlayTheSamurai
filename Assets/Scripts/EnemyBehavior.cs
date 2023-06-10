using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject hero;
    private bool isAlive;
    private bool isAttacking;
    private float distance;

    private Sprite enemy;
    private Sprite enemyStrike;
    private Sprite enemyCharge;

    void Start()
    {
        isAlive = true;
        isAttacking = false;

        enemy = Resources.Load<Sprite>("Sprites/enemy/enemy");
        enemyStrike = Resources.Load<Sprite>("Sprites/enemy/enemy_strike");
        enemyCharge = Resources.Load<Sprite>("Sprites/enemy/enemy_charge");
    }

    void Update()
    {
        if (isAlive) {
            distance = Vector3.Distance(hero.transform.position, transform.position);
            if (distance < 10 && !isAttacking)
            {
                StartCoroutine(Inspect());
            }
        }
    }

    public void Kill()
    {
        isAlive = false;
    }

    IEnumerator Inspect(){
        float elapsed = 0f;
        float duration = 1.3f;
        float speed = 0.002f;
        while (elapsed < duration)
        {
            transform.Translate(speed * Time.deltaTime, -speed * Time.deltaTime, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        GetComponent<SpriteRenderer>().sprite = enemyCharge;
        isAttacking = true;
        float speed = 0.01f;
        while (isAttacking && isAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, speed * Time.deltaTime);
            if (distance < 1)
            {
                StartCoroutine(Strike());
                isAttacking = false;
            }
            yield return null;
        }
        
    }

    IEnumerator Strike()
    {
        GetComponent<SpriteRenderer>().sprite = enemyStrike;
        isAttacking = false;
        float speed = 0.01f;
        while (isAlive)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            yield return null;
        }
    }


}
