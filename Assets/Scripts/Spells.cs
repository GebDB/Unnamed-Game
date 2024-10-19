
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    [SerializeField] private GameObject[] spells = new GameObject[9];
    [SerializeField] private Button[] hotbarSlots; // Array of button slots
    private PlayerControls playerControls;
    private Vector3 clickPosition;
    [SerializeField] private GameObject clickSprite;
    private Camera mainCamera;
    private Transform targetEnemy;
    private GameObject clickSpriteInstance;
    [SerializeField] private float clickSpriteYOffset = 0.05f;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Player.Fire.performed += ctx => SelectTarget();
        playerControls.Player.Spells.performed += ctx => FireSpell();
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Continuously update click position if target enemy is selected.
    void Update()
    {
        if (targetEnemy != null)
        {
            clickPosition = targetEnemy.position + new Vector3(0, clickSpriteYOffset, 0);
            if (clickSpriteInstance != null)
            {
                if (targetEnemy.GetComponent<EnemyHealth>().IsAlive())
                {
                    clickSpriteInstance.transform.position = clickPosition;
                }
                else
                {
                    clickSpriteInstance.transform.position = clickPosition + new Vector3(0, -100f, 0);
                }
            }
        }
    }

    // Sets a spell in the spells object and adds the spell to the hotbar.
    public void SetSpell(Spell spell, int i)
    {
        if (i < spells.Length)
        {
            spells[i] = spell.GetSpellObject();
            hotbarSlots[i].GetComponent<Image>().sprite = spell.GetSpellSprite();

            Image imageComponent = hotbarSlots[i].GetComponent<Image>();
            Color color = imageComponent.color; 
            color.a = 1; 
            imageComponent.color = color;
        }
    }
    

    // Handles targeting for spells. Done through raycasts and left clicks.
    private void SelectTarget()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("raycast hit");
            if (hit.collider.CompareTag("Enemy"))
            {
                targetEnemy = hit.collider.transform;
                clickPosition = targetEnemy.position + new Vector3(0, clickSpriteYOffset, 0);
                Debug.Log("Enemy targeted" + targetEnemy);
            }
            else
            {
                targetEnemy = null;
                clickPosition = hit.point + new Vector3(0, clickSpriteYOffset, 0);
            }
            
        }
        if (clickSpriteInstance == null)
        {
            clickSpriteInstance = Instantiate(clickSprite, clickPosition, Quaternion.Euler(90, 0, 0));
        }

        clickSpriteInstance.transform.position = clickPosition;
        
        Debug.Log("Select Target Entered" + " " + mousePosition);
    }

    // Fires a spell at the clickPosition.
    void FireSpell()
    {
        if (clickPosition != Vector3.zero && spells[0] != null)
        {
            Debug.Log("Spell Fired");
            Vector3 spawnPosition = clickPosition + new Vector3(0, 30, 0);
            // Instantiate the object
            //Instantiate(spells[0], spawnPosition, Quaternion.identity);
            ObjectPoolManager.SpawnObject(spells[0], spawnPosition, Quaternion.identity);
        }
    }

    public GameObject[] GetSpells()
    {
        return spells;
    }
    public Vector3 GetClickPosition()
    {
        return clickPosition;
    }
}
