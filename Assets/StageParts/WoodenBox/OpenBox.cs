using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace SleepingAnimals
{
    public class OpenBox : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _item;

        [SerializeField]
        [Header("�X�R�A����")]
        private ScoreModel _scoreModel;

        [SerializeField]
        [Header("�X�R�A�ƃA�C�e�����Ƃ̓��_�Ǘ�")]
        private GameData _gameData;

        [SerializeField]
        [Header("����UI��alpha�𑀍�")]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        [Header("�v���C���[����")]
        private PlayerInputProvider _inputProvider;

        [SerializeField]
        [Header("�j��p�ؔ��I�u�W�F�N�g")]
        private GameObject _woodenBox_break;

        [SerializeField]
        [Header("�����G�t�F�N�g")]
        private GameObject _explosion;

        [SerializeField]
        [Header("���͖�������")]
        private InputInvalid _inputInvalid;

        private async void OnCollisionStay(Collision collision)
        {
            // �v���C���[���J���鑀��������Ƃ��̂ݏ�������
            if (collision.gameObject.CompareTag("Player"))
            {
                if(_inputProvider.OpenBox.CurrentValue == true)
                {
                    // �A�C�e���̎�ނ��擾
                    var itemType = this.gameObject.GetComponent<WoodenBox>().Item;
                    // ����UI�̔�\��
                    _canvasGroup.alpha = 0.0f;

                    // �A�C�e���̎�ނɉ����ăI�u�W�F�N�g�����A�X�R�A���Z����
                    switch (itemType)
                    {
                        // <Instance����Ƃ��ɖؔ��̎q�Ƃ��ăA�C�e���𐶐����Ă��闝�R>
                        // �A�C�e�����V�[���ȂǊǗ��N���X������풓�V�[���ɐ�������Ă��܂��̂�
                        // ��U�ؔ��̎q�ŃA�C�e���𐶐����Đe�q�֌W���������邱�ƂŐ����ꏊ���w�肵�Ă���B
                        case ItemInfo.ItemType.GoldFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.GoldFish], this.transform.position, Quaternion.identity
                                , this.transform).transform.parent = null;
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.GoldFish]);
                            break;

                        case ItemInfo.ItemType.SilberFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.SilberFish], this.transform.position, Quaternion.identity
                                , this.transform).transform.parent = null;
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.SilberFish]);
                            break;

                        case ItemInfo.ItemType.RedFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.RedFish], this.transform.position, Quaternion.identity
                                , this.transform).transform.parent = null;
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.RedFish]);
                            break;

                        case ItemInfo.ItemType.BlueFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.BlueFish], this.transform.position, Quaternion.identity, 
                                this.transform).transform.parent = null;
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.BlueFish]);
                            break;

                        case ItemInfo.ItemType.Bomb:
                            var explosion = Instantiate(_explosion, this.transform.position, Quaternion.identity,
                                this.transform);
                            // �����G�t�F�N�g�Đ�
                            explosion.GetComponent<VisualEffect>().Play();
                            explosion.transform.parent = null;
                            // ����SE�Đ�
                            SoundManager.Instance.PlaySE(SoundManager.explosion);
                            break;
                    }

                    // �ؔ��j��p�I�u�W�F�N�g�𐶐�
                    Instantiate(_woodenBox_break, this.transform.position, Quaternion.identity);
                    // �ؔ��j��SE�Đ�
                    SoundManager.Instance.PlaySE(SoundManager.woodClash);

                    // �j��O�̖ؔ��폜
                    Destroy(this.gameObject);
                    
                    if(itemType == ItemInfo.ItemType.Bomb)
                    {
                        // �Q�[���I�[�o�[�B�V�[���J�ڂ���
                        _inputInvalid.Invalid();
                        // �ŏI�X�R�A��ۑ��A���U���g�V�[���ŕ\��������
                        _gameData.Score = _scoreModel.Score.CurrentValue;
                        // �V�[���J��
                        await UniTask.Delay(TimeSpan.FromSeconds(2));
                        SceneTransitionManager.Instance.ChangeScene("ResultScene");
                    }
                }
            }
        }
    }
}
