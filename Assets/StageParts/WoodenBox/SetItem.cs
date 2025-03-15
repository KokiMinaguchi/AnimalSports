using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    // 最後に実行されるようにする
    [DefaultExecutionOrder(10)]
    public class SetItem : MonoBehaviour
    {
        [SerializeField]
        const int _itemNum = 5;
        private int[,] _itemList = new int[_itemNum + 2, _itemNum + 2];

        [SerializeField]
        private List<WoodenBox> _boxList;

        // Start is called before the first frame update
        void Start()
        {
            int bombNum = 0;
            float random = 0.0f;
            Debug.Log("START");
            // 爆弾設置
            do
            {
                bombNum = 0;
                // 縦
                for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
                {
                    // 横
                    for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                    {
                        _itemList[i, j] = (int)ItemInfo.ItemType.None;
                        // ５割くらいの確率で爆弾設置
                        random = Random.Range(0.0f, 1.0f);
                        if (random < 0.25f)
                        {
                            _itemList[i, j] = (int)ItemInfo.ItemType.Bomb;
                            bombNum++;
                        }
                    }
                }
                Debug.Log(bombNum);
                // 爆弾の数が５〜７になるまで繰り返す
            } while (!(bombNum >= 5 && bombNum <= 7));

            // 空いているところにアイテム設置
            // 縦
            for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
            {
                // 横
                for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                {
                    // 爆弾が設置されていたらスキップ
                    if (_itemList[i, j] == (int)ItemInfo.ItemType.Bomb)
                        continue;

                    // 配列の要素の周りにある爆弾の数を調べる
                    bombNum = 0;
                    if (_itemList[i + 1, j] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;
                    if (_itemList[i - 1, j] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;
                    if (_itemList[i, j + 1] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;
                    if (_itemList[i, j - 1] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;
                    if (_itemList[i + 1, j + 1] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;
                    if (_itemList[i + 1, j - 1] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;
                    if (_itemList[i - 1, j - 1] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;
                    if (_itemList[i - 1, j + 1] == (int)ItemInfo.ItemType.Bomb)
                        bombNum++;

                    // 周りの爆弾の数によって設置するアイテムを決める
                    if (bombNum > 3)
                    {
                        _itemList[i, j] = (int)ItemInfo.ItemType.GoldFish;
                    }
                    else if (bombNum > 2)
                    {
                        _itemList[i, j] = (int)ItemInfo.ItemType.SilberFish;
                    }
                    else if (bombNum > 1)
                    {
                        _itemList[i, j] = (int)ItemInfo.ItemType.RedFish;
                    }
                    else
                    {
                        _itemList[i, j] = (int)ItemInfo.ItemType.BlueFish;
                    }
                }
            }

            // 箱にアイテムを設定
            int index = 0;
            // 縦
            for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
            {
                // 横
                for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                {
                    _boxList[index++].Item = (ItemInfo.ItemType)_itemList[i, j];
                }
            }
        }
    }
}
