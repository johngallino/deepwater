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
        
        
        anim.SetBool("isDead", false);
        StartCoroutine(AirDepletion(AirDepletionRate));
    }

    // Update is called once per frame
    void Update() {

        healthbar.value = CalculateHealth();
        airbar.value = CalculateAir();
        // Press X to self-inflict damage for testing
        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(6f);

    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "Projectile")
        {
            anim.SetTrigger("Impact");
            // Take damage value from projectile object and subtract it from player health
            GameObject projectile = _collision.gameObject;
            projectilescript = projectile.GetComponent<projectile>();
            damageFromEnemy = projectilescript.enemyDamage;
            DealDamage(damageFromEnemy);
            
        }
    }

    void DealDamage(float damageValue)
    {
        // Deduct the damage dealt from the character's health
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();
        // If the character is out of health, die!

        if (CurrentHealth <= 0)
        {
            
            isDead = true;
            Die();
        }
    }

   private IEnumerator AirDepletion(float airrate)
    {
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
