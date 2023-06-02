using DG.Tweening;
using UnityEngine;

namespace Arknights
{
    public class CameraManager:MonoBehaviour
    {
        public Camera mainCamera;
        
        public void Init()
        {
            mainCamera = Camera.main;
        }

        public void DoRotation(Vector3 r)
        {
            //dotween移动相机
            mainCamera.transform.DORotate(r, 0.1f);
        }
    }
}