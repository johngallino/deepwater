using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErikaHealth : MonoBehaviour {
    public float CurrentHealth;
    public float MaxHealth = 20f;
    public Slider healthbar;
    public float CurrentAir;
    public float MaxAir = 100f;
    public Slider airbar;
    public float AirDepletionRate = 5f;
    public bool isDead = false;
    private Animator anim;
    private int damageFromEnemy;
    private projectile projectilescript;
    
    // Use this for initialization
    void Start() {
        
        anim = gameObject.GetComponentInChildren<Animator>();
        MaxHealth = 20f;
        // Resets health and air to full on game load
        CurrentHealth = MaxHealth;
        CurrentAir = MaxAir;
        healthbar.value = CalculateHealth();
        airbar.value = CalculateAir();
        anim.SetBool("isDead", false);
        StartCoroutine(AirDepletion(AirDepletionRate));
    }

    // Update is called once per frame
    void Update() {

        // Press X to self-inflict damage for testing
        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6f);

    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "Projectile")
        {
            // Take damage value from projectile object and subtract it from player health
            Debug.Log("Erika shot with projectile");
            GameObject projectile = _collision.gameObject;
            projectilescript = projectile.GetComponent<projectile>();
            Debug.Log("projectile is " + projectile);
            damageFromEnemy = projectilescript.enemyDamage;
            DealDamage(damageFromEnemy);
        }
    }

    void DealDamage(float damageValue)
    {
        // Deduct the damage dealt from the character's health
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();
        Debug.Log("Current Health is " + CurrentHealth);
        // If the character is out of health, die!

        if (CurrentHealth <= 0)
        {
            Debug.Log("Current Health is " + CurrentHealth);
            isDead = true;
            Die();
        }
    }

   private IEnumerator AirDepletion(float airrate)
    {
        Debug.Log("AirDepletion Coroutine started");
        while (CurrentAir > 0)
        {
            CurrentAir -= airrate;
            airbar.value = CalculateAir();
            yield return new WaitForSeconds(1f);
        }

        while (CurrentAir <= 0 && isDead == false)
        {
            DealDamage(2);
            yield return new WaitForSeconds(1f);
        }

        /*else if (CurrentAir <= 0)
        {
            Debug.Log("Out of Air!");
            CurrentHealth -= 2f * Time.deltaTime;
            healthbar.value = CalculateHealth();
            if (CurrentHealth <= 0)
            {
                Debug.Log("You dead!");
                isDead = true;
                Die();
            }
         }*/
            
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    float CalculateAir()
    {
        return CurrentAir / MaxAir;
    }

    void Die()
    {
        CurrentHealth = 0;
        anim.SetBool("isDead", true);
        
    }
}
