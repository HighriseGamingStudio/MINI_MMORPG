using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName; // name of the character
    public int maxHealth; // maximum health points
    public int currentHealth; // current health points
    public int maxMana; // maximum mana points
    public int currentMana; // current mana points
    public int strength; // strength stat
    public int agility; // agility stat
    public int intelligence; // intelligence stat
    public int defense; // defense stat
    public int attackPower; // attack power stat
    public int magicPower; // magic power stat
    public int accuracy; // accuracy stat
    public int evasion; // evasion stat
    // Not Sure if i need this
    // public Ability[] abilities; // array of abilities for the character
    public int level; // current level of the character
    public int experience; // current experience of the character
    public int experienceToNextLevel; // experience required to reach the next level

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
    }

    // Method to restore health
    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    // Method to use mana
    public void UseMana(int amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0);
    }

    // Method to restore mana
    public void RestoreMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }

    // Method to add experience and level up the character if necessary
    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    // Method to level up the character and increase stats
    private void LevelUp()
    {
        level++;
        maxHealth += 10;
        maxMana += 5;
        strength += 2;
        agility += 2;
        intelligence += 2;
        defense += 1;
        attackPower += 3;
        magicPower += 3;
        accuracy += 1;
        evasion += 1;
        experienceToNextLevel = (int)(experienceToNextLevel * 1.5f);
    }
}
