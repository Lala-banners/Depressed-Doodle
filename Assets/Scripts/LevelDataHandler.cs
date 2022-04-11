using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using NullRE = System.NullReferenceException;

namespace Steph.Level
{
    public class LevelDataHandler : MonoBehaviour
    {
        #region Variables and Properties

        [SerializeField] private Collectibles collectibles;
        [SerializeField] private Enemies enemies;
        [SerializeField] private Obstacles obstacles;
        [SerializeField] private FallingArea fallingArea;

        #endregion

        private void Awake()
        {
            collectibles.Validate();
            enemies.Validate();
            obstacles.Validate();
            fallingArea.Validate();
        }


        #region My Methods

        //

        #endregion
    }


    [Serializable]
    public class Collectibles : ICollide
    {
        #region Variables and Properties

        [SerializeField] private LayerMask layer;
        [SerializeField] private GameObject prefab;
        [SerializeField, Min(0)] private int value;

        [Header("Possibly not for implementing")] [SerializeField, Range(1, 10)]
        private int frequency;

        #endregion


        #region Methods

        public void Validate()
        {
            if (layer == 0 || layer == 119) throw new NullRE("Collectible layer not valid.");
            if (prefab == null) throw new NullRE("Collectible prefab is not attached to Level Data Handler.");
            if (value == 0) throw new WarningException("Collectibles will not add to score if value is 0.");
        }

        public void OnCollision()
        {
            //get collectible
        }

        #endregion
    }

    [Serializable]
    public class Enemies : ICollide
    {
        #region Variables and Properties

        [SerializeField] private LayerMask layer;
        [SerializeField] private GameObject prefab;

        #endregion

        #region Methods

        public void Validate()
        {
            if (layer == 0 || layer == 119) throw new NullRE("Enemy layer not valid.");
            if (prefab == null) throw new NullRE("Enemy prefab is not attached to Level Data Handler.");
        }

        public void OnCollision()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [Serializable]
    public class Obstacles : ICollide
    {
        #region Variables and Properties

        [SerializeField] private LayerMask layer;
        [SerializeField] private GameObject[] prefabs;

        #endregion

        #region Methods

        public void Validate()
        {
            if (layer == 0 || layer == 119) throw new NullRE("Obstacle layer not valid.");
            if (prefabs.Length < 1) throw new NullRE("Obstacle prefabs are not attached to Level Data Handler.");
        }

        public void OnCollision()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [Serializable]
    public class FallingArea : ICollide
    {
        #region Variables and Properties
        
        [SerializeField] private LayerMask layer;

        #endregion

        #region Methods

        public void Validate()
        {
        }

        public void OnCollision()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public interface ICollide
    {
        void Validate();
        void OnCollision();
    }
}