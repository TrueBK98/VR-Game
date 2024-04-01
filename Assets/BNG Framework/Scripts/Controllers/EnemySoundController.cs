using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    AudioSource footstepSource, meleeSource;

    [Header("Footsteps")]
    public List<AudioClip> footsteps;
    public List<AudioClip> meleeHits;
    // Start is called before the first frame update
    protected void Awake()
    {
        footstepSource = GetComponents<AudioSource>()[0];
        meleeSource = GetComponents<AudioSource>()[1];
    }

    public void PlayFootsteps()
    {
        AudioClip clip;

        clip = footsteps[Random.Range(0, footsteps.Count - 1)];

        footstepSource.clip = clip;
        footstepSource.volume = Random.Range(0.4f, 0.6f);
        footstepSource.pitch = Random.Range(0.8f, 1.2f);
        footstepSource.Play();
    }

    public void PlayMeleeHits()
    {
        AudioClip clip;
        clip = meleeHits[Random.Range(0, meleeHits.Count - 1)];

        meleeSource.clip = clip;
        meleeSource.volume = Random.Range(0.4f, 0.6f);
        meleeSource.pitch = Random.Range(0.8f, 1.2f);
        meleeSource.Play();
    }
}
