using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationController : MonoBehaviour
{
    public GameObject wings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationEnded()
    {
        if (this.GetComponent<Animator>().GetInteger("state") != -9)
        {
            this.GetComponent<Animator>().SetInteger("state", 0);
            wings.SetActive(true);
        }

    }
}
