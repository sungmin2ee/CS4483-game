using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Instance that will store prefabs
    public GameObject[] prefabs;
    // Lists that will pool prefabs
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // Check for inactive objects in the pool
        foreach (GameObject item in pools[index])
        {
            if (item != null && !item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // If no inactive objects are available, instantiate a new one
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

    public void ReturnToPool(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(false); // Deactivate the object
        }
    }
}
