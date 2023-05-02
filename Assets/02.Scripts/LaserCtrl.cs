using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCtrl : MonoBehaviour
{
    public float damage;
    public float force = 5000.0f;
    private Rigidbody rb;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        tr.Rotate(0, -90, 90);
        rb.AddForce(-transform.up * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("DRONE"))
        {
            if (other.CompareTag("PLAYER"))
                other.transform.GetComponent<PlayerCtrl>()?.OnDamage();
            Destroy(this.gameObject);
        }
    }

}
