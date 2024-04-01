using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSoundController : EnemySoundController
{
    public List<AudioClip> zombieNoises;
    AudioSource zombieSource;
    int count = 0;
    int maxCount;

    new void Awake()
    {
        base.Awake();
        zombieSource = GetComponents<AudioSource>()[2];
        maxCount = Random.Range(300, 800);
        Debug.Log(zombieNoises.Count);
    }
    private void FixedUpdate()
    {
        count++;

        if (count > maxCount)
        {
            count = 0;
            maxCount = Random.Range(300, 800);
            
            AudioClip clip;
            clip = zombieNoises[Random.Range(0, zombieNoises.Count - 1)];

            zombieSource.clip = clip;
            zombieSource.volume = Random.Range(0.4f, 0.6f);
            zombieSource.pitch = Random.Range(0.8f, 1.2f);
            zombieSource.Play();
        }
    }
}
