using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Example
{
    public class TransformdTest : MonoBehaviour
    {
        public Transformd Transform;

        private void Start()
        {
            Transform = Transformd.Identity;
            //Transform.Rotate(Vector3d.Forward, 1d);
            //Transform.Rotate(Vector3d.Right, 1d);

            //transform.Rotate(Vector3.forward, 1f);
            //transform.Rotate(Vector3.right, 1f);
        }

        private void Update()
        {
            //_t.Rotate(Vector3d.Forward, 25d * Time.deltaTime);

            //transform.localPosition = Transform.LocalPosition.Vector3();
            //transform.localRotation = Transform.LocalRotation.Quaternion();
            transform.localEulerAngles = Transform.LocalRotation.EulerAngles.Vector3();
            //transform.localScale = Transform.LocalScale.Vector3();
        }
    }
} 