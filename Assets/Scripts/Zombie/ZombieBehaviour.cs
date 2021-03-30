using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;

public class ZombieBehaviour : MonoBehaviour
{
    NavMeshAgent2D pathfinder;
    ZombieMovement zombieMovement;
    SpriteRenderer spriteRenderer;
    Health health;
    bool isFlashing;
    GameObject objectToFollow;
    public bool ShouldFollow
    {
        get
        {
            if (pathfinder.speed == 0)
                return false;
            return true;
        }
        set
        {
            pathfinder.speed = value == true ? moveSpeed : 0;
            zombieMovement.IsAnimating = value;
        }
    }
    [SerializeField] ZombieType type;
    [SerializeField] float demagePerSecond;
    [SerializeField] float moveSpeed;

    void Awake()
    {
        pathfinder = GetComponent<NavMeshAgent2D>();
        zombieMovement = GetComponent<ZombieMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        isFlashing = false;
    }


    public void Initialize(GameObject objectToFollow)
    {
        this.objectToFollow = objectToFollow;
        pathfinder.speed = moveSpeed;
        health.OnDamaged += OnDamaged;
    }

    private void OnDamaged(Health health, float amount, GameObject source)
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        if (isFlashing)
            yield return null;
        else
        {
            Color defaultColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            isFlashing = true;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = defaultColor;
            isFlashing = false;
        }
    }

    void FixedUpdate()
    {
        pathfinder.SetDestination(objectToFollow.transform.position);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(demagePerSecond * Time.fixedDeltaTime, gameObject);
        }
    }
}
