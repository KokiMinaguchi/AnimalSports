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
        [Header("スコア処理")]
        private ScoreModel _scoreModel;

        [SerializeField]
        [Header("スコアとアイテムごとの得点管理")]
        private GameData _gameData;

        [SerializeField]
        [Header("操作UIのalphaを操作")]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        [Header("プレイヤー操作")]
        private PlayerInputProvider _inputProvider;

        [SerializeField]
        [Header("破壊用木箱オブジェクト")]
        private GameObject _woodenBox_break;

        [SerializeField]
        [Header("爆発エフェクト")]
        private GameObject _explosion;

        [SerializeField]
        [Header("入力無効処理")]
        private InputInvalid _inputInvalid;

        private async void OnCollisionStay(Collision collision)
        {
            // プレイヤーが開ける操作をしたときのみ処理する
            if (collision.gameObject.CompareTag("Player"))
            {
                if(_inputProvider.OpenBox.CurrentValue == true)
                {
                    // アイテムの種類を取得
                    var itemType = this.gameObject.GetComponent<WoodenBox>().Item;
                    // 操作UIの非表示
                    _canvasGroup.alpha = 0.0f;

                    // アイテムの種類に応じてオブジェクト生成、スコア加算する
                    switch (itemType)
                    {
                        // <Instanceするときに木箱の子としてアイテムを生成している理由>
                        // アイテムがシーンなど管理クラスがある常駐シーンに生成されてしまうので
                        // 一旦木箱の子でアイテムを生成して親子関係を解除することで生成場所を指定している。
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
                            // 爆発エフェクト再生
                            explosion.GetComponent<VisualEffect>().Play();
                            explosion.transform.parent = null;
                            // 爆発SE再生
                            SoundManager.Instance.PlaySE(SoundManager.explosion);
                            break;
                    }

                    // 木箱破壊用オブジェクトを生成
                    Instantiate(_woodenBox_break, this.transform.position, Quaternion.identity);
                    // 木箱破壊SE再生
                    SoundManager.Instance.PlaySE(SoundManager.woodClash);

                    // 破壊前の木箱削除
                    Destroy(this.gameObject);
                    
                    if(itemType == ItemInfo.ItemType.Bomb)
                    {
                        // ゲームオーバー。シーン遷移する
                        _inputInvalid.Invalid();
                        // 最終スコアを保存、リザルトシーンで表示させる
                        _gameData.Score = _scoreModel.Score.CurrentValue;
                        // シーン遷移
                        await UniTask.Delay(TimeSpan.FromSeconds(2));
                        SceneTransitionManager.Instance.ChangeScene("ResultScene");
                    }
                }
            }
        }
    }
}
