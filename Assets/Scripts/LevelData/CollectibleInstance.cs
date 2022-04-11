using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steph.Level
{
    public class CollectibleInstance : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((1 << other.gameObject.layer & LevelDataHandler.Instance.PlayerLayer) != 0)  {
                LevelDataHandler.Instance.Collectibles.OnCollision(this);
            }
        }
    }
}