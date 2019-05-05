using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int owner;
    public ParticleSystem m_fireballHitParticles;
    public ParticleSystem m_fireblastHitParticles;
    public AudioSource m_shieldHitSound;
    public AudioSource m_shieldUpSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ActivateShield()
    {
        m_shieldUpSound.Play();
        StartCoroutine(FadeTo(1f, 1f));
        yield return null;
    }

    public IEnumerator DeactivateShield()
    {
        StartCoroutine(FadeTo(0f, 1f));
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Shield Collision!");
        Projectile proj = other.gameObject.GetComponent<Projectile>();
        if ((proj) && (proj.owner != this.owner))
        {
            m_shieldHitSound.Play();
            proj.gameObject.SetActive(false);
            if (proj.m_ProjectileName == "Fireball")
            {
                m_fireballHitParticles.Play();
            }
            else if (proj.m_ProjectileName == "Fireblast")
            {
                m_fireblastHitParticles.Play();
            }
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }
}

