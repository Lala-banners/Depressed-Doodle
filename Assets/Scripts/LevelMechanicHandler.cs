using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Steph.Level.Collect;
using UnityEngine;

namespace Steph.Level
{
    public class LevelMechanicHandler : MonoBehaviour
    {
        #region Variables and Properties

        [SerializeField] private Collectibles collectibles;

        #endregion

        private void Awake()
        {
            
        }


        #region My Methods

        //

        #endregion
    }

    namespace Collect
    {
        [Serializable]
        public class Collectibles 
        {
            #region Variables and Properties

            [SerializeField] private LayerMask collectibleLayer;
            [SerializeField] private GameObject prefab;
            [SerializeField,Range(1,10)] private int frequency;

            #endregion



            #region My Methods

            //get collectible

            //

            #endregion
        }
    }
}