using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_horizontalSpeed = 0.1f;
    public int m_direction = 1;
    public float angle = 0;
    public int owner;
    public string m_ProjectileName;
    public SpriteRenderer m_body;
    public Animator m_bodyAnimator;
    public ParticleSystem particles;
    public AudioSource m_launchSound;



    // Start is called before the first frame update
    void Start()
    {
        this.m_launchSound.time = 0.2f;
        this.m_launchSound.Play();
        this.transform.localScale = new Vector3(this.transform.localScale.x * m_direction,
                                                  this.transform.localScale.y,
                                                  this.transform.localScale.z);
        this.particles = this.GetComponentInChildren<ParticleSystem>();
        particles.gameObject.transform.localScale = new Vector3(particles.gameObject.transform.localScale.x * m_direction,
                                                    particles.gameObject.transform.localScale.y,
                                                    particles.gameObject.transform.localScale.z);
    }

    void FixedUpdate()
    {
        this.transform.position += new Vector3(m_horizontalSpeed * m_direction, 0, 0);
    }

    public IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = m_body.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            m_body.color = newColor;
            yield return null;
        }
    }

    public IEnumerator Explode()
    {
        // No more collisions should happen
        this.GetComponent<Collider>().enabled = false;

        // Activates the dispersment particles
        m_bodyAnimator.SetInteger("state", 1);
        this.GetComponentInChildren<ParticleSystem>().gameObject.transform.position = this.transform.position;
        this.GetComponentInChildren<ParticleSystem>().Play();
        //this.m_horizontalSpeed = 0;


        // Fades the body
        if (this.m_ProjectileName == "Fireblast") {
            StartCoroutine(FadeTo(0f, 2f));
        }
        else if (this.m_ProjectileName == "Fireball")
        {
            StartCoroutine(FadeTo(0f, 0.1f));
            yield return new WaitForSeconds(0.1f);
            m_body.enabled = false;

            // Kills the game object
            yield return new WaitForSeconds(1f);
            this.gameObject.SetActive(false);
        }



        yield return null;
    }


}
