using R3;
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
        private ScoreModel _scoreModel;

        [SerializeField]
        private GameData _gameData;

        [SerializeField]
        CanvasGroup _canvasGroup;

        [SerializeField]
        private PlayerInputProvider _inputProvider;

        [SerializeField]
        private GameObject _woodenBox_break;

        [SerializeField]
        private GameObject _explosion;

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(_inputProvider.OpenBox.CurrentValue == true)
                {
                    var itemType = this.gameObject.GetComponent<WoodenBox>().Item;
                    _canvasGroup.alpha = 0.0f;
                    // アイテムの種類に応じてオブジェクト生成、スコア加算する
                    switch (itemType)
                    {
                        case ItemInfo.ItemType.GoldFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.GoldFish], this.transform.position, Quaternion.identity);
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.GoldFish]);
                            break;

                        case ItemInfo.ItemType.SilberFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.SilberFish], this.transform.position, Quaternion.identity);
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.SilberFish]);
                            break;

                        case ItemInfo.ItemType.RedFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.RedFish], this.transform.position, Quaternion.identity);
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.RedFish]);
                            break;

                        case ItemInfo.ItemType.BlueFish:
                            Instantiate(_item[(int)ItemInfo.ItemType.BlueFish], this.transform.position, Quaternion.identity);
                            _scoreModel.AddScore(_gameData.FishPoint[(int)ItemInfo.ItemType.BlueFish]);
                            break;

                        case ItemInfo.ItemType.Bomb:
                            //Instantiate(_item[(int)ItemInfo.ItemType.Bomb], this.transform.position, Quaternion.identity);

                            // ゲームオーバー。シーン遷移する
                            var explosion = Instantiate(_explosion, this.transform.position, Quaternion.identity);
                            explosion.GetComponent<VisualEffect>().Play();
                            //SceneTransitionManager.Instance.ChangeScene("ResultScene");
                            break;
                    }
                    Instantiate(_woodenBox_break, this.transform.position, Quaternion.identity);
                    // 木箱の位置にアイテムを生成してから木箱削除
                    Destroy(this.gameObject);
                    
                }
            }
        }
    }
}
