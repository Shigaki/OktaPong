using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScatterProj : Projectile
{

    public GameObject miniScatterPrefab;
    private const int SCATTER_COUNT = 12;
    private const float SCATTER_DISPERSION_ANGLE = 30f;
    protected override void Awake()
    {
        lifespan = 2f;
        Invoke("Scatter", lifespan);
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

   /* protected override int CollisionDetection(Vector2 position, Vector2 direction)
    
        return 0;
    }*/

    private void Scatter()
    {
        var currentAngle = Vector3.SignedAngle(Vector3.right, transform.forward, Vector3.forward);

        for(int i = 0; i < SCATTER_COUNT; i++)
        {
            var newAngle = currentAngle + Random.Range(-SCATTER_DISPERSION_ANGLE, SCATTER_DISPERSION_ANGLE+1);
            var newRot = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
            Instantiate(miniScatterPrefab, transform.position, Quaternion.LookRotation(newRot));
        }
    }

}
