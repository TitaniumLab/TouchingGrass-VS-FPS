using UnityEngine;

namespace GrassVsFps
{
    public class Level2 : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;
        private Transform[] _transforms;
        private Vector3[] _positions;

        private void Start()
        {
            var count = GridTool.GetUnitsCount;
            _transforms = new Transform[count];
            _positions = new Vector3[count];

            GridTool.CreateGrid((Vector3 pos, int i) =>
            {
                var obj = Instantiate(_prefab, pos, Quaternion.identity, _parent);
                if (TouchController.IsTouching)
                {
                    obj.AddComponent<MeshCollider>().
                        convex = true;
                }
                _transforms[i] = obj.transform;
                _positions[i] = pos;
            });

            UIController.Instance.CurrentHUD.SetCountVolue = count.ToString();
            UIController.Instance.CurrentHUD.SetTouchTypeText = "Add to List by tag \n(bad practice)";
            UIController.Instance.CurrentHUD.SetSceneName = "Grass manager \ncached position";
        }


        private void Update()
        {
            var time = Time.time;
            for (int i = 0; i < _transforms.Length; i++)
            {
                var rot = _positions[i].GetNoiseRotation(time);
                _transforms[i].rotation = rot;
            }
        }
    }
}
