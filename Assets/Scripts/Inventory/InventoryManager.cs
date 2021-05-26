﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    /*private List<string> _items;

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        _items = new List<string>();
        status = ManagerStatus.Started;
    }
    
    private void DisplayItems()
    {
        string itemDisplay = "List of Items: ";
        foreach (string item in _items)
        {
            itemDisplay += item + " ";
        }
        Debug.Log(itemDisplay);
    }
    
    public void AddItem(string name)
    {
        _items.Add(name);
        DisplayItems();
    }*/

    private Dictionary<string, int> _items;
    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        _items = new Dictionary<string, int>();
        status = ManagerStatus.Started;
    }

    private void DisplayItems()
    {
        string itemDisplay = "List of Items: ";
        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + "(" + item.Value + ") ";
        }
        Debug.Log(itemDisplay);
    }

    public void AddItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] += 1;
        }
        else
        {
            _items[name] = 1;
        }
        DisplayItems();
    }

    public List<string> GetItemList()
    {
        List<string> list = new List<string>(_items.Keys);
        return list;
    }
    public int GetItemCount(string name)
    {
        if (_items.ContainsKey(name))
        {
            return _items[name];
        }
        return 0;
    }

    public void ConsumeItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name]--;
            if (_items[name] == 0)
            {
                _items.Remove(name);
            }
        }
        else
        {
            Debug.Log("cannot consume " + name);
        }
        DisplayItems();
    }
}