using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSoundController : EnemySoundController
{
    public List<AudioClip> zombieNoises;
    int count = 0;
    int maxCount;

    new void Awake()
    {
        base.Awake();
        maxCount = Random.Range(300, 800);
        //Debug.Log(zombieNoises.Count);
    }
    private void FixedUpdate()
    {
        count++;

        if (count > maxCount)
        {
            count = 0;
            maxCount = Random.Range(300, 800);
            
            AudioClip clip;
            clip = zombieNoises[Random.Range(0, zombieNoises.Count)];
            audioSource.PlayOneShot(clip, 1f);
        }
    }
}
