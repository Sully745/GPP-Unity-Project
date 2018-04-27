using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantUI : MonoBehaviour
{
    private static bool created = false;
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
}
