using UnityEngine;

// Class to represent an ability slot
public class AbilitySlot
{
    public Ability ability; // reference to the ScriptableObject ability
    public float cooldown; // current cooldown for the ability
    public int resources; // current resource cost for the ability

    // Method to trigger the ability and update the cooldown and resource variables
    public void TriggerAbility()
    {
        if (cooldown <= 0 && resources >= ability.resourceCost)
        {
            // Execute the ability's logic and decrement resources
            ability.UseAbility();
            resources -= ability.resourceCost;

            // Set the cooldown for the ability
            cooldown = ability.cooldown;
        }
    }
}
