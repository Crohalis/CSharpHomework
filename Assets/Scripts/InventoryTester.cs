using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    void Start()
    {
        Inventory inventory = new Inventory();
        inventory.AddItem("Potion", 3);
        inventory.AddItem("Sword", 2);
        inventory.AddItem("Apple", 5);

        Debug.Log("Добавляем 2 Potion");
        inventory.AddItem("Potion", 2);

        Debug.Log("Удаляем 1 Potion");
        inventory.RemoveItem("Potion", 1);

        inventory.Print();
    }
    
}
