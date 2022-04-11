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
        [Header("Below this header is not implemented:")]
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

        #region Methods

        private void Awake()
        {
            Instance = this;
            Score = 0;

            ValidateTypes();
        }

        private void ValidateTypes()
        {
            collectibles.Validate();
            enemies.Validate();
            obstacles.Validate();
            fallingArea.Validate();
        }

        #endregion
    }


    [Serializable]
    public class Collectibles : ICollideType
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

            SpawnNew(new Vector3(-4, 4, -1));
        }

        public void OnCollision<T>(T instance)
        {
            //check valid type
            if (instance.GetType() != typeof(CollectibleInstance)) return;
            CollectibleInstance castType = instance as CollectibleInstance;

            //remove from list
            RemoveFromActiveList(castType);

            //destroy object
            GameObject.Destroy(castType.gameObject);

            //get score
            LevelDataHandler.Score += value;
            Debug.Log("Score is: "+LevelDataHandler.Score);
        }

        public void SpawnNew(Vector3 spawnLocation)
        {
            //spawn
            GameObject newCollectibleObject =
                GameObject.Instantiate(prefab, spawnLocation, Quaternion.identity, collectibleParentObject);
            //naming
            newCollectibleObject.name = defaultName;

            //check has script
            if (!newCollectibleObject.TryGetComponent(out CollectibleInstance collectibleClass))
            {
                collectibleClass = newCollectibleObject.AddComponent<CollectibleInstance>();
            }

            //add to list
            if (!CheckActiveListForInstance(collectibleClass))
                AddToActiveList(collectibleClass);
        }

        public bool CheckActiveListForInstance(CollectibleInstance collectibleInstance)
        {
            return activeCollectibles.Contains(collectibleInstance);
        }

        public void AddToActiveList(CollectibleInstance collectibleInstance)
        {
            activeCollectibles.Add(collectibleInstance);
        }

        public void RemoveFromActiveList(CollectibleInstance collectibleInstance)
        {
            activeCollectibles.Remove(collectibleInstance);
        }

        #endregion
    }

    [Serializable]
    public class Enemies : ICollideType
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
    public class Obstacles : ICollideType
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
    public class FallingArea : ICollideType
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

    #region Interfaces and Abstract Classes

    public interface ICollideType
    {
        void Validate();
        void OnCollision<T>(T instance);
    }

    public abstract class CollideInstance : MonoBehaviour
    {
        protected abstract void OnTriggerEnter2D(Collider2D collider);
    }

    #endregion
}