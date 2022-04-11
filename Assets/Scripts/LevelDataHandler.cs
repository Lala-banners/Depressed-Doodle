using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lara.Movement;
using Sirenix.Utilities;
using UnityEngine;
using NullRE = System.NullReferenceException;

namespace Steph.Level
{
    public class LevelDataHandler : MonoBehaviour
    {
        #region Variables and Properties

        public static LevelDataHandler Instance;

        public static int Score;

        //serialised private
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Collectibles collectibles;
        [SerializeField] private Enemies enemies;
        [SerializeField] private Obstacles obstacles;
        [SerializeField] private FallingArea fallingArea;

        //public properties
        public LayerMask PlayerLayer => playerLayer;
        public Collectibles Collectibles => collectibles;
        public Enemies Enemies => enemies;
        public Obstacles Obstacles => obstacles;
        public FallingArea FallingArea => fallingArea;

        #endregion

        private void Awake()
        {
            Instance = this;
            Score = 0;

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

        //serialised private
        [SerializeField] private LayerMask layer;
        [SerializeField] private GameObject prefab;
        [SerializeField, Min(0)] private int value;
        [SerializeField] private Transform collectibleParentObject;
        [SerializeField] private string defaultName;

        //local
        private List<CollectibleInstance> activeCollectibles;

        #endregion


        #region Methods

        public void Validate()
        {
            if (layer == 0 || layer == 119) throw new NullRE("Collectible layer not valid.");
            if (prefab == null) throw new NullRE("Collectible prefab is not attached to Level Data Handler.");
            if (value == 0) Debug.LogWarning("Collectibles will not add to score if value is 0.");
            if (collectibleParentObject == null)
            {
                collectibleParentObject = LevelDataHandler.Instance.transform;
                Debug.LogWarning("No parent object set for new collectibles spawned.");
            }

            if (defaultName.IsNullOrWhitespace())
            {
                defaultName = "Collectible";
                Debug.LogWarning("Default name set to 'Collectible'");
            }

            Setup();
        }

        private void Setup()
        {
            activeCollectibles = new List<CollectibleInstance>();

            Spawn(new Vector3(-4, 4, -1));
        }

        public void OnCollision<T>(T instance)
        {
            //check valid type
            if (instance.GetType() != typeof(CollectibleInstance)) return;
            CollectibleInstance castType = instance as CollectibleInstance;

            //remove from list
            RemoveFromActive(castType);

            //destroy object
            GameObject.Destroy(castType.gameObject);

            //get score
            LevelDataHandler.Score += value;
        }

        public void Spawn(Vector3 spawnLocation)
        {
            //spawn
            GameObject newCollectibleObject =
                GameObject.Instantiate(prefab, spawnLocation, Quaternion.identity, collectibleParentObject);
            //naming
            newCollectibleObject.name = defaultName;

            //check has script
            if (!newCollectibleObject.TryGetComponent(out CollectibleInstance collectibleClass)) {
                collectibleClass = newCollectibleObject.AddComponent<CollectibleInstance>();
            }

            //add to list
            if (!CheckList(collectibleClass))
                AddToActive(collectibleClass);
        }

        public bool CheckList(CollectibleInstance collectibleInstance)
        {
            return activeCollectibles.Contains(collectibleInstance);
        }

        public void AddToActive(CollectibleInstance collectibleInstance)
        {
            activeCollectibles.Add(collectibleInstance);
        }

        public void RemoveFromActive(CollectibleInstance collectibleInstance)
        {
            activeCollectibles.Remove(collectibleInstance);
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
            //if (layer == 0 || layer == 119) throw new NullRE("Enemy layer not valid.");
            //if (prefab == null) throw new NullRE("Enemy prefab is not attached to Level Data Handler.");
        }

        public void OnCollision<T>(T instance)
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
            //if (layer == 0 || layer == 119) throw new NullRE("Obstacle layer not valid.");
            //if (prefabs.Length < 1) throw new NullRE("Obstacle prefabs are not attached to Level Data Handler.");
        }

        public void OnCollision<T>(T instance)
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

        public void OnCollision<T>(T instance)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public interface ICollide
    {
        void Validate();
        void OnCollision<T>(T instance);
    }
}