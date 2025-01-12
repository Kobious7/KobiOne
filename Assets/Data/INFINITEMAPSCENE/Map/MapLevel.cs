using System;
using UnityEngine;

namespace InfiniteMap
{
    public class MapLevel : MapAb
    {
        [SerializeField] private int currentLevel = 0;
        [SerializeField] private int previousLevel = 0;

        public int PreviousLevel => previousLevel;

        private void FixedUpdate()
        {
            if (!Changed()) return;

            previousLevel = currentLevel;
        }

        private bool Changed()
        {
            currentLevel = (int)Map.Distance / 500;

            if (currentLevel == previousLevel) return false;

            return true;
        }
    }
}