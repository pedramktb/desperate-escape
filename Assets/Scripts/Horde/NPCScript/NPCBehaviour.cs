using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;
public abstract class NPCBehaviour : MonoBehaviour
{
    Health m_Health;
    NPCData m_data;
    AgentMovement movement;
    SpriteRenderer spriteRenderer;
    bool isFlashing;
    public int IsUnderAttack { get; private set; }
    public int Value { get; private set; }

    private void OnDamaged(Health health, float amount, GameObject source)
    {
        StartCoroutine(Flash());
    }

    private void OnDeath(Health health)
    {
        Destroy(gameObject);
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

    void Awake()
    {
        m_Health = GetComponent<Health>();
        movement = GetComponent<AgentMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFlashing = false;
    }

    public virtual void Initialize(NPCData data)
    {
        m_data = data;
        movement.Initialize(data.MoveSpeed);
        m_Health.SetMaxHealth(data.HP);
        m_Health.SetMaxShield(0);
        m_Health.OnDamaged += OnDamaged;
        m_Health.OnDeath += OnDeath;
        Value = data.Value;
    }

    public void SetDestination(Vector2 pos)
    {
        movement.SetDestination(pos);
    }
}
