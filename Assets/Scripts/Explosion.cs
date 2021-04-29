using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _explosionAudio;
    void Start()
    {
        _explosionAudio = GameObject.Find("Explosion_Music").GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.LogError("Audio Source is null");
        }
        _explosionAudio.Play();
        Destroy(gameObject, 2.5f);
    }

    
}
