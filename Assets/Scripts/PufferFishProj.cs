using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFishProj : Projectile
{

    private float newSize;
    private float baseSpeed;

    protected override void Awake()
    {
        lifespan = 5f;
        Destroy(gameObject, lifespan);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        baseSpeed = speed;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        newSize = size * (hitCount + 1);
        transform.localScale = new Vector3(newSize, newSize, newSize);
        speed = (baseSpeed * (hitCount + 1));
    }
}
