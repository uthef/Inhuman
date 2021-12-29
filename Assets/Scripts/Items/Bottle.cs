using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource aSource;
    new Rigidbody rigidbody;
    public bool silent = true;

    void Awake()
    {
        aSource = transform.Find("Sound").GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Bump(Vector3 movement)
    {
        silent = false;
        if (!aSource.isPlaying) aSource.PlayOneShot(audioClips[1]);
        rigidbody.AddForce(movement * 5f * Time.deltaTime, ForceMode.Impulse);
        rigidbody.AddTorque(movement * 60f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
        if (!silent && !aSource.isPlaying) aSource.PlayOneShot(clip);
    }

    void Update()
    {
        
    }
}
