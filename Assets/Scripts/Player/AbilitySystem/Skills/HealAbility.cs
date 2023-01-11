using UnityEngine;

// Derived class for a healing ability
public class HealAbility : Ability
{
    public int healAmount; // amount of health to restore

    // Override the UseAbility method to provide a custom implementation
    public override void UseAbility()
    {
        // Insert code here to handle the healing effects
        Debug.Log("Healing for " + healAmount + " points!");
    }
}