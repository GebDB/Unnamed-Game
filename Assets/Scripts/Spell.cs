using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spell : MonoBehaviour
{
    [SerializeField] protected Sprite spellSprite;
    [SerializeField] protected GameObject spellObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetSpellObject()
    {
        return spellObject;
    }
    public Sprite GetSpellSprite()
    {
        return spellSprite;
    }
}
