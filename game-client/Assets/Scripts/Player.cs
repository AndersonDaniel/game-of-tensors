using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int MAX_HEALTH = 5;
    public GameObject m_basicProjectileAbility;
    public GameObject m_superProjectileAbility;
    public GameObject m_shieldAbility;
    public int m_currentHealth = MAX_HEALTH;
    public Queue<string> AbilityQueue = new Queue<string>();
    public Animator abilityAnimator;
    public int m_direction = 1;
    public int m_playerNumber;
    public AudioSource m_dragonHitSmall;
    public AudioSource m_dragonHitSuper;
    public GameObject m_lifebar;
    public EnergyManager m_energyManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFlying());
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            StartCoroutine(this.PerformAbility("throw"));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(this.PerformAbility("shield"));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(this.PerformAbility("flick"));
        }

        if (this.AbilityQueue.Count > 0)
        {
            StartCoroutine(this.PerformAbility(this.AbilityQueue.Dequeue()));
        }
    }

    private IEnumerator StartFlying()
    {
        yield return new WaitForSeconds(UnityEngine.Random.value * 3);
        GetComponent<Animator>().SetInteger("state", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        Projectile proj = other.gameObject.GetComponent<Projectile>();
        if ((proj) && (proj.owner != this.m_playerNumber))
        {
            StartCoroutine(PlayerHit(proj));
        }
    }

    public IEnumerator PlayerHit(Projectile proj)
    {
        // TODO: Actual collision damage
        //m_health -= 50;

        StartCoroutine(proj.Explode());
        if (proj.m_ProjectileName == "Fireball") {
            this.m_dragonHitSmall.time = 0.5f;
            this.m_dragonHitSmall.Play();
            TakeDamage(1);
        }
        else if (proj.m_ProjectileName == "Fireblast")
        {
            this.m_dragonHitSuper.time = 0.5f;
            this.m_dragonHitSuper.Play();
            TakeDamage(2);
        }


        if (this.m_currentHealth == 0)
        {
            abilityAnimator.SetInteger("state", -999);
            this.GetComponent<Animator>().SetInteger("state", -999);
        }
        else
        {
            abilityAnimator.SetInteger("state", -1);
        }
        this.GetComponentInChildren<DragonAnimationController>().wings.SetActive(false);

        yield return null;
    }

    public IEnumerator PerformAbility(string ability)
    {
        if (!m_energyManager.TryUseAbility(ability))
        {
            Debug.Log("Not enough energy!");
        }
        else
        {
            Debug.Log("performing ability");
            if (ability == "throw")
            {
                abilityAnimator.SetInteger("state", 1);
                yield return new WaitForSeconds(0.25f);
                GameObject proj = Instantiate<GameObject>(m_basicProjectileAbility,
                     new Vector3(this.transform.position.x + 1.7f * m_direction, -3.3f, 0), Quaternion.identity);
                proj.GetComponent<Projectile>().m_direction = this.m_direction;
                proj.GetComponent<Projectile>().owner = this.m_playerNumber;
            }
            else if (ability == "flick")
            {
                abilityAnimator.SetInteger("state", 1);
                yield return new WaitForSeconds(0.25f);
                GameObject proj = Instantiate<GameObject>(m_superProjectileAbility,
                     new Vector3(this.transform.position.x + 1.7f * m_direction, -2.7f, 0), Quaternion.identity);
                proj.GetComponent<Projectile>().m_direction = this.m_direction;
                proj.GetComponent<Projectile>().owner = this.m_playerNumber;
            }
            else if (ability == "shield")
            {
                m_shieldAbility.SetActive(true);
                StartCoroutine(m_shieldAbility.GetComponent<Shield>().ActivateShield());
                yield return new WaitForSeconds(3f);
                StartCoroutine(m_shieldAbility.GetComponent<Shield>().DeactivateShield());
                yield return new WaitForSeconds(1f);
                m_shieldAbility.SetActive(false);
            }
            yield return null;
        }
    }

    public void TakeDamage(int dmg)
    {
        int new_health = m_currentHealth - dmg;

        if (new_health <= 0)
        {
            new_health = 0;
        }

        for (int healthIdx = m_currentHealth; healthIdx > new_health; healthIdx-=1)
        {
            m_lifebar.transform.Find(String.Format("heart{0}", healthIdx)).GetComponent<Heart>().LoseHeart();
        }

        this.m_currentHealth = new_health;
    }
}
