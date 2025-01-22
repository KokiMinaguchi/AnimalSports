using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using Unity.VisualScripting;
using log4net.Util;

public class CreateStageWindow : EditorWindow
{
    GameObject parent;
    int count;//�ݒu��
    [SerializeField]
    private GameObject[] _stagePartsPrefab = new GameObject[2];

    [SerializeField]
    private GameObject[] _stageCollisionPrefab = new GameObject[2];
    //[SerializeField]
    //private StageObject _stageObject;
    private string filepath;

    int lineLength;//�s
    int rowLength;//��
    private string[,] csvDateArray;//����̓�d�z��ɋ�؂�ꂽ�f�[�^���i�[�����[�s][��]�œǂݎ���

    //ReorderableList list;

    [MenuItem("CreateStage/Window")]
    // �ÓI�ɂ���̂�Y��Ȃ�
    private static void ShowWindow()
    {
        CreateStageWindow window = GetWindow<CreateStageWindow>();
    }

    private void Init()
    {
        //list = new(mapChipObj_Prefab, typeof(GameObject), true, true, true, true);
    }
    GameObject obj;
    private void SetMaptipObject()
    {
        
        //mapChipObj_Prefab[0] = _stageObject.field;
        //mapChipObj_Prefab[1] = _stageObject.fish;
        //mapChipObj_Prefab[2] = _stageObject.player;
        //mapChipObj_Prefab[3] = _stageObject.nekokan;
        //mapChipObj_Prefab = new GameObject[4];
         //obj = null;
        //mapChipObj_Prefab[0] = (GameObject)EditorGUILayout.ObjectField("�n��", mapChipObj_Prefab[0], typeof(GameObject), false);
        //mapChipObj_Prefab[1] = (GameObject)EditorGUILayout.ObjectField("�v���C���[", obj, typeof(GameObject), true);
        //mapChipObj_Prefab[2] = (GameObject)EditorGUILayout.ObjectField("��", obj, typeof(GameObject), true);
        //mapChipObj_Prefab[3] = (GameObject)EditorGUILayout.ObjectField("�l�R��", obj, typeof(GameObject), true);
    }

    
    private void OnGUI()
    {
        EditorGUILayout.LabelField("�X�e�[�W�I�u�W�F�N�g�z�u");
        EditorGUILayout.LabelField("�X�e�[�W�t�@�C��");
        EditorGUILayout.LabelField(filepath);
        //list.DoLayoutList();
        //_stagePartsPrefab = new GameObject[2];
        //for (int partsCnt = 0; partsCnt < 2; partsCnt++)
        //{
        //    _stagePartsPrefab[partsCnt] = EditorGUILayout
        //                .ObjectField("Original Prefab", _stagePartsPrefab[partsCnt], typeof(GameObject), true) as GameObject;
        //}
        _stagePartsPrefab[0] = EditorGUILayout.ObjectField(
            "�m�[�}���p�l��", _stagePartsPrefab[0], typeof(GameObject), true) as GameObject;

        _stageCollisionPrefab[0] = EditorGUILayout.ObjectField(
            "�m�[�}���p�l���p�����蔻�胁�b�V��", _stageCollisionPrefab[0], typeof(GameObject), true) as GameObject;

        SetMaptipObject();
        if (GUILayout.Button("�t�@�C�����J��"))
        {
            filepath = EditorUtility.OpenFilePanel("aaa", "", "csv");

            if (filepath == "") return;
        }

        if(GUILayout.Button("�ǂݍ���"))
        {
            ReadCSV();
            ApplyStageObject();
        }

        EditorGUILayout.LabelField("�X�e�[�W�̓����蔻��p���b�V�����쐬���Č���");
        if (GUILayout.Button("�쐬���Ĕz�u"))
        {
            ReadCSV();
            ApplyStageCollision();
            CombineCollisionMesh();
        }
    }

    /// <summary>
    /// CSV�t�@�C���ǂݍ���
    /// </summary>
    private void ReadCSV()
    {
        StreamReader sr = new StreamReader(filepath);//�ǂݍ��񂾃t�@�C�����X�g���[���ɒu��������

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

    /// <summary>
    /// �X�e�[�W�I�u�W�F�N�g�z�u
    /// </summary>
    private void ApplyStageObject()
    {
        parent = new GameObject("stage");//�V�������b�p�[ ����̎q�I�u�W�F�N�g�ɐ�������邼

        GameObject obj;
        BoxCollider rend;
        for (int i = 0; i < lineLength; i++)
        {
            for (int j = 0; j < rowLength; j++)
            {
                int caseNum = int.Parse(csvDateArray[i, j]);
                if (caseNum == -1) { continue; }
                obj = _stagePartsPrefab[caseNum];

                rend = obj.GetComponentInChildren<BoxCollider>();
                Debug.Log(rend.size);
                //GameObject�Ƃ���Prefab���쐬
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                //Prefab�̊Ԋu���̐ݒ�
                Vector3 offset = new Vector3(rend.size.x * (obj.transform.position.x + i),
                                            rend.size.y * obj.transform.position.y,
                                            rend.size.z * (obj.transform.position.z - j));
                clone.transform.Translate(offset);//�Ԋu�ݒ��K�p
                clone.transform.SetParent(parent.transform);
                count++;
            }
        }
    }

    /// <summary>
    /// �X�e�[�W�̓����蔻��p���b�V���z�u
    /// </summary>
    private void ApplyStageCollision()
    {
        parent = new GameObject("stageCollision");//�V�������b�p�[ ����̎q�I�u�W�F�N�g�ɐ�������邼
        parent.AddComponent<MeshFilter>();
        parent.transform.position = new Vector3(0f, 0.1f, 0f);

        GameObject obj;
        BoxCollider rend;
        for (int i = 0; i < lineLength; i++)
        {
            for (int j = 0; j < rowLength; j++)
            {
                int caseNum = int.Parse(csvDateArray[i, j]);
                if (caseNum == -1) { continue; }
                obj = _stageCollisionPrefab[caseNum];

                rend = obj.GetComponentInChildren<BoxCollider>();
                Debug.Log(rend.size);
                //GameObject�Ƃ��ăN���[�����쐬
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                //�N���[���̊Ԋu���̐ݒ�
                Vector3 offset = new Vector3(obj.transform.localScale.x * (obj.transform.position.x + i),
                                            obj.transform.localScale.y * obj.transform.position.y,
                                            obj.transform.localScale.z * (obj.transform.position.z - j));
                clone.transform.Translate(offset);//�Ԋu�ݒ��K�p
                clone.transform.SetParent(parent.transform);
                count++;
            }
        }
    }

    /// <summary>
    /// �����蔻��p���b�V��������
    /// </summary>
    private void CombineCollisionMesh()
    {
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        parent.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        parent.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        parent.gameObject.SetActive(true);

        parent.AddComponent<MeshCollider>().convex = true;
    }

    // �쐬�ł�����X�e�[�W�̎q�I�u�W�F�N�g�ɔz�u��AddComponent�iMeshCollider�j�Ȃǐݒ�
}