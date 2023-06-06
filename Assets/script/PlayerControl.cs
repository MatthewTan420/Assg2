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
    public float maxYrot = 20f;
    public float minYrot = -20f;

    public Transform camera;
    public Rigidbody rb;
    public bool PlayerGrounded = true;
    public bool isLadder = false;
    public bool isDie = false;
    public bool EPort = false;

    public GameObject playerPrefab;
    private PlayerControl activePlayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SpawnPlayerOnScreenLoad;
    }

    private void SpawnPlayerOnScreenLoad(Scene curScene, Scene next)
    {
        spawn spawnpoint = FindObjectOfType<spawn>();
        if (activePlayer == null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, spawnpoint.transform.position, Quaternion.identity);
            activePlayer = newPlayer.GetComponent<PlayerControl>();
        }
        else
        {
            activePlayer.transform.position = spawnpoint.transform.position;
        }
    }

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
    }

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
    void OnInteract(InputValue value)
    {
        if (EPort == true)
        {
            SceneManager.LoadScene(1);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie == true) 
        {
            return;
        }

        Vector3 up = Vector3.zero;

        if (Input.GetKey("w") && isLadder == true)
        {
            up.y = 1;
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
