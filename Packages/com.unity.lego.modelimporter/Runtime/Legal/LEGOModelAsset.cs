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
    [RequireComponent(typeof(ParentChecker))]
    public class LEGOModelAsset : LEGOAsset
    {
        ParentChecker parentChecker;

#if UNITY_EDITOR
        public LEGOModelAsset()
        {
            onlyActiveGameObject = false;
            hideChildren = false;

            if (legoAssets.Contains(this)) { return; }
            legoAssets.Add(this);
        }

        void OnDestroy()
        {
            if (!legoAssets.Contains(this)) { return; }
            legoAssets.Remove(this);
        }

        protected override void EditorUpdate()
        {
            base.EditorUpdate();

            if (parentChecker) {
                parentChecker.EditorUpdate();
            }
        }
#endif

        public override void HideInInspector()
        {
            if (!parentChecker)
            {
                parentChecker = GetComponent<ParentChecker>();
            }
            parentChecker.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;

            base.HideInInspector();
        }
    }
}
