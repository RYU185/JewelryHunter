using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    public GameObject door;
    public Sprite imageOn;
    public Sprite imageOff;
    bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (on)
            {
                on = false;
                this.GetComponent<SpriteRenderer>().sprite = imageOff;
                door.GetComponent<MovingBlock>().isCanMove = false;
            }
            else
            {
                on = true;
                this.GetComponent<SpriteRenderer>().sprite = imageOn;
                door.GetComponent<MovingBlock>().isCanMove = true;
            }
        }    
    }
}
