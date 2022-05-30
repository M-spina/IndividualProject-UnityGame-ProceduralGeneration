using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class is used to pervent object form being destroyed when trasitioning to a different scene in the game

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
