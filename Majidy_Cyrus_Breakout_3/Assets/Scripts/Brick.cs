using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Brick : MonoBehaviour
{
    [SerializeField] GameObject hitParticle;
    [SerializeField] GameObject destroyParticle;
    [SerializeField] Transform hitSpawn;
    [SerializeField] AudioClip[] audioClip;

    void Start()
    {
        GetComponent<SpriteRenderer>();
    } 

    /*
    void Update()
    {
        if (transform.position.y < transform.position.y - 3f)
        {
            StartCoroutine("DestroyCoroutine");
        }
    }
    */

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            StartCoroutine("HitCoroutine");
        }

		if (col.gameObject.tag == "Projectile")
		{
			StartCoroutine("HitCoroutine");
		}
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }

    IEnumerator HitCoroutine ()
    {
        PlaySound(0);
        //this.gameObject.SetActive(false);
      
        GameObject p = Instantiate(hitParticle, hitSpawn.position, hitSpawn.rotation) as GameObject;
        transform.DOJump(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.3f, 1, 0.5f);
        //  transform.DORotate(new Vector3(0, 0, 360), 1f);
        transform.DOPunchRotation(new Vector3(0, 0, 360), 2f, 1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.DOMove(new Vector3(transform.position.x, -7, transform.position.z), 2f);
     //   yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        GameObject p2 = Instantiate(destroyParticle, hitSpawn.position, hitSpawn.rotation) as GameObject;
        yield return new WaitForSeconds(1f);
        Destroy(p);
        Destroy(p2);
        Destroy(gameObject);
    }

    /*
    IEnumerator DestroyCoroutine()
    {

    }
    */
}
