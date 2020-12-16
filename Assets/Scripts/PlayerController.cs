using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShootParams
{
    public int pNum;
    public GameObject prefab;
    public Vector3 spawnPos;
    public Quaternion spawnRot;
}

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;

    private Vector3 rotator;
    public Transform firePoint;
    private int pNum;

    private int projIndex;

    void Awake()
    {
        //mHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<MatchHandler>();
        firePoint = transform.Find("FirePoint");

        if (CompareTag("Player1"))
        {
            pNum = 1;
            foreach (Transform child in transform.GetChild(0))
            {
                child.GetComponent<MeshRenderer>().material.color = Color.red; 
            }
            
        } else if (CompareTag("Player2"))
        {
            pNum = 2;
            foreach (Transform child in transform.GetChild(0))
            {
                child.GetComponent<MeshRenderer>().material.color = Color.blue; 
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 7.0f;
        rotateSpeed = 2.5f;

        projIndex = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (pNum == 1) {
            if (MatchHandler.state == MatchState.FP_TURN)
            {
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 5f)
                {
                    transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -10f)
                {
                    transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rotator.z = Mathf.Clamp(rotator.z + rotateSpeed, -90f, 90f);
                    transform.rotation = Quaternion.Euler(rotator);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rotator.z = Mathf.Clamp(rotator.z - rotateSpeed, -90f, 90f);
                    transform.rotation = Quaternion.Euler(rotator);
                }
            }
        } else if (pNum == 2) {
            if (MatchHandler.state == MatchState.SP_TURN)
            {
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 5f)
                {
                    transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -10f)
                {
                    transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rotator.z = Mathf.Clamp(rotator.z + rotateSpeed, -90f, 90f);
                    transform.rotation = Quaternion.Euler(rotator) * Quaternion.Euler(0, 0, 180f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rotator.z = Mathf.Clamp(rotator.z - rotateSpeed, -90f, 90f);
                    transform.rotation = Quaternion.Euler(rotator) * Quaternion.Euler(0, 0, 180f);
                }
            }
        }
        // Debug.Log(transform.rotation.eulerAngles.z);

    }

    void Update()
    {

        if (pNum == 1 && MatchHandler.state == MatchState.FP_TURN) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            } else if (Input.GetKeyDown(KeyCode.Q))
            {
                projIndex += 1;
                if (projIndex > ProjectileHandler._pHandler.GetProjectileCount())
                {
                    projIndex = 0;
                }
                ProjectileHandler._pHandler.ChangeSelectedProjectile(pNum, projIndex);
            }
        } else if (pNum == 2 && MatchHandler.state == MatchState.SP_TURN) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            } else if (Input.GetKeyDown(KeyCode.Q))
            {
                projIndex += 1;
                if (projIndex > ProjectileHandler._pHandler.GetProjectileCount())
                {
                    projIndex = 0;
                }
                ProjectileHandler._pHandler.ChangeSelectedProjectile(pNum, projIndex);
            }
        }

    }

    public void ShootProjectile()
    {
        ShootParams sp;
        sp.pNum = pNum;
        sp.prefab = (projIndex != ProjectileHandler._pHandler.GetProjectileCount()) ?  
                     ProjectileHandler._pHandler.GetSelectedProjectile(projIndex) : 
                     ProjectileHandler._pHandler.GetSelectedProjectile(Random.Range(0, projIndex));
        sp.spawnPos = firePoint.transform.position;
        Vector3 relativePos = firePoint.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.right);
        sp.spawnRot = rotation;
        MatchHandler._mHandler.SpawnProjectile(sp);
    }

}