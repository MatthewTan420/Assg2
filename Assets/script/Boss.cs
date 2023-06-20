using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float MaxSpeed;
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

    private bool seePlayer;

    public AudioSource dieAudio;
    public GameObject Spawn;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (hp > 0)
            {
                hp -= 1;
                dieAudio.Play();
            }
            else
            {
                dieAudio.Play();
                Spawn.SetActive(true);
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

                    Vector3 Move = new Vector3(Direction.x * Speed, 0, Direction.z * Speed);
                    rb.velocity = Move;
                    transform.forward = Move;
                }
                else if (Hit.collider.tag == "Player" && (hp <= RageHP))
                {
                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    Vector3 Move = new Vector3(Direction.x * RageSpeed, 0, Direction.z * RageSpeed);
                    rb.velocity = Move;
                    transform.forward = Move;
                }
            }
        }
    }
}
