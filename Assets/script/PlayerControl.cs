using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Vector3 movementInput = Vector3.zero;
    public float movementSpeed = 0.0f;
    Vector3 rotationInput = Vector3.zero;
    public float rotationSpeed = 0.0f;

    public Transform camera;
    public Rigidbody rb;
    public bool PlayerGrounded = true;
    public bool isLadder = false;
    public bool isDie = false;
    public bool EPort = false;
    public static bool gunActive = false;
    public bool gunOn= false;
    public int sceneNum = 0;
    public GameObject gun_item;
    public GameObject gun;
    public GameObject bench;
    public GameObject ammo;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float firerate;
    public float nextfire;
    public int mag = 12;
    int capacity;
    public TextMeshProUGUI ammoCount;

    public GameObject playerPrefab;
    private PlayerControl activePlayer;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            PlayerGrounded = true;
        }

        if (collision.gameObject.tag == "Damage")
        {
            isDie = true;
            GetComponent<Animator>().SetTrigger("Die");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            isLadder = true;
        }

        if (col.gameObject.tag == "Teleport")
        {
            sceneNum = 1;
            EPort = true;
        }
        if (col.gameObject.tag == "Teleport1")
        {
            sceneNum = 2;
            EPort = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            isLadder = false;
        }

        if (col.gameObject.tag == "Teleport")
        {
            EPort = false;
        }
        if (col.gameObject.tag == "Teleport1")
        {
            EPort = false;
        }
    }

    //Player Controls
    //////////////////////////////////////////////////////////

    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x;
        rotationInput.x = -value.Get<Vector2>().y;
    }
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (PlayerGrounded == true)
        {
            rb.AddForce(new Vector3(0, 25, 0), ForceMode.Impulse);
            PlayerGrounded = false;
        }
    }

    void OnFire()
    {
        if (gunActive == true && gunOn == true)
        {
            if (capacity > 0)
            {
                if (Time.time > nextfire)
                {
                    nextfire = Time.time + firerate;
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                    capacity -= 1;
                    ammoCount.text = capacity + "/12";
                }
            }
        }
    }

    void OnInteract(InputValue value)
    {
        if (EPort == true && sceneNum == 1)
        {
            SceneManager.LoadScene(1);
        }
        if (EPort == true && sceneNum == 2)
        {
            SceneManager.LoadScene(2);
        }
    }

    void OnEquip(InputValue value)
    {
        if (gunActive == true && gunOn == true)
        {
            gunOn = false;
            gun.SetActive(false);
            ammo.SetActive(false);
        }
        else if (gunActive == true && gunOn == false)
        {
            gunOn = true;
            gun.SetActive(true);
            ammo.SetActive(true);
        }
    }

    void OnReload(InputValue value)
    {
        if (gunActive == true && gunOn == true)
        {
            capacity = mag;
            ammoCount.text = capacity + "/12";
        }
    }

    //////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        capacity = mag;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie == true) 
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if user mouse is at the button
        if (Input.GetMouseButtonDown(0))
        {
            //if user clicks on the button
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Gun")
            {
                //what happens after clicking
                gunActive = true;
                gunOn = true;
                gun.SetActive(true);
                ammo.SetActive(true);
            }

            else if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Bench")
            {
                bench.SetActive(true);
            }
        }

        Vector3 up = Vector3.zero;

        if (Input.GetKey("w") && isLadder == true)
        {
            up.y = 2 * movementSpeed;
        }

        Vector3 forwarDir = transform.forward;
        forwarDir *= movementInput.y;

        Vector3 rightDir = transform.right;
        rightDir *= movementInput.x;

        GetComponent<Rigidbody>().MovePosition(transform.position
            + (forwarDir + rightDir + up) * movementSpeed);

        //transform.position += (forwarDir + rightDir) * movementSpeed;

        var headRot = camera.rotation.eulerAngles
            + new Vector3(rotationInput.x, 0, 0) * rotationSpeed;

        camera.rotation = Quaternion.Euler(headRot);

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles
            + new Vector3(0, rotationInput.y, 0) * rotationSpeed);
    }
}
