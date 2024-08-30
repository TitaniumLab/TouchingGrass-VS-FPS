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
        }


        private void InstanriatePrefab(Vector3 position)
        {
            Instantiate(_prefab, position, Quaternion.identity, _parent).
                AddComponent<IndividualGrassAnimator>().
                Init(position);
        }
    }
}
