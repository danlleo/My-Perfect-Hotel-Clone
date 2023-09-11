using UnityEngine;

namespace Misc
{
    public static class InputHandler
    {
        public static bool IsMouseButtonDownThisFrame()
            => Input.GetMouseButtonDown(0);

        public static bool IsMouseButtonHeldThisFrame()
            => Input.GetMouseButton(0);

        public static bool IsMouseButtonUpThisFrame()
            => Input.GetMouseButtonUp(0);
    }
}
