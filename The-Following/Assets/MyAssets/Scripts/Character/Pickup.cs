using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField]
    int amount;

    enum Type
    {
        Protein
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.GiveProtein(amount);
            Destroy(transform.parent.gameObject);
        }
    }

}
