﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class framerate : MonoBehaviour
{
    void Awake() {
        Application.targetFrameRate = 40;    
    }
}
