using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    public int GetItemCount(string item)
    {
        if (items.ContainsKey(item))
        {
            return items[item];
        }

        return 0;
    }

    public void AddItem(string item, int count = 1)
    {
        if (items.ContainsKey(item))
        {
            items[item] = items[item] + count;
        }
        else
        {
            
            if (items.Count >= 10)
            {
                Debug.LogWarning("Инвентарь заполнен. Нельзя добавить новый предмет: " + item);
            }
            else
            {
                items.Add(item, count);
            }
        }
    }

    public void RemoveItem(string item, int count = 1)
    {
        if (!items.ContainsKey(item))
        {
            Debug.LogWarning("Предмет не найден: " + item);
            return;
        }

        if (items[item] < count)
        {
            Debug.LogWarning("Недостаточно предметов для удаления: " + item);
            return;
        }

        items[item] = items[item] - count;
        if (items[item] == 0)
        {
            items.Remove(item);
        }
    }

    public void Print()
        {
            int totalCount = 0;
            foreach (KeyValuePair<string, int> pair in items)
            {
                Debug.Log(pair.Key + " - " + pair.Value);
                totalCount = totalCount + pair.Value;
            }
            Debug.Log("Разных предметов: " + items.Count);
            Debug.Log("Всего предметов: " + totalCount);
        }
    
}
