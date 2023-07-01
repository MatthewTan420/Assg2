using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */

public class PlayerControl : MonoBehaviour
{
    Vector3 movementInput = Vector3.zero;
    public float movementSpeed = 0.0f;
    public float sprintSpeed = 0.0f;
    Vector3 rotationInput = Vector3.zero;
    public float rotationSpeed = 0.0f;

    public GameObject UI;
    public GameObject Menu;

    public Transform camera;
    public Rigidbody rb;

    public float staminaRate = 0.1f;
    public float recoverRate = 0.02f;
    private bool sprint = false;
    public Image staminaBar;

    private bool PlayerGrounded = true;
    private bool isMoving = false;
    private bool Interact = false;
    private bool isLadder = false;
    public bool isDie = false;
    public bool isEmo = false;
    private bool EPort = false;
    public static bool gunActive = true;
    public static bool rifleActive = true;
    private static bool gunOn = false;
    private static bool rifleOn = false;
    public static bool getUpg = true;
    public static bool rifleWork = false;
    public int sceneNum = 0;
    public GameObject gun;
    public GameObject rifle;
    public GameObject bench;
    public GameObject ammo;
    public GameObject ammoR;
    public GameObject Win;
    public GameObject Defeat;
    public GameObject EmoDmg;
    public bool Key = false;
    public bool isLock = false;
    public static bool crystal = true;
    public static bool parts = true;
    public GameObject lockDoor;
    public static float hP = 100;
    public TextMeshProUGUI HP;
    public GameObject Warning;
    private bool warningMsg = false;
    private bool isGas = false;
    float gasTime = 0;
    float gasVal = 0;
    float timeGas = 0;
    float timerVal = 0;
    float timerWarn = 0;
    bool endGame = false;
    bool isEsc = true;

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

    public GameObject msgHint;
    public TextMeshProUGUI Hintmsg;
    public GameObject engine;
    public GameObject power;

    public GameObject playerPrefab;
    private PlayerControl activePlayer;

    ///<summary>
    /// Player Collisions with objects
    ///</summary>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Menu")
        {
            isDie = true;
            Menu.SetActive(true);
            UI.SetActive(false);

            gunActive = false;
            rifleActive = false;
            rifleWork = false;
            crystal = false;
            parts = false;
            hP = 100;
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
            getUpg = true;
            Destroy(collision.gameObject);
        }

        ///<summary>
        /// Player Takes Damage
        ///</summary>
        if (collision.gameObject.tag == "Enemy")
        {
            if (hP > 0) 
            {
                hP -= 10;
                HP.text = hP + "%";
            }
        }
        ///<summary>
        /// Player Takes Damage
        ///</summary>
        if (collision.gameObject.tag == "Projectile")
        {
            if (hP > 0) 
            {
                hP -= 20;
                HP.text = hP + "%";
            }
        }
    }

    ///<summary>
    /// Player Triggering the objects
    ///</summary>
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
        if (col.gameObject.tag == "Teleport3")
        {
            sceneNum = 4;
            EPort = true;
        }
        if (col.gameObject.tag == "Teleport4" && crystal)
        {
            sceneNum = 0;
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
        }

        if (col.gameObject.tag == "Warning")
        {
            warningMsg = true;
            timerWarn = 0;
        }

        if (col.gameObject.tag == "EmotionDmg")
        {
            isEmo = true;
            GetComponent<Animator>().SetTrigger("Die");
        }

        ///<summary>
        /// Player Takes Damage
        ///</summary>
        if (col.gameObject.tag == "Gas")
        {   
            if (hP > 0)
            {
                HP.text = hP + "%";
                isGas = true;
                gasTime = 0;
            }
        }
        if (col.gameObject.tag == "Explode")
        {
            if (hP > 0)
            {
                hP -= 50;
                HP.text = hP + "%";
            }
        }

        if (col.gameObject.tag == "hitshipHint")
        {
            msgHint.SetActive(true);
            Hintmsg.text = "Maybe i can shoot the ship to cross over";
        }
        if (col.gameObject.tag == "mainSign")
        {
            msgHint.SetActive(true);
            if (!parts)
            {
                Hintmsg.text = "I should find a Village";
            }
            else
            {
                Hintmsg.text = "Now I can go to the Cave";
            }
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
            warningMsg = false;
            Warning.SetActive(false);
        }

        if (col.gameObject.tag == "Gas")
        {
           isGas = false;
        }
    }

    void Timer(float sec)
    {
        timerVal += Time.deltaTime;
        if (timerVal > sec)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void warnStop(int sec)
    {
        Warning.SetActive(true);
        timerWarn += Time.deltaTime;
        if (timerWarn > sec)
        {
            Warning.SetActive(false);
        }
    }

    public void craftRifle()
    {
        if (getUpg == true && rifleActive == true)
        {
            rifleWork = true;
            ammoRCount.text = capacityR + "/30";
        }
    }

    public void restart()
    {
        hP = 100;
        HP.text = hP + "%";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    ///<summary>
    /// Player Controls
    /// </summary>

    //////////////////////////////////////////////////////////

    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x;
        rotationInput.x = -value.Get<Vector2>().y;
    }
    void OnMove(InputValue value)
    {
        isMoving = true;
        movementInput = value.Get<Vector2>();
    }
    void OnMoveStop(InputValue value)
    {
        isMoving = false;
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
        Interact = true;
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
        if ((EPort == true && sceneNum == 1) && getUpg)
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
        if (EPort == true && sceneNum == 4)
        {
            SceneManager.LoadScene(4);
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

    void OnEsc(InputValue value)
    {
        if (isEsc == true)
        {
            isEsc = false;
        }
        else if (isEsc == false)
        {
            isEsc = true;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isEsc == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (isEsc == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (isDie == true) 
        {
            isEsc = false;
            Defeat.SetActive(true);
            return;
        }

        if (isEmo == true)
        {
            isEsc = false;
            EmoDmg.SetActive(true);
            return;
        }

        if (endGame == true)
        {
            Timer(5);
        }

        if (gunOn)
        {
            gun.SetActive(true);
            ammo.SetActive(true);
        }

        if (rifleOn)
        {
            rifle.SetActive(true);
            ammoR.SetActive(true);
        }

        if (warningMsg)
        {
            warnStop(5);
        }

        if (hP <= 0)
        {
            isDie = true;
            GetComponent<Animator>().SetTrigger("Die");
        }

        if (isGas == true && 10 > gasTime)
        {
            gasTime += Time.deltaTime;
            timeGas += Time.deltaTime;
            if (timeGas > gasVal)
            {
                hP -= 1;
                HP.text = hP + "%";
                timeGas -= 1;

            }
        }

        ///<summary>
        /// Raycasting, when player looks at the object and interacted with it
        ///</summary>
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag == "Gun" && Interact)
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
            else if (hit.transform.tag == "Rifle" && Interact)
            {
                rifleActive = true;
                rifleOn = true;
                rifle.SetActive(true);
                ammoR.SetActive(true);
                gunOn = false;
                gun.SetActive(false);
                ammo.SetActive(false);
            }
            else if (hit.transform.tag == "Bench" && Interact)
            {
                bench.SetActive(true);
            }
            else if (hit.transform.tag == "NPC" && Interact)
            {
                hit.transform.GetComponent<NPC>().npcTalk();
            }
            else if (hit.transform.tag == "Button" && Interact)
            {
                hit.transform.GetComponent<Button>().buttonClick();
            }
            else if (hit.transform.tag == "Bomb" && Interact)
            {
                hit.transform.GetComponent<Bomb_PickUp>().bombUp();
            }
            else if (hit.transform.tag == "Crystal" && Interact)
            {
                crystal = true;
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.tag == "Engine" && Interact)
            {
                parts = true;
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.tag == "Win" && Interact)
            {
                if (crystal == true && parts == true)
                {
                    power.SetActive(false);
                    Win.SetActive(true);
                    endGame = true;
                }
                else if (crystal == false && parts == true)
                {
                    engine.SetActive(false);
                }
                else if (crystal == true && parts == false)
                {
                    power.SetActive(false);
                }
            }
            Interact = false;
        }

        Vector3 up = Vector3.zero;

        if (isMoving == true && isLadder == true)
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
