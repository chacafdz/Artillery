using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objetivo : MonoBehaviour
{
    public UnityEvent GameWon;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            GameWon.Invoke();
            Destroy(this.gameObject);
        }
    }
}