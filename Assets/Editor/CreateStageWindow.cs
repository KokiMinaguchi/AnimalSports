using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using Unity.VisualScripting;
using log4net.Util;

/// <summary>
/// CSV�t�@�C������X�e�[�W�ƃX�e�[�W�p�̓����蔻���
/// �쐬����G�f�B�^�g��
/// </summary>
public class CreateStageWindow : EditorWindow
{
    // �X�e�[�W���܂Ƃ߂�e�I�u�W�F�N�g
    private GameObject _parent;

    [SerializeField]
    [Header("�X�e�[�W�p�l���I�u�W�F�N�g")]
    private GameObject[] _stagePartsPrefab = new GameObject[2];

    [SerializeField]
    [Header("�X�e�[�W�̓����蔻��p�I�u�W�F�N�g")]
    private GameObject[] _stageCollisionPrefab = new GameObject[2];

    private string _filepath;

    // �s�A��
    int _lineLength;
    int _rowLength;
    //����̓�d�z��ɋ�؂�ꂽ�f�[�^���i�[�����[�s][��]�œǂݎ���
    private string[,] _csvDateArray;

    [MenuItem("CreateStage/Window")]
    // �ÓI�ɂ���̂�Y��Ȃ�
    private static void ShowWindow()
    {
        CreateStageWindow window = GetWindow<CreateStageWindow>();
    }

    
    private void OnGUI()
    {
        EditorGUILayout.LabelField("�X�e�[�W�I�u�W�F�N�g�z�u");
        EditorGUILayout.LabelField("�X�e�[�W�t�@�C��");
        EditorGUILayout.LabelField(_filepath);
        
        _stagePartsPrefab[0] = EditorGUILayout.ObjectField(
            "�m�[�}���p�l��", _stagePartsPrefab[0], typeof(GameObject), true) as GameObject;

        _stageCollisionPrefab[0] = EditorGUILayout.ObjectField(
            "�m�[�}���p�l���p�����蔻�胁�b�V��", _stageCollisionPrefab[0], typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("�t�@�C�����J��"))
        {
            _filepath = EditorUtility.OpenFilePanel("aaa", "", "csv");

            if (_filepath == "") return;
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
        // �ǂݍ��񂾃t�@�C�����X�g���[���ɒu��������
        StreamReader sr = new StreamReader(_filepath);

        //�X�g���[����string�ɕς���
        string strStream = sr.ReadToEnd();

        // StringSplitOption��ݒ� "�J���}�ƃJ���}�ɉ����Ȃ�������i�[���Ȃ����Ƃɂ���"
        System.StringSplitOptions options = System.StringSplitOptions.RemoveEmptyEntries;

        //�s�ŕ�����
        string[] line = strStream.Split(new char[] { '\r', '\n' }, options);

        //��؂镶����ݒ�@���̏ꍇ�J���}��؂�Ȃ̂ŃJ���}��ݒ肷��
        char[] spilter = new char[1] { ',' };

        // �s�̐��ݒ�
        _lineLength = line.Length;
        _rowLength = line[0].Split(spilter, options).Length;

        //�f�[�^�i�[�p�z��̏�����
        _csvDateArray = new string[_lineLength, _rowLength];

        for (int i = 0; i < _lineLength; ++i)
        {
            string[] sDate = line[i].Split(spilter, options);
            for (int j = 0; j < _rowLength; ++j)
            {
                _csvDateArray[i, j] = sDate[j];
            }
        }
    }

    /// <summary>
    /// �X�e�[�W�I�u�W�F�N�g�z�u
    /// </summary>
    private void ApplyStageObject()
    {
        //�V�����e�I�u�W�F�N�g�쐬�B����̎q�I�u�W�F�N�g�ɐ���
        _parent = new GameObject("stage");

        GameObject obj;
        BoxCollider rend;
        for (int i = 0; i < _lineLength; i++)
        {
            for (int j = 0; j < _rowLength; j++)
            {
                int caseNum = int.Parse(_csvDateArray[i, j]);
                if (caseNum == -1) { continue; }
                obj = _stagePartsPrefab[caseNum];

                rend = obj.GetComponentInChildren<BoxCollider>();
                //GameObject�Ƃ���Prefab���쐬
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                //Prefab�̊Ԋu���̐ݒ�
                Vector3 offset = new Vector3(rend.size.x * (obj.transform.position.x + i),
                                            rend.size.y * obj.transform.position.y,
                                            rend.size.z * (obj.transform.position.z + j));

                clone.transform.Translate(offset);//�Ԋu�ݒ��K�p
                clone.transform.SetParent(_parent.transform);
            }
        }
    }

    /// <summary>
    /// �X�e�[�W�̓����蔻��p���b�V���z�u
    /// </summary>
    private void ApplyStageCollision()
    {
        //�V�����e�I�u�W�F�N�g�쐬�B����̎q�I�u�W�F�N�g�ɐ���
        _parent = new GameObject("stageCollision");
        _parent.AddComponent<MeshFilter>();

        // �����蔻��̈ʒu�������
        _parent.transform.position = new Vector3(0f, 0.1f, 0f);

        GameObject obj;
        BoxCollider collider;
        for (int i = 0; i < _lineLength; i++)
        {
            for (int j = 0; j < _rowLength; j++)
            {
                int caseNum = int.Parse(_csvDateArray[i, j]);
                if (caseNum == -1) { continue; }
                obj = _stageCollisionPrefab[caseNum];

                collider = obj.GetComponentInChildren<BoxCollider>();
                // GameObject�Ƃ���Prefab���쐬
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                // Prefab�̊Ԋu���̐ݒ�
                Vector3 offset = new Vector3(obj.transform.localScale.x * (obj.transform.position.x + i),
                                             obj.transform.localScale.y *  obj.transform.position.y,
                                             obj.transform.localScale.z * (obj.transform.position.z + j)
                                           );
                //�Ԋu��ݒ�
                clone.transform.Translate(offset);
                clone.transform.SetParent(_parent.transform);
            }
        }
    }

    /// <summary>
    /// �����蔻��p���b�V��������
    /// </summary>
    private void CombineCollisionMesh()
    {
        MeshFilter[] meshFilters = _parent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        // ���b�V��������
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        // �����������b�V����MeshFilter�ɐݒ�
        _parent.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        _parent.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        _parent.gameObject.SetActive(true);

        // MeshCollider��ǉ�
        _parent.AddComponent<MeshCollider>();
    }
}