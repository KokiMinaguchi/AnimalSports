using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    // �Ō�Ɏ��s�����悤�ɂ���
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
            // ���e�ݒu
            do
            {
                bombNum = 0;
                // �c
                for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
                {
                    // ��
                    for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                    {
                        _itemList[i, j] = (int)ItemInfo.ItemType.None;
                        // �T�����炢�̊m���Ŕ��e�ݒu
                        random = Random.Range(0.0f, 1.0f);
                        if (random < 0.25f)
                        {
                            _itemList[i, j] = (int)ItemInfo.ItemType.Bomb;
                            bombNum++;
                        }
                    }
                }
                Debug.Log(bombNum);
                // ���e�̐����T�`�V�ɂȂ�܂ŌJ��Ԃ�
            } while (!(bombNum >= 5 && bombNum <= 7));

            // �󂢂Ă���Ƃ���ɃA�C�e���ݒu
            // �c
            for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
            {
                // ��
                for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                {
                    // ���e���ݒu����Ă�����X�L�b�v
                    if (_itemList[i, j] == (int)ItemInfo.ItemType.Bomb)
                        continue;

                    // �z��̗v�f�̎���ɂ��锚�e�̐��𒲂ׂ�
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

                    // ����̔��e�̐��ɂ���Đݒu����A�C�e�������߂�
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

            // ���ɃA�C�e����ݒ�
            int index = 0;
            // �c
            for (int i = 1; i < _itemList.GetLength(0) - 1; ++i)
            {
                // ��
                for (int j = 1; j < _itemList.GetLength(1) - 1; ++j)
                {
                    _boxList[index++].Item = (ItemInfo.ItemType)_itemList[i, j];
                }
            }
        }
    }
}
