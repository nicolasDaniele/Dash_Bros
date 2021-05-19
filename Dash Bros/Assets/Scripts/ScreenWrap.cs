using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wrapper"))
        {
            // If X position is negative, add 0.2 to it and invert
            // so that the character won't appear right in the other wrapper
            // but in a distance of 0.2.
            //For the positive X position, it's the other way around.
            float newXPos;
            if (transform.position.x < 0)
            {
                newXPos = transform.position.x + 0.2f;
            }
            else
            {
                newXPos = transform.position.x - 0.2f;
            }
            transform.position = new Vector3(-newXPos, transform.position.y, transform.position.z);
        }
    }
}
