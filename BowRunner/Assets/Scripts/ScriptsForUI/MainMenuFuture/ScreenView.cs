using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenView : Singleton<ScreenView>
{
   protected virtual void Awake()
    {
        Instance = this;
    }
}
