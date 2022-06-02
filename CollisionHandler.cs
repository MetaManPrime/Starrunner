using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeDelay = 1f;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem godModeParticles;
    [SerializeField] AudioClip explosion;

    ParticleSystem particleSystem;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionDisabled = false;
    bool godModeIsOn = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DisableCollisions();
        EnableCollisions();
    }

    void DisableCollisions()
    {
        if (!godModeIsOn)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Hit the C key");
                godModeParticles.Play();
                isCollisionDisabled = !isCollisionDisabled;
                godModeIsOn = true;
            }
        }
    }

    void EnableCollisions()
    {
        if (godModeIsOn)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("Disable Godmode");
                godModeParticles.Stop();
                isCollisionDisabled = !isCollisionDisabled;
                godModeIsOn = false;
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (isTransitioning || isCollisionDisabled) { return; }

        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        crashParticles.Play();
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("ReloadLevel", timeDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
