using UnityEngine;
using UnityEngine.Events;

public interface IDamageable {
    void TakeDamage(float amount = 25);
}

public interface IHealable {
    void Heal(float amount);
}

public enum State {
    PreSpawn,
    Alive,
    Dead
}

public class Health : MonoBehaviour, IDamageable, IHealable {
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public State state;

    // Regeneration parameters
    [SerializeField] private bool canRegenerate = false;
    [SerializeField] private float regenerationRate = 5f;

    // Declare events to signal state changes
    public UnityEvent OnDeath;
    public UnityEvent OnSpawn;

    private void Start() {
        currentHealth = maxHealth;
        SetState(State.Alive);
    }

    private void Update() {
        // Regenerate health over time
        if (canRegenerate && state == State.Alive) {
            currentHealth += regenerationRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }
    }

    public void TakeDamage(float amount = 25) {
        if (state != State.Alive) {
            Debug.LogWarning("Damage applied to non-alive state. Ignored.");
            return;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f) {
            SetState(State.Dead);
        }
    }

    public void Heal(float amount) {
        if (state != State.Alive) {
            Debug.LogWarning("Heal applied to non-alive state. Ignored.");
            return;
        }

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public void SetHealth(float amount) {
        currentHealth = amount;
    }

    public float GetHealth() {
        return currentHealth;
    }

    private void HandleDeath() {
        Debug.Log("The object is dead.");
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    public void SetState(State newState) {
        if (state == newState)
            return;

        state = newState;

        HandleStateActions();
    }

    private void HandleStateActions() {
        switch (state) {
            case State.PreSpawn:
                break;
            case State.Alive:
                break;
            case State.Dead:
                HandleDeath();
                break;
        }
    }

    public void ToggleCanRegenerate() {
        canRegenerate = !canRegenerate;
    }

    public void SetRegenerateRate(float regenRate) {
        regenerationRate = regenRate;
    }

    public void Spawn(float? health = null) {
        if (state != State.PreSpawn) {
            Debug.LogWarning("Spawn called on non-pre-spawn state. Ignored.");
            return;
        }

        SetState(State.Alive);
        SetHealth(health ?? maxHealth);

        // Trigger the OnSpawn event
        OnSpawn.Invoke();
    }
}
