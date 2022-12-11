using System;
using UnityEngine;

namespace HonestMistake.Utils
{
    public class RotateUpDownAnimation : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float upDownSpeed;
        [SerializeField] private float upDownAmplitude;

        private float originalHeight;
        private Transform thisTransform;

        private void Awake()
        {
            thisTransform = transform;
            originalHeight = thisTransform.position.y;
        }

        private void Update()
        {
            thisTransform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
            var newHeight = originalHeight + Mathf.Sin(Time.time * upDownSpeed) * upDownAmplitude;
            thisTransform.position = new Vector3(thisTransform.position.x, newHeight, thisTransform.position.z);
        }
    }
}