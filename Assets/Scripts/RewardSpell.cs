using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpell : MonoBehaviour
{
    [SerializeField] private Spell[] spells;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Reward trigger entered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Reward True");
            int randomSpellIndex = spells.Length - 1;
            Spells playerSpells = other.gameObject.GetComponent<Spells>();

            // Set the spell and initialize its pool
            playerSpells.SetSpell(spells[randomSpellIndex], randomSpellIndex);

            Destroy(gameObject);
        }
    }
}
