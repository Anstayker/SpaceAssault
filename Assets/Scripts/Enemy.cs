using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [Tooltip("Score on destroy")][SerializeField] int scorePerHit = 0;
    [SerializeField] int hitPoints = 5;

    ScoreBoard scoreBoard;

    private void Start() {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider() {
        Collider newCollider = gameObject.AddComponent<BoxCollider>();
        newCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other) {
        scoreBoard.ScoreHit(scorePerHit);
        hitPoints--;
        if(hitPoints <= 0) {
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            Destroy(gameObject); 
        }
    }

}
