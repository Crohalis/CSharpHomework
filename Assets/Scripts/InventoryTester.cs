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
    
    private IEnumerator Start()
    
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
        Debug.Log("Проверка удаления отсутствующего предмета");
        inventory.RemoveItem("Bow", 1);

        Debug.Log("Проверка удаления большего количества, чем есть");
        inventory.RemoveItem("Sword", 100);

        Debug.Log("Проверка переполнения инвентаря");

        inventory.AddItem("Shield");
        inventory.AddItem("Helmet");
        inventory.AddItem("Ring");
        inventory.AddItem("Key");
        inventory.AddItem("Gem");
        inventory.AddItem("Map");
        inventory.AddItem("Book");

        inventory.AddItem("OverflowItem");
        inventory.Print();
        
    }
    private void UpdateTexts(Inventory inventory)
    {
        int potionCount = inventory.GetItemCount("Potion");
        int swordCount = inventory.GetItemCount("Sword");
        int appleCount = inventory.GetItemCount("Apple");
        potionText.text = "Potion x" + potionCount;
        swordText.text = "Sword x" + swordCount;
        appleText.text = "Apple x" + appleCount;

        
            inventory.Print();
            inventory.AddItem("Potion", 0);
            inventory.AddItem("Potion", -2);
            
            inventory.RemoveItem("Potion", 0);
            inventory.RemoveItem("Potion", -2);
            }
    }
   
