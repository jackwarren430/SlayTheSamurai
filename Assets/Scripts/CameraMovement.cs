using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private bool camera_follow = false;

    public GameObject hero;

    void start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void LateUpdate()
    {
        if (camera_follow)
        {
            transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);
        }
       
    }

    public void StartFollowing()
    {
        //StartCoroutine(MoveToOffset());
        camera_follow = true;
        
    }

    IEnumerator MoveToOffset()
    {
        float duration = 1.2f;
        Vector3 start = transform.position;
        Vector3 end = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);
        
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
        camera_follow = true;
        hero.GetComponent<HeroMovement>().StartGame();
    }
}

