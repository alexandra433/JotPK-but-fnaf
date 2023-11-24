using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Eventually, collectibles will disappear after a certain amount of time
    // 10 seconds
    [SerializeField] protected bool pickedUp;
    protected float timeLeft = 10f;
    [SerializeField] protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!pickedUp)
        {
            if (timeLeft <= 0f)
            {
                Destroy(this.gameObject);
            } else
            {
                if (timeLeft <= 3f)
                {
                    animator.SetBool("disappear", true);
                }
                timeLeft -= Time.deltaTime;
            }
        }
    }
}
