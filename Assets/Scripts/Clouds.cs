using UnityEngine;
using System.Linq;

namespace HonestMistake.Utility
{
    public class Clouds : MonoBehaviour
    {
        [SerializeField] private Vector3 _axis = Vector3.up;
        [SerializeField] private float _minSpeed = 1.0f;
        [SerializeField] private float _maxSpeed = 5.0f;

        private float _minDistance, _maxDistance;

        private void Awake()
        {
            _minDistance = transform.Cast<Transform>().Select(t => t.localPosition.magnitude).Min();
            _maxDistance = transform.Cast<Transform>().Select(t => t.localPosition.magnitude).Max();
        }

        private void Update()
        {
            if (_axis.sqrMagnitude <= 0)
                return;

            foreach (Transform child in transform)
            {
                float t = Mathf.InverseLerp(_minDistance, _maxDistance, child.localPosition.magnitude);
                float speed = Mathf.Lerp(_maxSpeed, _minSpeed, t);
                child.RotateAround(transform.position, _axis, Time.deltaTime * speed);
                child.LookAt(transform.position);
            }
        }
    }
}