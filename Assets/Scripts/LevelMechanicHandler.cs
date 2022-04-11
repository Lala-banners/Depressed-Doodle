using System.Collections;
using System.Collections.Generic;
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
            collectibles ??= gameObject.GetComponent<Collectibles>();
        }


        #region My Methods

        //

        #endregion
    }
}