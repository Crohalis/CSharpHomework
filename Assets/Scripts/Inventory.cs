using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    private const int MaxItemTypes = 10;
    private Dictionary<string, int> m_items = new Dictionary<string, int>();
    
    public int GetItemCount(string item)
    {
        if (m_items.ContainsKey(item))
        {
            return m_items[item];
        }

        return 0;
    }
    
    public void AddItem(string item, int count = 1)
    {
        if (count <= 0)
        {
            Debug.LogWarning("Количество для добавления должно быть больше нуля.");
            return;
        }

        if (m_items.ContainsKey(item))
        {
            m_items[item] = m_items[item] + count;
            return;
        }

        if (m_items.Count >= MaxItemTypes)
        {
            Debug.LogWarning("Инвентарь заполнен. Нельзя добавить новый предмет: " + item);
            return;
        }

        m_items.Add(item, count);
    }
    

    public void RemoveItem(string item, int count = 1)
    {
        if (count <= 0)
        {
            Debug.LogWarning("Количество для удаления должно быть больше нуля.");
            return;
        }

        if (!m_items.ContainsKey(item))
        {
            Debug.LogWarning("Предмет не найден: " + item);
            return;
        }

        if (m_items[item] < count)
        {
            Debug.LogWarning("Недостаточно предметов для удаления: " + item);
            return;
        }

        m_items[item] = m_items[item] - count;

        if (m_items[item] == 0)
        {
            m_items.Remove(item);
        }
    }

    public void Print()
        {
            int totalCount = 0;
            foreach (KeyValuePair<string, int> pair in m_items)
            {
                Debug.Log(pair.Key + " - " + pair.Value);
                totalCount = totalCount + pair.Value;
            }
            Debug.Log("Разных предметов: " + m_items.Count);
            Debug.Log("Всего предметов: " + totalCount);
        }
    
}
