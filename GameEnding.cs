using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] float displayImageDuration = 1f;
    [SerializeField] ParticleSystem explosionParticles;

    public CanvasGroup exitBackgroundImageCanvasGroup;
    public GameObject laser;

    ParticleSystem particleSystem;

    bool mineIsDestroyed;
    float m_Timer;
    
    void OnParticleCollision (GameObject other)
    {
        if (other.gameObject == laser)
        {
            explosionParticles.Play();
            GetComponent<MeshRenderer>().enabled = false;
            mineIsDestroyed = true;
        }
    }

    void Update()
    {
        if (mineIsDestroyed)
        {
            Debug.Log("EndLevel engaged");
            EndLevel();
        }
        
    }

    void EndLevel()
    {
        m_Timer += Time.deltaTime;
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            Debug.Log("Application Ended");
            Application.Quit();
        }
    }

}
