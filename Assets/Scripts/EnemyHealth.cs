using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private float currentHealth;
    public SkinnedMeshRenderer enemyMesh;
    private Color originalColor;
    [SerializeField] private Color damageColor;
    [SerializeField] private GameObject deathParticle;

    private bool isDead = false; 

    void Start()
    {
        currentHealth = enemyHealth;
        originalColor = enemyMesh.material.color;
    }
    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }
    public bool IsAlive()
    {
        return currentHealth > 0;
    }
    // When trigger entered, enemy takes damage if the trigger was a spell.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEntered");
        if (other.CompareTag("Spell"))
        {
            Debug.Log("OnTriggerSucceeded");
            TakeDamage(other.GetComponent<DealDamage>().GetDamage());
            StartCoroutine(FlashColor(damageColor, .25f));
        }
        
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
    }
    // Flash color to signal damage taken.
    private IEnumerator FlashColor(Color flashColor, float duration)
    {
        enemyMesh.material.color = flashColor;
        
        yield return new WaitForSeconds(duration);

        enemyMesh.material.color = originalColor;
    }
    private void Die()
    {
        isDead = true;
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}

