using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] Transform ballTransform;
	[SerializeField] float ballStart;

    float input;

    Rigidbody2D _rigidbody;

	int score = 0;

    [SerializeField]
    GameObject particle;
    [SerializeField]
    Transform hitSpawn;
	//[SerializeField] Transform p2;

    
    [SerializeField]
    Transform brickHitSpawn;
    [SerializeField]
    Transform destroyParticle;
    

    [SerializeField]
    AudioClip[] audioClip;

	// Shooting
	[SerializeField] GameObject bulletEmitter;
	[SerializeField] GameObject bullet;
	[SerializeField] float bulletSpeed;

	int ammo = 0;
	bool canShoot;

	// UI Score and Ammo.
	[SerializeField] Text scoreText;
	[SerializeField] Text Ammo;
	[SerializeField] Text Loose;

    void Start ()
    {
		speed = speed * Time.deltaTime;
		UIRoutine();
	//	p2 = GetComponent<Transform> ();
        _rigidbody = GetComponent<Rigidbody2D>();
        ballTransform.position = transform.position + (Vector3.up * ballStart);
        score = 0;
    }
	
	void Update ()
    {
        transform.localScale += new Vector3(-0.001F, 0, 0);

        input = Input.GetAxisRaw("Horizontal");

        /*
        if(ballTransform.position.y < transform.position.y -4f)
        {
           p2 = Instantiate(destroyParticle, hitSpawn.position, hitSpawn.rotation);
        }
        */

        if (ballTransform.position.y < transform.position.y - 5f)
        {
            Debug.Log("Loose.");
            //You loose.
			StartCoroutine("LooseCoroutine");
            
        }

		// Shooting
		if (ammo > 0) 
		{
			canShoot = true;
		} 
		else
			canShoot = false;

		if (canShoot == true) 
		{
			if (Input.GetKeyDown ("space")) 
			{
				GameObject temporaryBulletHandler
				= Instantiate (bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

				Rigidbody2D temporaryRigidbody = temporaryBulletHandler.GetComponent<Rigidbody2D> ();

				temporaryRigidbody.AddForce (transform.up * bulletSpeed);

				ammo--;

				transform.localScale += new Vector3(-0.15F, 0, 0);
			}
		}
	}

    void FixedUpdate ()
    {
     	_rigidbody.velocity = new Vector2(input * speed, 0f);
    }

    void OnCollisionEnter2D (Collision2D c)
    {
        StartCoroutine("HitCoroutine");
    }

    void PlaySound (int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }

	IEnumerator LooseCoroutine()
	{
		Loose.text = "You Loose!";
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    IEnumerator HitCoroutine ()
    {
		score = score + 10;
		ammo++;
        PlaySound(0);
        GameObject p = Instantiate(particle, hitSpawn.position, hitSpawn.rotation) as GameObject;
        transform.localScale += new Vector3(0.1F, 0, 0);
        yield return new WaitForSeconds(1f);
        Destroy(p);
		UIRoutine();
    }

	void UIRoutine()
	{
		//Score text
		scoreText.text = "Score: " + score.ToString();

		//Ammo text
		if (ammo > 10) 
		{
			Ammo.text = "Ammo: " + ammo.ToString ();
		}
		else if (ammo < 10)
			Ammo.text = "Ammo:0 " + ammo.ToString ();
	}
}
