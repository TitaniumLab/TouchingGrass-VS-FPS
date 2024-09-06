using UnityEngine;
using UnityEngine.EventSystems;

namespace GrassVsFps
{
    public class TouchController : MonoBehaviour
    {
        void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                Debug.Log("LeftClick");
            }
        }
    }
}
