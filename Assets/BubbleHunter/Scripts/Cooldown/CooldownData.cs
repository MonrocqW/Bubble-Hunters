using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubHun.Cooldown
{
    public struct CooldownData
    {
        public float progress;
        public float timeLeft;
        public int storedCharges;
        public int maxCharges;
    }
}