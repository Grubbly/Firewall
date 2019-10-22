using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class WallHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private GameObject hitEffect;
    [SerializeField]
    private GameObject destroyEffect;
    [SerializeField]
    private WallManager wallManager;
    private PhotonView photonView;
    

    // Start is called before the first frame update
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        wallManager.register(photonView.ViewID, maxHealth);
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(gameObject + " hit by " + other.gameObject);
        if(other.gameObject.tag == "malware") {
            GameObject effect = Instantiate(
                hitEffect, 
                other.gameObject.GetComponent<Rigidbody>().position,
                other.gameObject.GetComponent<Rigidbody>().rotation
            );

            wallManager.takeDamage(photonView.ViewID, 1);

            Destroy(other.gameObject);
            Destroy(effect, 1f);
        }
    }

    private void Update() {
        if(wallManager.getHealth(photonView.ViewID) <= 0) {
            GameObject effect = Instantiate(
                destroyEffect, 
                gameObject.transform.position,
                gameObject.transform.rotation
            );
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
