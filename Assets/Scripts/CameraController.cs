using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float bottomLimit = 0.0f;
    public float topLimit = 0.0f;

    public GameObject subScreen;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = this.transform.position.z;

            if (x < leftLimit)
            {
                x = leftLimit;
            }else if (x > rightLimit)
            {
                x = rightLimit;
            }

            if(y < bottomLimit)
            {
                y = bottomLimit;
            } else if (y > topLimit)
            {
                y = topLimit;
            }

            this.transform.position = new Vector3(x, y, z);

            if (subScreen != null)
            {
                subScreen.transform.position = new Vector3(
                    x * 0.5f,
                    subScreen.transform.position.y,
                    subScreen.transform.position.z
                    );
            }
        }
    }
}
