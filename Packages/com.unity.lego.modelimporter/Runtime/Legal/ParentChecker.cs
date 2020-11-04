// Copyright (C) LEGO System A/S - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited

using UnityEngine;
using UnityEditor;

namespace LEGOModelImporter
{
    /// <summary>
    /// Ensures that an object is not under a scaled parent or a LEGO Model Asset.
    /// </summary>
    [HideInInspector]
    [ExecuteAlways]
    public class ParentChecker : MonoBehaviour
    {
        public void EditorUpdate()
        {
            EnsureNotChild();
        }

#if !UNITY_EDITOR
        void Update()
        {
            EnsureNotChild();
        }

#endif

        void EnsureNotChild()
        {
            if (transform.parent == null)
            {
                return;
            }

            var parent = transform.parent;
            var scaledParent = false;
            var modelParent = false;
            while(parent != null)
            {
                scaledParent = Vector3.Distance(parent.localScale, Vector3.one) >= Mathf.Epsilon;

                if (scaledParent)
                {
                    parent.localScale = Vector3.one;
                }

                modelParent = parent.GetComponent<LEGOModelAsset>();
                if (modelParent)
                {
                    break;
                }

                parent = parent.parent;
            }

            if (scaledParent)
            {
                Debug.LogError("LEGO Model assets cannot be children of scaled game objects");
            }
            if (modelParent)
            {
                Debug.LogError("LEGO Model assets cannot be children of other LEGO Model assets");
            }

            if (modelParent)
            {
#if UNITY_EDITOR
                Undo.SetTransformParent(transform, parent.parent, "Nested LEGO Model");
#else
                transform.parent = parent.parent;
#endif
            }
        }
    }
}
