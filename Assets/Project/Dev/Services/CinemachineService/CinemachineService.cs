using Cinemachine;
using UnityEngine;

namespace Project.Dev.Services.CinemachineService
{
    public class CinemachineService : ICinemachineService
    {
        private CinemachineVirtualCamera _virtualCamera;

        public CinemachineService(CinemachineVirtualCamera virtualCamera)
        {
            _virtualCamera = virtualCamera;
        }

        public void HeroCamera(GameObject hero)
        {
            Transform heroSpine = hero.transform
                .Find("GameSkeleton")
                .Find("Hips")
                .Find("Spine");

            if (heroSpine != null)
            {
                _virtualCamera.Follow = heroSpine;
                _virtualCamera.LookAt = hero.transform;
            }
        }
    }
}
