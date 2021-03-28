﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}