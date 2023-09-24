using UnityEngine;

namespace Surface
{
    [DisallowMultipleComponent]
    public abstract class Surface : MonoBehaviour
    {
        public abstract int GetFMODMaterialIndex();
    }
}
