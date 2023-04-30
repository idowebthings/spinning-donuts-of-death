using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillProductivity : MonoBehaviour
{
    public ProductivityTracker productivityTracker;
    public ParticleSystem workerParticle;
    public ParticleSystem computerParticle;
    public ParticleSystem scrumMasterHitParticle;
    public ParticleSystem scrumMasterKillParticle;
    public AudioClip hitSound;
    public AudioClip scrumMasterKillSound;
    private AudioSource donutAudio;
    public int pointsHigh = 40;
    public int pointsMed = 15;
    public int pointsLow = 5;

    // Start is called before the first frame update
    void Start()
    {
        productivityTracker = GameObject.Find("Productivity Tracker").GetComponent<ProductivityTracker>();
        donutAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Worker")) {
            donutAudio.PlayOneShot(hitSound, 0.5f);
            Instantiate(workerParticle, transform.position, workerParticle.transform.rotation);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 4f);
            Destroy(other.gameObject);
            Destroy(other.transform.parent.gameObject);
            productivityTracker.UpdateScore(pointsMed);
        } else if (other.gameObject.CompareTag("Computer")) {
            donutAudio.PlayOneShot(hitSound, 0.5f);
            Instantiate(computerParticle, transform.position, computerParticle.transform.rotation);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 4f);
            Destroy(other.gameObject);
            productivityTracker.UpdateScore(pointsLow);
        } else if (other.gameObject.CompareTag("Obstacle")) {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("ScrumMaster")) {
            productivityTracker.scrumMasterHits++;
            if (productivityTracker.scrumMasterHits == 3) {
                Instantiate(scrumMasterKillParticle, transform.position, scrumMasterKillParticle.transform.rotation);
                donutAudio.PlayOneShot(scrumMasterKillSound, 1.0f);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject, 4f);
                Destroy(other.gameObject);
                Destroy(other.transform.parent.gameObject);
                productivityTracker.UpdateScore(pointsHigh);
            } else {
                Instantiate(scrumMasterHitParticle, transform.position, scrumMasterHitParticle.transform.rotation);
                donutAudio.PlayOneShot(hitSound, 0.6f);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject, 4f);
            }
        }
    }
}
