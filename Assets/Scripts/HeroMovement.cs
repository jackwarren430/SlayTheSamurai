using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{

    private float dashingSpeed = 11f;
    private readonly float DASH_TIME = 0.25f;
    private float dashCooldown;
    private bool canDash;

    private float walkingSpeed = 2f;
    
    private bool inPlay;
    private HeroState heroState;

    private Sprite hero;
    private Sprite hero_strike;
    private Sprite hero_deflect;

    private GameObject badGuy;

    void Start()
    {
        inPlay = false;
        canDash = false;
        dashCooldown = DASH_TIME;
        transform.position = new Vector3(0, -10, 0);
        badGuy = GameObject.Find("enemy1");
        hero = Resources.Load<Sprite>("Sprites/hero/hero");
        hero_strike = Resources.Load<Sprite>("Sprites/hero/hero_strike");
        hero_deflect = Resources.Load<Sprite>("Sprites/hero/hero_deflect");
        GetComponent<SpriteRenderer>().sprite = hero;

        StartCoroutine(MoveForSeconds());

    }

    void Update()
    {
        
        if (inPlay){
            Debug.Log("HeroState:  " + heroState);
            float distance = Vector3.Distance(transform.position, badGuy.transform.position);

            switch (heroState)
            {
                case HeroState.Walking:
                    if (Input.GetKey(KeyCode.UpArrow) && canDash)
                    {
                        canDash = false;
                        heroState = HeroState.Dashing;
                        StartCoroutine(DashCooldown());
                        break;
                    }
                    transform.Translate(0, walkingSpeed * Time.deltaTime, 0);
                    break;
                case HeroState.Dashing:
                    transform.Translate(0, dashingSpeed * Time.deltaTime, 0);
                    break;
                case HeroState.Waiting:
                    break;
                case HeroState.Striking:
                    break;
                case HeroState.Shielding:
                    break;
                case HeroState.Deflecting:
                    break;
                default:
                    Debug.Log("No HeroState set");
                    break;
            }
        }
    }

    public void StartGame()
    {
        inPlay = true;
        canDash = true;
        heroState = HeroState.Walking;
    }


        //  Coroutines

    IEnumerator MoveForSeconds()
    {
        float duration = 1.5f;
        float elapsed = 0f;
        float speed = 4.0f;
        while (elapsed < duration)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.GetComponent<CameraMovement>().StartFollowing();
        StartGame();
    }

     IEnumerator DashCooldown()
    { 
        while (inPlay && dashCooldown > 0.0)
        {
            dashCooldown -= Time.deltaTime;
            yield return null;
        }
        heroState = HeroState.Walking;
        while (inPlay && dashCooldown < DASH_TIME)
        {
            dashCooldown += Time.deltaTime / 100;
        }
        dashCooldown = DASH_TIME;
        canDash = true;
    }

/*
    void StartEnemyEncounter(){
        inEnemyEncounter = true;
        Debug.Log("encounter");
    }

    void KillEnemy()
    {
        badGuy.GetComponent<EnemyBehavior>().Kill();
        
    }

    IEnumerator Walk()
    {
        float speed = 0.001f;
        while (!inEnemyEncounter)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            yield return null;
        }
    }

    IEnumerator Dash()
    {
        GetComponent<SpriteRenderer>().sprite = hero_strike;
        canDash = false;
        speed *= 3;
        float duration = 0.2f;
        float elapsed = 0f;
        while (elapsed < duration && !inEnemyEncounter)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        speed /= 3;
        GetComponent<SpriteRenderer>().sprite = hero;
        StartCoroutine(Cooldown());
    }

    IEnumerator Strike()
    {
        GetComponent<SpriteRenderer>().sprite = hero_strike;
        canDash = false;
        float duration = 0.2f;
        float elapsed = 0f;
        while (elapsed < duration && inPlay)
        {
            float distance = Vector3.Distance(transform.position, badGuy.transform.position);
            if (distance < 2){
                KillEnemy();
            }
            transform.Translate(0, 3 * speed * Time.deltaTime, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().sprite = hero;
        StartCoroutine(Cooldown());
    }

    

   

    */

    private enum HeroState
    {
        Walking,
        Dashing,
        Striking,
        Waiting,
        Deflecting,
        Shielding
    }

}
