using UnityEngine;

public class AbilityCharacter : MonoBehaviour
{
    public int maxAbilities = 7; // maximum number of abilities that the character can have
    public AbilitySlot[] abilitySlots; // array of ability slots for the character
    public float resourceRegenRate; // rate at which resources are regenerated

    void Awake()
    {
        // Initialize the ability slots array with the specified number of slots
        abilitySlots = new AbilitySlot[maxAbilities];
    }

    void Update()
    {
        // Decrement cooldowns and increment resources each frame
        foreach (AbilitySlot slot in abilitySlots)
        {
            slot.cooldown = Mathf.Max(slot.cooldown - Time.deltaTime, 0);
            slot.resources = Mathf.Min(slot.resources + (int)(resourceRegenRate * Time.deltaTime), slot.ability.maxResources);
        }
    }

    // Method to add an ability to the character's ability list
    public void AddAbility(Ability ability)
    {
        // Check if there is an empty slot available
        AbilitySlot emptySlot = null;
        foreach (AbilitySlot slot in abilitySlots)
        {
            if (slot.ability == null)
            {
                emptySlot = slot;
                break;
            }
        }

        // If there is an empty slot, add the ability to it
        if (emptySlot != null)
        {
            emptySlot.ability = ability;
        }
    }

    // Method to remove an ability from the character's ability list
    public void RemoveAbility(Ability ability)
    {
        AbilitySlot foundSlot = null;
        foreach (AbilitySlot slot in abilitySlots)
        {
            if (slot.ability == ability)
            {
                foundSlot = slot;
                break;
            }
        }
        if (foundSlot != null)
        {
            foundSlot.ability = null;
        }
    }
}
