using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileShooter;
    public ProductivityTracker productivityTracker;
    private Vector3 currentRotation;
    private float rotationSpeed = 50;
    private int rotationLimit = 180;
    private float horizonalInput;
    private Rigidbody playerRb; 
    private Animator playerAnim;
    public bool gameOver = false;
    private bool inRotationRange = false;

    // Start is called before the first frame update
    void Start()
    {
        productivityTracker = GameObject.Find("Productivity Tracker").GetComponent<ProductivityTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (productivityTracker.isGameActive) {
            controlPlayer();
        }
    }

    void controlPlayer() {
        currentRotation = transform.localRotation.eulerAngles;
        if ((currentRotation.y >= 0 && currentRotation.y <= 90) || (currentRotation.y >= 270 && currentRotation.y <= 360)) {
            inRotationRange = true;
        } else {
            inRotationRange = false;
        }
        horizonalInput = Input.GetAxis("Horizontal");
        if (inRotationRange) {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizonalInput); 
            currentRotation.y = Mathf.Clamp(currentRotation.y, 0, 180);
        } else if (Input.GetKey(KeyCode.RightArrow) && currentRotation.y > rotationLimit) {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizonalInput); 
        } else if (Input.GetKey(KeyCode.LeftArrow) && currentRotation.y < rotationLimit) {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizonalInput); 
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Launch Player Projectile
            Instantiate(projectilePrefab, projectileShooter.position, projectileShooter.rotation);
        }
    }
}
