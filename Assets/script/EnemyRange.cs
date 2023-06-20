using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public float MaxSpeed;
    float Speed;

    private Collider[] hitColliders;
    private RaycastHit Hit;

    public float SightRange;
    public float DetectiontRange;

    public Rigidbody rb;
    public GameObject Target;

    public float hp;
    public Transform ProjectileSpawnPoint;
    public GameObject projectilePrefab;
    public float ProjectileSpeed = 7;
    public float firerate;
    public float nextfire;

    private bool seePlayer;

    public AudioSource dieAudio;
    public AudioSource shotAudio;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (hp > 0)
            {
                hp -= 1;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = MaxSpeed;
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
                else
                {
                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    if (Time.time > nextfire)
                    {
                        shotAudio.Play();
                        nextfire = Time.time + firerate;
                        var bullet = Instantiate(projectilePrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
                        bullet.GetComponent<Rigidbody>().velocity = ProjectileSpawnPoint.forward * ProjectileSpeed;
                    }

                    Vector3 Move = new Vector3(Direction.x * Speed, 0, Direction.z * Speed);
                    rb.velocity = Move;
                    transform.forward = Move;
                }
            }
        }
    }
}


