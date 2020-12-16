using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniScatterProj : Projectile
{
    protected override void Awake()
    {
        lifespan = 1f;
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
