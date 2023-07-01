using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float MaxSpeed;
    public float chrgSpeed;
    private float RageSpeed;
    float Speed;
    public float rageSpeed;

    private Collider[] hitColliders;
    private RaycastHit Hit;

    public float SightRange;
    public float DetectiontRange;

    public Rigidbody rb;
    public GameObject Target;

    public float hp;
    float RageHP;
    float numberLol = 2;

    public Transform ProjectileSpawnPoint;
    public GameObject projectilePrefab;
    public float ProjectileSpeed = 7;
    public float firerate;
    public float nextfire;

    public float chrgAtk;
    float nextchrg;

    private bool seePlayer;

    public AudioSource dieAudio;

    public GameObject Spawn;
    public GameObject Disable;
    public HealthBar healthBar;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (hp > 0)
            {
                hp -= 1;
                dieAudio.Play();
                healthBar.SetHealth(hp);
            }
            if (hp <= 0)
            {
                dieAudio.Play();
                Spawn.SetActive(true);
                Disable.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = MaxSpeed;
        RageSpeed = Speed * rageSpeed;
        RageHP = hp * 0.3f;
        nextchrg = chrgAtk;
        healthBar.SetMaxHealth(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (!seePlayer)
        {
            hitColliders = Physics.OverlapSphere(transform.position, DetectiontRange);
            foreach (var HitCollider in hitColliders)
            {
                if (HitCollider.tag == "Player")
                {
                    Target = HitCollider.gameObject;
                    seePlayer = true;
                }
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, (Target.transform.position - transform.position), out Hit, SightRange))
            {
                if (Hit.collider.tag != "Player")
                {
                    seePlayer = false;
                }
                else if (Hit.collider.tag == "Player" && (hp > RageHP))
                {
                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    if (Time.time > nextfire)
                    {
                        //shotAudio.Play();
                        nextfire = Time.time + firerate;
                        var bullet = Instantiate(projectilePrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
                        bullet.GetComponent<Rigidbody>().velocity = ProjectileSpawnPoint.forward * ProjectileSpeed;
                    }

                    if (Time.time > nextchrg)
                    {
                        
                        if (numberLol > 0)
                        {
                            numberLol -= 1 * Time.deltaTime;
                            Vector3 Move = new Vector3(Direction.x * chrgSpeed, 0, Direction.z * chrgSpeed);
                            rb.velocity = Move;
                            transform.forward = Move;
                        }
                        else
                        {
                            numberLol = 2;
                            nextchrg = Time.time + chrgAtk + numberLol;
                        }
                    }
                    else if (Time.time <= nextchrg)
                    {
                        Vector3 Move = new Vector3(Direction.x * Speed, 0, Direction.z * Speed);
                        rb.velocity = Move;
                        transform.forward = Move;
                    }

                    
                }

                else if (Hit.collider.tag == "Player" && (hp <= RageHP))
                {
                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    if (Time.time > nextfire)
                    {
                        nextfire = Time.time + firerate;
                        var bullet = Instantiate(projectilePrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
                        bullet.GetComponent<Rigidbody>().velocity = ProjectileSpawnPoint.forward * ProjectileSpeed;
                    }

                    Vector3 Move = new Vector3(Direction.x * RageSpeed, 0, Direction.z * RageSpeed);
                    rb.velocity = Move;
                    transform.forward = Move;
                }
            }
        }
    }
}