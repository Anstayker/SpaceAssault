using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 3.0f;
    [Tooltip("FX prefab on")][SerializeField] GameObject deathFX;

    private void OnTriggerEnter(Collider other) {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }
    
    private void StartDeathSequence() {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene() { // String referenced
        SceneManager.LoadScene(1);
    }
}
