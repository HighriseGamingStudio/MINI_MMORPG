using UnityEngine;

// ScriptableObject class for an ability
[CreateAssetMenu(menuName = "Ability")]
public class Ability : ScriptableObject
{
    public string abilityName; // name of the ability
    public string description; // description of the ability
    public float duration; // duration of the ability
    public float cooldown; // cooldown time for the ability
    public int resourceCost; // resource cost of using the ability (such as mana or energy)
    public int maxResources; // maximum number of resources that the 
    public Sprite icon; // icon to display for the ability


    // Method to execute the ability's logic
    public virtual void UseAbility()
    {
        // Insert code here for the default behavior of the ability
        Debug.Log("Using ability: " + abilityName);
    }
}