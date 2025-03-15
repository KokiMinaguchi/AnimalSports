using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    // ç≈å„Ç…é¿çsÇ≥ÇÍÇÈÇÊÇ§Ç…Ç∑ÇÈ
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
            // îöíeê›íu
            do
            {
                bombNum = 0;
                // èc
                for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
                {
                    // â°
                    for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                    {
                        _itemList[i, j] = (int)ItemInfo.ItemType.None;
                        // ÇTäÑÇ≠ÇÁÇ¢ÇÃämó¶Ç≈îöíeê›íu
                        random = Random.Range(0.0f, 1.0f);
                        if (random < 0.25f)
                        {
                            _itemList[i, j] = (int)ItemInfo.ItemType.Bomb;
                            bombNum++;
                        }
                    }
                }
                Debug.Log(bombNum);
                // îöíeÇÃêîÇ™ÇTÅ`ÇVÇ…Ç»ÇÈÇ‹Ç≈åJÇËï‘Ç∑
            } while (!(bombNum >= 5 && bombNum <= 7));

            // ãÛÇ¢ÇƒÇ¢ÇÈÇ∆Ç±ÇÎÇ…ÉAÉCÉeÉÄê›íu
            // èc
            for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
            {
                // â°
                for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                {
                    // îöíeÇ™ê›íuÇ≥ÇÍÇƒÇ¢ÇΩÇÁÉXÉLÉbÉv
                    if (_itemList[i, j] == (int)ItemInfo.ItemType.Bomb)
                        continue;

                    // îzóÒÇÃóvëfÇÃé¸ÇËÇ…Ç†ÇÈîöíeÇÃêîÇí≤Ç◊ÇÈ
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

                    // é¸ÇËÇÃîöíeÇÃêîÇ…ÇÊÇ¡Çƒê›íuÇ∑ÇÈÉAÉCÉeÉÄÇåàÇﬂÇÈ
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

            // î†Ç…ÉAÉCÉeÉÄÇê›íË
            int index = 0;
            // èc
            for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
            {
                // â°
                for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                {
                    _boxList[index++].Item = (ItemInfo.ItemType)_itemList[i, j];
                }
            }
        }
    }
}
