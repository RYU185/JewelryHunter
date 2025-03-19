using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject shell; // ÇÁ¸®Æé µî·Ï
    public float delayTime = 3.0f;
    public float fireSpeedX = -4.0f;
    public float fireSpeedY = 0.0f;
    public float detectingRange = 8.0f;

    GameObject player;
    GameObject muzzle;
    float waitingTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        muzzle = transform.Find("muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {   
        waitingTime += Time.deltaTime;
        if (CheckRange(player.transform.position))
        {
            if (waitingTime > delayTime)
            {
                waitingTime = 0;
                Vector3 pos = muzzle.transform.position;
                GameObject shellObj = Instantiate(shell, pos, Quaternion.identity);
                shellObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(fireSpeedX, fireSpeedY), ForceMode2D.Impulse);
            }
        }
      
    }

    bool CheckRange(Vector2 target)
    {
        float distance = Vector2.Distance(this.transform.position, target);
        if(distance <= detectingRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
