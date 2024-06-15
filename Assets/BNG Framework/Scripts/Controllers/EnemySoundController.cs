using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    protected AudioSource audioSource;

    [Header("Footsteps")]
    public List<AudioClip> footsteps;
    public List<AudioClip> meleeHits;
    // Start is called before the first frame update
    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootsteps()
    {
        AudioClip clip;

        clip = footsteps[Random.Range(0, footsteps.Count - 1)];
        audioSource.PlayOneShot(clip, 1f);
    }

    public void PlayMeleeHits()
    {
        AudioClip clip;
        clip = meleeHits[Random.Range(0, meleeHits.Count - 1)];

        audioSource.PlayOneShot(clip, 1f);
    }
}
