using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.name + " - Tag: " + other.tag);
        if (other.tag == "Explosion") Destroy(this.gameObject);
    }
}
