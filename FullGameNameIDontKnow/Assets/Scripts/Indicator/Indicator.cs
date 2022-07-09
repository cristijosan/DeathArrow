using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Indicator
{
    public class Indicator : MonoBehaviour
    {
        public float hideDistance; // To ugprade
        public Transform targetObjectPosition;
        
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Transform objectPosition;
        // Local variable
        private RectTransform pointerRectTransform;
        private Rect rect;

        private void Start() {
            pointerRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
            rect = pointerRectTransform.rect;
        }

        private void Update() {
            if (!targetObjectPosition)
                return;

            UpdatePointer();
        }

        private void UpdatePointer() {
            // Collect position's
            var toPos =  new Vector3(targetObjectPosition.position.x, targetObjectPosition.position.z);
            var fromPos = new Vector3(objectPosition.position.x, objectPosition.position.z);
            var indicatorPosition = uiCamera.WorldToScreenPoint(toPos);
            // Calculate angle
            var dir = toPos - fromPos;
            var angle = Utils.GetAngleFromVectorFloat(dir.normalized);
            // Handle if object is behind camera
            if (indicatorPosition.z < 0) {
                indicatorPosition.x = -indicatorPosition.x;
                indicatorPosition.y = -indicatorPosition.y;
            }
            var newPos = new Vector3(indicatorPosition.x, indicatorPosition.y, indicatorPosition.z);
            // Set coordonates to show the object inside screen
            indicatorPosition.x = Mathf.Clamp(indicatorPosition.x, rect.width / 2, Screen.width - rect.width / 2);
            indicatorPosition.y = Mathf.Clamp(indicatorPosition.y, rect.height / 2, Screen.height - rect.height / 2);
            indicatorPosition.z = 0f;
            // Set position and rotation
            pointerRectTransform.position = indicatorPosition;
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
            // Hide || Show indicator
            if (dir.magnitude < hideDistance)
                pointerRectTransform.gameObject.SetActive(false);
            else
                pointerRectTransform.gameObject.SetActive(true);
        }
    }
}