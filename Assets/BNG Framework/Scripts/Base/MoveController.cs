using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed;

    public virtual void Move(float vertical, float horizontal)
    {
        transform.position += ((transform.forward * vertical) + (transform.right * horizontal)) * speed * Time.deltaTime;
    }
}
