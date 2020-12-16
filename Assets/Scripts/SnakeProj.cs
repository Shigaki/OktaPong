using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeProj : Projectile
{
    public float turnSpeed;

    protected override void Awake()
    {
        lifespan = 1.5f;
        Destroy(gameObject, lifespan);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxDeflections = 10;
        InvokeRepeating("ChangeDirection", 0.1f, 0.2f);
    }

    void ChangeDirection()
    {
        turnSpeed = -turnSpeed;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.Rotate(Vector3.right * turnSpeed * Time.deltaTime);
    }
}
