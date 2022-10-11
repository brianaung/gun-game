// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    public HealthBar healthBar;

    // Using an event to maximise component re-use. Any other component can 
    // listen to this event to do arbitrary actions when this game object dies.
    [SerializeField] private UnityEvent onDeath;

    private int _currentHealth;

    private int CurrentHealth
    {
        get => this._currentHealth;
        set
        {
            // Using a C# property to ensure the onHealthChanged event is
            // consistently fired when the health changes, and also to check if
            // the object has died (<= 0 health). It's not really different to
            // the concept of a "setter" as per OOP good practice, however, we
            // can still treat it like an integer variable (add, subtract, etc).
            this._currentHealth = value;
            
            if (CurrentHealth <= 0) // Did we die?
            {
                // Let onDeath event listeners know that we died. 
                this.onDeath.Invoke();

                // Destroy ourselves.
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        healthBar.SetMaxHealth(startingHealth);
        ResetHealthToStarting();
    }

    public void ResetHealthToStarting()
    {
        CurrentHealth = this.startingHealth;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.SetHealth(this._currentHealth);
    }
}
