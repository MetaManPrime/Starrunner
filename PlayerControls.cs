using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f; // Speed control for movement
    [SerializeField] float xRange = 7f; // Limits on movements on the x-axis
    [SerializeField] float yRange = 5f; // Limits on the movement on the y-axis
    [SerializeField] GameObject[] lasers;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation(); // Method for movement
        ProcessRotation(); // Method for controlling rotation
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal"); // This sets the input of the x-axis
        yThrow = Input.GetAxis("Vertical"); // This sets the input of the y-axis

        float xOffset = xThrow * Time.deltaTime * controlSpeed; // This sets a variable for the movement while compensating for variable framerates
        float rawXPos = transform.localPosition.x + xOffset; // This sets the movement along the x-axis
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); //This sets a limit on how far one can move along the x-axis

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation() // Method to rotate the ship naturally during movements
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetKey(KeyCode.F))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
