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

        }
    }
}
