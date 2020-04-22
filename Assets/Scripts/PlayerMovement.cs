using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    [Header("General")]
    [Tooltip("In M/S")][SerializeField] float controlSpeed = 45.0f;
    [Tooltip("In m")][SerializeField] float xRange = 20.0f;
    [Tooltip("In m")][SerializeField] float yRange = 13.0f;
    [SerializeField] GameObject[] guns;

    [Header("Screen Position based")]
    [SerializeField] float positionPitchFactor = -3.0f;
    [SerializeField] float controllPitchFactor = -20.0f;

    [Header("Control Throw Based")]
    [SerializeField] float positionYawFactor = 2.5f;
    [SerializeField] float controllRollFactor = 20.0f;

    float xThrow = 0.0f, yThrow = 0.0f;
    bool isControlEnabled = true;

    void Update() {
        if (isControlEnabled) {
            ProcessMovement();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void OnPlayerDeath() { //String Reference
        isControlEnabled = false;
    }

    private void ProcessMovement() {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, - yRange, yRange);

        transform.localPosition = new Vector3(
                                    clampedXPos, 
                                    clampedYPos, 
                                    transform.localPosition.z);
    }

    private void ProcessRotation() {
        float pitch = transform.localPosition.y * positionPitchFactor
                    + yThrow * controllPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controllRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring() {
        if(CrossPlatformInputManager.GetButton("Fire")) {
            SetGunsActivate(true);
        } else {
            SetGunsActivate(false);
        }
    }

    private void SetGunsActivate(bool isActive) {
        foreach (GameObject gun in guns) { // may affect death FX
            ParticleSystem.EmissionModule emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
    
}
