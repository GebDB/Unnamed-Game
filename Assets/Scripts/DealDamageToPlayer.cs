using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToPlayer : MonoBehaviour
{
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // When trigger entered, deal damage to the player.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Deal Damage to Player on trigger entered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Deal Damage to Player on condition entered");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
