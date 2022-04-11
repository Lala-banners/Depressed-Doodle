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
            Debug.Log("collided");
            LevelDataHandler.Instance.Collectibles.OnCollision(this);
        }
    }
}