using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CannonBall : MonoBehaviourPun
{
    public float thrust = 500f;
    public float ttl = 5f;

    private PhotonView photonView;

    IEnumerator destroyCannonBall() {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrust);

        photonView = GetComponent<PhotonView>();
        StartCoroutine("destroyCannonBall");
    }
}
