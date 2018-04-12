﻿using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float projectileLaserSpeed;
	public float health = 150f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	
	
	void Start () {
		GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			health -= laser.getDamage();
			laser.Hit();
			if (health <= 0) {
				Destroy (gameObject);
			}
		}
	}
	
	void Fire() {
		Vector3 beam1 = new Vector3(transform.position.x-0.5f,transform.position.y-0.2f); 
		Vector3 beam2 = new Vector3(transform.position.x+0.5f,transform.position.y-0.2f);
		GameObject laserBeam1 = Instantiate(projectile,beam1,Quaternion.identity) as GameObject;
		laserBeam1.rigidbody2D.velocity = new Vector3(0f, -projectileLaserSpeed,0f);
		GameObject laserBeam2 = Instantiate(projectile,beam2,Quaternion.identity) as GameObject;
		laserBeam2.rigidbody2D.velocity = new Vector3(0f, -projectileLaserSpeed,0f);
	}
	
	void Update () {
		float probability = shotsPerSecond * Time.deltaTime;
		if (Random.value < probability) {
		Fire ();
		}
	}

}


// \n
// <>