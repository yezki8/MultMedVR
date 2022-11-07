using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float startForce = 50f;

    Rigidbody rb;
    bool isActive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startForce *= Random.Range(0.85f, 1.05f);
        rb.AddRelativeForce(startForce * Vector3.forward);
        rb.AddRelativeTorque(startForce * Vector3.right);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Blade" && isActive)
        {
            isActive = false;

            Vector3 direction = (col.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            FruitSaberManager.Instance.InvokeOnHitBomb(this);
            transform.GetComponent<DissolveController>().StartDissolver();
            StartCoroutine(SelfDestructTimer());
        }
        if (col.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

}
