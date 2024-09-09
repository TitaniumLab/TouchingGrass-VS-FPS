using UnityEngine;

namespace GrassVsFps
{
    public class Level1 : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;

        private void Start()
        {
            GridTool.CreateGrid(InstanriatePrefab);
            UIController.Instance.SetCountText(GridTool.GetUnitsCount);
        }


        private void InstanriatePrefab(Vector3 position)
        {
            var obj = Instantiate(_prefab, position, Quaternion.identity, _parent);
            obj.AddComponent<IndividualGrassAnimator>().
              Init(position);
            if (TouchController.IsTouching)
            {
                obj.AddComponent<MeshCollider>().
                    convex = true;
            }
        }
    }
}