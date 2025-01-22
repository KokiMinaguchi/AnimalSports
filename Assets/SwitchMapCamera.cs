using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Cinemachine;

namespace SleepingAnimals
{
    [RequireComponent(typeof(PlayerInputProvider))]
    public class SwitchMapCamera : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCameraBase _playerCamera;

        [SerializeField]
        private CinemachineVirtualCameraBase _mapCamera;

        private PlayerInputProvider _inputProvider;

        // Start is called before the first frame update
        void Start()
        {
            _inputProvider = GetComponent<PlayerInputProvider>();
            // Mƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚ç
            // Camera—Dæ“x‚Ì“ü‚ê‘Ö‚¦
            _inputProvider.OpenMap
                .Where(value => value == true)
                .Subscribe(_ =>
                {
                    int tmp;
                    tmp = _playerCamera.Priority;
                    _playerCamera.Priority = _mapCamera.Priority;
                    _mapCamera.Priority = tmp;
                })
                .AddTo(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
