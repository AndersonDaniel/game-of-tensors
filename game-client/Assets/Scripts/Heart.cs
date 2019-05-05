using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite full_heart;
    public Sprite dead_heart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHeart()
    {
        this.GetComponent<SpriteRenderer>().sprite = dead_heart;
    }
}
