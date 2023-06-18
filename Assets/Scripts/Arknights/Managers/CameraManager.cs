using DG.Tweening;
using UnityEngine;

namespace Arknights
{
    public class CameraManager:MonoBehaviour
    {
        public Camera mainCamera;
        public GameObject hpSpSliderRoot;
        
        public void Init()
        {
            mainCamera = Camera.main;
            mainCamera!.depthTextureMode = DepthTextureMode.Depth;
        }

        public void DoRotation(Vector3 r)
        {
            //todo: dotween移动相机
            mainCamera.transform.rotation = Quaternion.Euler(r);
            
            //修正一下血条的位置
            var pos = new Vector3(r.z < 0 ? 6 : 0, r.z < 0 ? 10 : 0, 0);
            hpSpSliderRoot.transform.localPosition = pos;
        }
    }
}