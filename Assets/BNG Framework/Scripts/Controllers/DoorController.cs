using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator DoorAnim;

    // Start is called before the first frame update
    void Start()
    {
        DoorAnim = GetComponent<Animator>();
    }

    public void DoorClosed()
    {
        if (name.Contains("1"))
        {
            DoorAnim.Play("door_1_closed");
        }
        else if (name.Contains("2"))
        {
            DoorAnim.Play("door_2_closed");
        }
        else if (name.Contains("3"))
        {
            DoorAnim.Play("door_3_closed");
        }
    }

    public void DoorOpened()
    {
        if (name.Contains("1"))
        {
            DoorAnim.Play("door_1_opened");
        }
        else if (name.Contains("2"))
        {
            DoorAnim.Play("door_2_opened");
        }
        else if (name.Contains("3"))
        {
            DoorAnim.Play("door_3_opened");
        }
    }
}
