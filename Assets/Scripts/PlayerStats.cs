using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float spiritEnergy;

    private PlayerStatInvoker playerStateInvoker;
    private ShiftInvoker shiftInvoker;

    private AnimalType currentAnimal;

    // Start is called before the first frame update
    void Start()
    {
        playerStateInvoker = InvokerHolder.Instance.PlayerStatInvoker;
        playerStateInvoker.playerHitAction += PlayerTakeDamage;

        shiftInvoker = InvokerHolder.Instance.ShiftInvoker;
        shiftInvoker.shiftOccurred += UpdateAnimalType;
    }

    private void OnEnable()
    {
        if (playerStateInvoker != null)
        {
            playerStateInvoker.playerHitAction += PlayerTakeDamage;
        }

        if(shiftInvoker != null)
        {
            shiftInvoker.shiftOccurred += UpdateAnimalType;
        }
    }

    private void OnDisable()
    {
        playerStateInvoker.playerHitAction -= PlayerTakeDamage;
        shiftInvoker.shiftOccurred -= UpdateAnimalType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealPlayer(float amount)
    {
        health += amount;

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        //Update UI
    }

    public void PlayerTakeDamage(float amount)
    {
        health -= amount;

        //Update UI

        if (health <= 0)
        {
           //Die
        }
    }

    private void UpdateAnimalType(AnimalType newAnimalType)
    {
        currentAnimal = newAnimalType;
    }
}
