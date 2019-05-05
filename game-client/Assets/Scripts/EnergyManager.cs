using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private Dictionary<string, int> m_abilityCosts = new Dictionary<string, int>();
    private const float MAX_ENERGY = 100f;
    private const float BAR_SCALE_FULL = 1f;
    private const float BAR_SCALE_EMPTY = 3f;
    public SpriteRenderer energy_bar;
    public float m_currentEnergy = 100f;
    public float m_regenRate = 0.001f;
    public AudioSource m_noEnergySound;

    // Start is called before the first frame update
    void Start()
    {
        m_abilityCosts["flick"] = 80;
        m_abilityCosts["throw"] = 20;
        m_abilityCosts["shield"] = 40;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.m_currentEnergy = Mathf.Min(100f, this.m_currentEnergy + m_regenRate);
        energy_bar.transform.localScale = new Vector3(BAR_SCALE_EMPTY - ((float)(this.m_currentEnergy)) / MAX_ENERGY * (BAR_SCALE_EMPTY - BAR_SCALE_FULL),
                                                      1, 1);
    }

    public bool TryUseAbility(string ability_code)
    {
        if (this.m_currentEnergy >= this.m_abilityCosts[ability_code])
        {
            this.m_currentEnergy -= this.m_abilityCosts[ability_code];
            return true;
        }

        m_noEnergySound.Play();
        return false;
    }
}
