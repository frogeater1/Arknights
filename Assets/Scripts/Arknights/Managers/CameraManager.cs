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
            mainCamera!.depthTextureMode = DepthTextureMode.Depth;
        }

        public void DoRotation(Vector3 r)
        {
            //todo: dotween移动相机
            mainCamera.transform.rotation = Quaternion.Euler(r);
        }
    }
}