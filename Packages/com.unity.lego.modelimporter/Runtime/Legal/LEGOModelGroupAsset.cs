// Copyright (C) LEGO System A/S - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace LEGOModelImporter
{
    /// <summary>
    /// An asset with this script is considered to be a LEGO Asset
    /// </summary>
    [ExecuteAlways]
    public class LEGOModelGroupAsset : LEGOAsset
    {

#if UNITY_EDITOR
        public LEGOModelGroupAsset()
        {
            hideChildren = false;
            hideSelf = true;

            if (legoAssets.Contains(this)) { return; }
            legoAssets.Add(this);
        }

        void OnDestroy()
        {
            if (!legoAssets.Contains(this)) { return; }
            legoAssets.Remove(this);
        }
#endif
    }
}
