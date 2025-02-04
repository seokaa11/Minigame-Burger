using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HUD : MonoBehaviour
{
    public static Action Submit;

    public void submitBurger()
    {
        Submit();
    }

}
