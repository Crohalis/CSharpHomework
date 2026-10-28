using System.Collections;
using UnityEngine;
using TMPro;

public class InventoryTester : MonoBehaviour
{
    public TMP_Text potionText;
    public TMP_Text swordText;
    public TMP_Text appleText;
    public Transform potion;
    public Transform sword;
    public Transform apple;
    
    IEnumerator Start()
    
    {
        Inventory inventory = new Inventory();
        inventory.AddItem("Potion", 3);
        inventory.AddItem("Sword", 2);
        inventory.AddItem("Apple", 5);
        
        UpdateTexts(inventory);

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Добавляем 2 Potion");
        inventory.AddItem("Potion", 2);
        
        UpdateTexts(inventory);
        yield return new WaitForSeconds(1.5f);

        Debug.Log("Удаляем 1 Potion");
        inventory.RemoveItem("Potion", 1);
        UpdateTexts(inventory);

        inventory.Print();
        
    }
    void UpdateTexts(Inventory inventory)
    {
        int potionCount = inventory.GetItemCount("Potion");
        int swordCount = inventory.GetItemCount("Sword");
        int appleCount = inventory.GetItemCount("Apple");
        potionText.text = "Potion x" + potionCount;
        swordText.text = "Sword x" + swordCount;
        appleText.text = "Apple x" + appleCount;

        
    }
}
