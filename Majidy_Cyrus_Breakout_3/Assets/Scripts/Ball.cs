using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    TrailRenderer _trail;

    Tween ballShakeTween;
    Tween screenShakeTween;

	Vector2 velocity;

    //My variables.
    Tween ballLineRender;

    [SerializeField]
    GameObject particle;
    [SerializeField]
    GameObject particle2;
    [SerializeField]
    Transform hitSpawn;

	//Ball's Starting Velocity
 	Vector2 startingVelocity;

    void Start()
    {
		//Ball's Starting Velocity
		startingVelocity = new Vector2(Random.Range(-200f,200f), -200f);
		startingVelocity = startingVelocity * Time.deltaTime;
		startingVelocity = startingVelocity;
        _rigidbody = GetComponent<Rigidbody2D>();
		velocity = startingVelocity;
        //ballLineRender = DOColor(new Color2(Color.white, Color.white), new Color2(Color.yellow, Color.green), 1);
        _trail = GetComponent<TrailRenderer>();
        _trail.DOResize(0.6f, 0.1f, 1f);
    }

	void FixedUpdate()
	{
		_rigidbody.velocity = velocity;
	}

    void OnCollisionEnter2D(Collision2D c)
    {
		//TODO:REFLECTION
		//Accesses array w/ predefined functions.
		velocity = Vector2.Reflect (velocity, c.contacts[0].normal);

        //Shake ball scale tween.
        if (ballShakeTween != null && ballShakeTween.IsPlaying())
        {
            ballShakeTween.Complete();
        }

        ballShakeTween = transform.DOShakeScale(1f, 0.5f);

        //Screen shake tween.
        if (screenShakeTween != null && screenShakeTween.IsPlaying())
        {
            screenShakeTween.Complete();
        }

        screenShakeTween = Camera.main.transform.DOShakePosition(0.45f, 0.45f);

        if (gameObject.CompareTag("Kill Trigger"))
        {
            StartCoroutine("DestroyCoroutine");
        }

        if (gameObject.CompareTag("Wall"))
        {
            StartCoroutine("HitWallCoroutine");
        }

    /*
    if (gameObject.CompareTag("Paddle"))
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForceAtPosition();
            //StartCoroutine("HitWallCoroutine");
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        */
    }

    IEnumerator DestroyCoroutine()
    {
        this.gameObject.SetActive(false);
        Instantiate(particle, hitSpawn.position, hitSpawn.rotation);
        yield return new WaitForSeconds(1f);
        Destroy(particle.gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene("Main_11");
    }

    IEnumerator HitWallCoroutine()
    {
        GameObject p = Instantiate(particle2, hitSpawn.position, hitSpawn.rotation) as GameObject;
        yield return new WaitForSeconds(1f);
        Destroy(p);
    }
}
