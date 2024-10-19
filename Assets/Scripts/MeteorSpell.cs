using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MeteorSpell : Spell
{
    [SerializeField] private float downwardVelocity = 10f;
    private Rigidbody rb;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject explosion;
    private bool start = true;
    private bool collided = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetInitialVelocity();
        start = false;
    }
    
    // Reset values to false. Start boolean is used to prevent null exception error.
    void OnEnable()
    {
        collided = false;
        SetInitialVelocity();
        if (!start)
        {
            explosion.SetActive(false);
            MakeInvisible(rock, false);
            MakeInvisible(fire, false);
            MakeInvisible(fire.transform.GetChild(0).gameObject, false);
            MakeInvisible(fire.transform.GetChild(1).gameObject, false);
        }

    }
    void SetInitialVelocity()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.down * downwardVelocity;
        }
    }

    // Play explosion and make meshes invisible, then return the spell.
    // collided boolean used to prevent multiple collision calls.
    private void OnCollisionEnter(Collision collision)
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);

        if (!collided)
        {
            explosion.SetActive(true);
            float duration = 1.5f;
            explosion.GetComponent<ParticleSystem>().Play();
            MakeInvisible(rock, true);
            MakeInvisible(fire, true);
            MakeInvisible(fire.transform.GetChild(0).gameObject, true);
            MakeInvisible(fire.transform.GetChild(1).gameObject, true);
            StartCoroutine(ReturnSpell(duration));
            collided = true;
        }
    }
    void MakeInvisible(GameObject prefab, bool invisible)
    {
        Renderer renderer = prefab.GetComponent<Renderer>();
        if (renderer != null && invisible)
        {
            renderer.enabled = false;
        }
        else if (renderer != null)
        {
            renderer.enabled = true;
        }
    }
    private IEnumerator ReturnSpell(float duration) {
        yield return new WaitForSeconds(duration);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
