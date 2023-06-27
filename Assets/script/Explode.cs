using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject exp;
    public float expForce, radius;
    public bool expBool = false;
    public GameObject gas;
    public GameObject Exp;
    public AudioSource expAudio;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject _exp = Instantiate(exp, transform.position, transform.rotation);
            Destroy(_exp, 3);
            knockback();
            expBool = true;
            expAudio.Play();
            gas.SetActive(true);
            Exp.SetActive(true);

            if (expBool)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Projectile")
        {
            GameObject _exp = Instantiate(exp, transform.position, transform.rotation);
            Destroy(_exp, 3);
            knockback();
            expAudio.Play();
            expBool = true;
            Exp.SetActive(true);

            if (expBool)
            {
                Destroy(gameObject);
            }
        }
    }

    void knockback()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearyby in colliders)
        {
            Rigidbody rigg = nearyby.GetComponent<Rigidbody>();
            if (rigg != null)
            {
                rigg.AddExplosionForce(expForce, transform.position, radius);
            }
        }
    }
}
