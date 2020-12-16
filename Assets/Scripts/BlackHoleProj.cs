using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProj : Projectile
{

    private float expandRate;
    private float decelerationRate;
    private bool isActive;

    private float pullSpeed;
    private float pullRotSpeed;

    protected override void Awake()
    {
        lifespan = 5f;
        Destroy(gameObject, lifespan);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        isActive = false;
        expandRate = 2f;
        decelerationRate = 5f;
        pullSpeed = 2f;
        pullRotSpeed = 80f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (size < 5f)
        {
            size += size * expandRate * Time.deltaTime;
            transform.localScale = new Vector3(size, size, size);
        }

        if (speed > 0)
        {
            transform.Translate(fwdDirection * speed * Time.deltaTime);
            speed -= decelerationRate * Time.deltaTime;
        }
        else
        {
            RadialPull();
        }
    }

    IEnumerator KillPlayer(Collider hitPlayer)
    {
        yield return new WaitForSeconds(3f);
        hitPlayer.SendMessage("ApplyDamage", baseDamage);
        Destroy(gameObject);
    }

    private void RadialPull()
     {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f, 1 << 9);
        foreach (var hitCollider in hitColliders)
        {
            if(!isActive)
            {
                StartCoroutine(KillPlayer(hitCollider));
            }

            hitCollider.transform.position = Vector3.Lerp(hitCollider.transform.position, transform.position, pullSpeed* Time.deltaTime);
            hitCollider.transform.Rotate(Vector3.one * pullRotSpeed * Time.deltaTime);
        }
        isActive = true;
     }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 5f);
    }
}
