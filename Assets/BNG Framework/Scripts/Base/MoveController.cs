using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed;
    Rigidbody body;

    protected void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public virtual void Move(float vertical, float horizontal)
    {
        /*Vector3 movement = new Vector3(horizontal, 0, vertical);
        Vector3 direction = body.rotation * movement;
        body.MovePosition(body.position + direction * speed * Time.deltaTime);*/

        body.AddRelativeForce(Vector3.forward * vertical * speed);
        body.AddRelativeForce(Vector3.right * horizontal * speed);
    }
}
