using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Vector3 movementInput = Vector3.zero;
    public float movementSpeed = 0.0f;
    public float sprintSpeed = 0.0f;
    Vector3 rotationInput = Vector3.zero;
    public float rotationSpeed = 0.0f;

    //public Start script;
    //public Camera mainCamera;
    public GameObject UI;
    public GameObject Menu;

    public Transform camera;
    public Rigidbody rb;

    public float staminaRate = 0.1f;
    public float recoverRate = 0.02f;
    public bool sprint = false;
    public Image staminaBar;

    public bool PlayerGrounded = true;
    public bool isLadder = false;
    public bool isDie = false;
    public bool EPort = false;
    public static bool gunActive = true;
    public static bool rifleActive = true;
    public bool gunOn= false;
    public bool rifleOn = false;
    public static bool rifleWork = false;
    public int sceneNum = 0;
    public GameObject gun;
    public GameObject rifle;
    public GameObject bench;
    public GameObject ammo;
    public GameObject ammoR;
    public bool Key = false;
    public bool isLock = false;
    public GameObject lockDoor;
    public static float hP = 100;
    public TextMeshProUGUI HP;
    public Explode explode;
    public Explode expscript;
    public GameObject Warning;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float firerate;
    public float firerateR;
    public float nextfire;
    public int mag = 12;
    int capacity;
    public int magR = 30;
    int capacityR;
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI ammoRCount;
    public ParticleSystem MuzzleFlash;
    public AudioSource shotAudio;
    public ParticleSystem MuzzleFlashR;
    public AudioSource shotAudioR;

    public GameObject playerPrefab;
    private PlayerControl activePlayer;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Menu")
        {
            isDie = true;
            Menu.SetActive(true);
            UI.SetActive(false);
        }

        if (collision.gameObject.tag == "Solid")
        {
            PlayerGrounded = true;
        }

        if (collision.gameObject.tag == "Damage")
        {
            isDie = true;
            GetComponent<Animator>().SetTrigger("Die");
        }

        if (collision.gameObject.tag == "Heal")
        {
            hP = 100;
            Destroy(collision.gameObject);
            HP.text = hP + "%";
        }

        if (collision.gameObject.tag == "Upgrade")
        {
            rifleWork = true;
            ammoRCount.text = capacityR + "/30";
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            if (hP > 0) 
            {
                hP -= 10;
                HP.text = hP + "%";
            }
            else
            {
                isDie = true;
                GetComponent<Animator>().SetTrigger("Die");
            }
        }

        if (collision.gameObject.tag == "Projectile")
        {
            if (hP > 0) 
            {
                hP -= 20;
                HP.text = hP + "%";
            }
            else
            {
                isDie = true;
                GetComponent<Animator>().SetTrigger("Die");
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            isLadder = true;
        }

        if (col.gameObject.tag == "Lock")
        {
            isLock = true;
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
        if (col.gameObject.tag == "Teleport2" && rifleWork)
        {
            sceneNum = 3;
            EPort = true;
        }
        if (col.gameObject.tag == "Teleport0")
        {
            sceneNum = 0;
            EPort = true;
        }

        if (col.gameObject.tag == "Brute")
        {
            if (hP > 0)
            {
                hP -= 30;
                HP.text = hP + "%";
            }
            else if (hP <= 0)
            {
                isDie = true;
                GetComponent<Animator>().SetTrigger("Die");
            }
        }

        if (col.gameObject.tag == "Warning")
        {
            Warning.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            isLadder = false;
        }

        if (col.gameObject.tag == "Lock")
        {
            isLock = false;
        }

        if (col.gameObject.tag == "Teleport")
        {
            EPort = false;
        }
        if (col.gameObject.tag == "Teleport1")
        {
            EPort = false;
        }
        if (col.gameObject.tag == "Teleport0")
        {
            EPort = false;
        }

        if (col.gameObject.tag == "Warning")
        {
            Warning.SetActive(false);
        }
    }

    void craftRifle()
    {
        rifleWork = true;
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
                    MuzzleFlash.Play();
                    shotAudio.Play();
                    nextfire = Time.time + firerate;
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                    capacity -= 1;
                    ammoCount.text = capacity + "/12";
                }
            }
        }
        if ((rifleActive == true && rifleOn == true) && rifleWork == true)
        {
            if (capacityR > 0)
            {
                if (Time.time > nextfire)
                {
                    MuzzleFlashR.Play();
                    shotAudioR.Play();
                    nextfire = Time.time + firerateR;
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                    capacityR -= 1;
                    ammoRCount.text = capacityR + "/30";
                }
            }
        }
    }

    void OnSprint()
    {
        sprint = true;
    }
    void OnSprintDone()
    {
        sprint = false;
    }

    void OnInteract(InputValue value)
    {
        if (EPort == true && sceneNum == 0)
        {
            SceneManager.LoadScene(0);
        }
        if ((EPort == true && sceneNum == 1) && gunActive)
        {
            SceneManager.LoadScene(1);
        }
        if (EPort == true && sceneNum == 2)
        {
            SceneManager.LoadScene(2);
        }
        if (EPort == true && sceneNum == 3)
        {
            SceneManager.LoadScene(3);
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
            rifleOn = false;
            rifle.SetActive(false);
            ammoR.SetActive(false);
        }
    }

    void OnEquip1(InputValue value)
    {
        if (rifleActive == true && rifleOn == true)
        {
            rifleOn = false;
            rifle.SetActive(false);
            ammoR.SetActive(false);
        }
        else if (rifleActive == true && rifleOn == false)
        {
            rifleOn = true;
            rifle.SetActive(true);
            ammoR.SetActive(true);
            gunOn = false;
            gun.SetActive(false);
            ammo.SetActive(false);
        }
    }

    void OnReload(InputValue value)
    {
        if (gunActive == true && gunOn == true)
        {
            capacity = mag;
            ammoCount.text = capacity + "/12";
        }
        if ((rifleActive == true && rifleOn == true) && rifleWork == true)
        {
            capacityR = magR;
            ammoRCount.text = capacityR + "/30";
        }
    }

    //////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        capacity = mag;
        capacityR = magR;
        HP.text = hP + "%";

        staminaBar.fillAmount = 1;
        //Debug.Log(script.start);
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (script.start == true)
        {
            SceneManager.LoadScene(0);
            mainCamera.enabled = true;
            Menu.SetActive(false);
            UI.SetActive(true);
        }
        else
        {
            mainCamera.enabled = false;
        }
        */
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
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Start")
            {
                Debug.Log("Done");
            }

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Gun")
            {
                //what happens after clicking
                gunActive = true;
                gunOn = true;
                gun.SetActive(true);
                ammo.SetActive(true);
                rifleOn = false;
                rifle.SetActive(false);
                ammoR.SetActive(false);
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Rifle")
            {
                rifleActive = true;
                rifleOn = true;
                rifle.SetActive(true);
                ammoR.SetActive(true);
                gunOn = false;
                gun.SetActive(false);
                ammo.SetActive(false);
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

        if (sprint && staminaBar.fillAmount > 0)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position
            + (forwarDir + rightDir + up) * sprintSpeed);

            //Debug.Log(staminaBar.fillAmount > 0);
            staminaBar.fillAmount -= staminaRate * Time.deltaTime;
        }
        else if (sprint && staminaBar.fillAmount <= 0)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position
                + (forwarDir + rightDir + up) * movementSpeed);
            
        }
        else if (!sprint && staminaBar.fillAmount >= 0)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position
                + (forwarDir + rightDir + up) * movementSpeed);
            staminaBar.fillAmount += recoverRate * Time.deltaTime;
        }

        //transform.position += (forwarDir + rightDir) * movementSpeed;

        var headRot = camera.rotation.eulerAngles
            + new Vector3(rotationInput.x, 0, 0) * rotationSpeed;

        camera.rotation = Quaternion.Euler(headRot);

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles
            + new Vector3(0, rotationInput.y, 0) * rotationSpeed);
    }
}
