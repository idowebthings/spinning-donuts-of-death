using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody rb;
    public float forceSpeed = 10.0f;
    private bool hit;
    public Transform projectileShooter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(projectileShooter.transform.forward * forceSpeed);
        Destroy(gameObject, 4f);
    }
}
