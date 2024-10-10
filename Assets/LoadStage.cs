using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadStage : MonoBehaviour
{
    GameObject parent;
    int lineLength;//�s
    int rowLength;//��
    int count;//�ݒu��
    private string[,] csvDateArray;//����̓�d�z��ɋ�؂�ꂽ�f�[�^���i�[�����[�s][��]�œǂݎ���
    [SerializeField]
    GameObject[] mapChipObj_Prefab;

    // Start is called before the first frame update
    void Start()
    {
        ReadCsv();

        Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadCsv()//CSV�ǂݎ��
    {
        //if (GUILayout.Button("�ǂݍ���"))//Editor�E�B���h�E�̓ǂݍ��݃{�^���������ꂽ��
        {
            //var path = EditorUtility.OpenFilePanel("aaa", "", "csv");//�G�N�X�v���[���[�J���ăt�@�C���̃p�X���擾
            var path = EditorUtility.OpenFilePanel("aaa", "", "csv");//�G�N�X�v���[���[�J���ăt�@�C���̃p�X���擾

            if (path == "")
            {
                return;
            }
            StreamReader sr = new StreamReader(path);//�ǂݍ��񂾃t�@�C�����X�g���[���ɒu��������

            string strStream = sr.ReadToEnd();//�X�g���[����string�ɕς���

            System.StringSplitOptions options = System.StringSplitOptions.RemoveEmptyEntries; // StringSplitOption��ݒ� "�J���}�ƃJ���}�ɉ����Ȃ�������i�[���Ȃ����Ƃɂ���"

            string[] line = strStream.Split(new char[] { '\r', '\n' }, options);//�s�ŕ�����

            char[] spilter = new char[1] { ',' };//��؂镶����ݒ�@���̏ꍇ�J���}��؂�Ȃ̂ŃJ���}��ݒ肷��

            lineLength = line.Length;// �s�̐��ݒ�
            rowLength = line[0].Split(spilter, options).Length;

            csvDateArray = new string[lineLength, rowLength];//�f�[�^�i�[�p�z��̏�����

            for (int i = 0; i < lineLength; i++)
            {
                string[] sDate = line[i].Split(spilter, options);
                for (int j = 0; j < rowLength; j++)
                {
                    csvDateArray[i, j] = sDate[j];
                }
            }
            Debug.Log(lineLength);
            Debug.Log(rowLength);
        }
    }
    //private void objectlist()
    //{
    //    mapChipObj[0] = mapchip.field;
    //    mapChipObj[1] = mapchip.forest;
    //    mapChipObj[2] = mapchip.water;
    //    mapChipObj[3] = mapchip.wilderness;
    //    mapChipObj[4] = mapchip.mountain;
    //    mapChipObj[5] = mapchip.dontEnter;
    //    mapChipObj[6] = mapchip.pBase;
    //    mapChipObj[7] = mapchip.pMainBase;
    //    mapChipObj[8] = mapchip.eBase;

    //    for (int i = 0; i < rends.Length; i++)
    //    {
    //        rends[i] = mapChipObj[i].GetComponentInChildren<Renderer>();
    //    }
    //}
    private void Apply()
    {
        parent = new GameObject("stage");//�V�������b�p�[ ����̎q�I�u�W�F�N�g�ɐ�������邼

        GameObject obj;
        Renderer rend;
        for (int i = 0; i < lineLength; i++)
        {
            for (int j = 0; j < rowLength; j++)
            {
                int caseNum = int.Parse(csvDateArray[i, j]);
                if (caseNum == -1) { continue; }
                obj = mapChipObj_Prefab[caseNum];
                
                rend = obj.GetComponentInChildren<Renderer>();
                //GameObject�Ƃ��ăN���[�����쐬
                GameObject clone = Instantiate(obj, new Vector3(0, 0, 0), obj.transform.rotation) as GameObject;
                //�N���[���̊Ԋu���̐ݒ�
                Vector3 offset = new Vector3(rend.bounds.size.x * obj.transform.position.x + j,
                                            rend.bounds.size.y * obj.transform.position.y - i,
                                            rend.bounds.size.z * obj.transform.position.z);
                clone.transform.Translate(offset);//�Ԋu�ݒ��K�p
                clone.transform.SetParent(parent.transform);
                count++;
            }
        }
    }
    //private void Rollback()
    //{
    //    DestroyImmediate(wrapper);//�����߂�
    //}
    //void OnDestroy()
    //{
    //    if (count == 0) Rollback();
    //}
}
