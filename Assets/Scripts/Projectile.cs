using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Projectile : MonoBehaviour
{
    public string description = "Just a simple ball";
    public string pName = "SoftBall";
    public float baseDamage;
    public float speed;
    public float size;
    public int maxDeflections = 5;

    public float lifespan;

    protected Vector3 fwdDirection;

    private int maxReflectionCount = 3;
    private float maxRayDistance = 100f;

    protected int hitCount;

    protected virtual void Awake()
    {
        lifespan = 3.0f;
        Destroy(gameObject, lifespan);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        fwdDirection = new Vector3(0, 0, 1);
        transform.localScale = new Vector3(size, size, size);

        hitCount = 0;
    }

    protected virtual void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (hitCount > maxDeflections)
        {
            Destroy(gameObject);
        }

        transform.Translate(fwdDirection * speed * Time.deltaTime);
        hitCount += CollisionDetection(transform.position, transform.forward);
    }

    public float CalculateDamage(float baseDamage, float speed, float size)
    {
        return (((baseDamage * speed / 30) * size) * Random.Range(0.75f, 1.25f));
    }

    protected virtual int CollisionDetection(Vector2 position, Vector2 direction)
    {
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        var range = (size > 1.5f) ? size / 2 : 1;
        if (Physics.Raycast(position, direction, out hit, range))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                direction = Vector3.Reflect(direction, hit.normal);
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
                return 1;
            }
            else if (hit.collider.CompareTag("Player1") || hit.collider.CompareTag("Player2"))
            {
                Destroy(gameObject);
                var damage = CalculateDamage(baseDamage, speed, size);
                hit.collider.SendMessage("ApplyDamage", damage);
                //Debug.Log("Hit");
                return 1;
            }
        }
        return 0;
    }
    void OnDrawGizmos()
    {
        DrawTrajectoryPrediction(transform.position + transform.forward * 0.5f, transform.forward, maxReflectionCount);
    }

    private void DrawTrajectoryPrediction(Vector2 position, Vector2 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            return;
        }

        Vector2 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position = direction * maxRayDistance;
        }


        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

        DrawTrajectoryPrediction(position, direction, reflectionsRemaining - 1);
    }
    
}