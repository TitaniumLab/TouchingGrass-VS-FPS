using UnityEngine;

namespace GrassVsFps
{
    public class Level1 : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;

        private void Start()
        {
            GridTool.CreateGrid((Vector3 position, int i) =>
            {
                var obj = Instantiate(_prefab, position, Quaternion.identity, _parent);
                obj.AddComponent<IndividualGrassAnimator>().
                  Init(position);
                if (TouchController.IsTouching)
                {
                    obj.AddComponent<MeshCollider>().
                        convex = true;
                }
            });

            UIController.Instance.CurrentHUD.SetCountVolue = GridTool.GetUnitsCount.ToString();
            UIController.Instance.CurrentHUD.SetTouchTypeText = "Trigger + Interface";
            UIController.Instance.CurrentHUD.SetSceneName = "Individual scripts \ncached position";
        }
    }
}