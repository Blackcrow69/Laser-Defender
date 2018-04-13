 using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float shipSpeed = 1.0f;
	public float padding = 0.7f;
	public GameObject projectileLaser;
	public float projectileLaserSpeed;
	public float fireRate = 0.2f;
	public float health = 250f;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	float xMin = -5f;
	float xMax = 5f;
	
	void control ()
	{
		if (Input.GetKey(KeyCode.LeftArrow)) {
			//transform.position += new Vector3(- shipSpeed*Time.deltaTime, 0f, 0f);
			transform.position+= Vector3.left * shipSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			//transform.position += new Vector3(shipSpeed*Time.deltaTime, 0f, 0f);
			transform.position+= Vector3.right * shipSpeed * Time.deltaTime;
			}
		
		//restrict to gamespace
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void Fire (){
		//replicate TIE fighter laser pattern
		Vector3 beam1 = new Vector3(transform.position.x-0.01f,transform.position.y+0.65f); 
		Vector3 beam2 = new Vector3(transform.position.x+0.01f,transform.position.y+0.65f);
		
		GameObject laserBeam1 = Instantiate (projectileLaser, beam1, Quaternion.identity) as GameObject;
		GameObject laserBeam2 = Instantiate (projectileLaser, beam2, Quaternion.identity) as GameObject;
		laserBeam1.rigidbody2D.velocity = new Vector3(0f, projectileLaserSpeed,0f);
		laserBeam2.rigidbody2D.velocity = new Vector3(0f, projectileLaserSpeed,0f);
		AudioSource.PlayClipAtPoint(fireSound, transform.position,0.025f);
	}
	
	
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z-Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		control();
		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire",0.000001f,fireRate);
		}else if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			health -= laser.getDamage();
			laser.Hit();
			if (health <= 0) {
				Destroy (gameObject);
				AudioSource.PlayClipAtPoint(deathSound,transform.position,0.007f);
				LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
				man.LoadLevel("Win Screen");
			}
		}
	}
}
