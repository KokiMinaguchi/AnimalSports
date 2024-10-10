using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using Unity.VisualScripting;

public class CreateStageWindow : EditorWindow
{
    GameObject parent;
    int count;//�ݒu��
    [SerializeField]
    GameObject[] mapChipObj_Prefab;

    //[SerializeField]
    private StageObject _stageObject;
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
        
        mapChipObj_Prefab[0] = _stageObject.field;
        mapChipObj_Prefab[1] = _stageObject.fish;
        mapChipObj_Prefab[2] = _stageObject.player;
        mapChipObj_Prefab[3] = _stageObject.nekokan;
        //mapChipObj_Prefab = new GameObject[4];
         //obj = null;
        //mapChipObj_Prefab[0] = (GameObject)EditorGUILayout.ObjectField("�n��", mapChipObj_Prefab[0], typeof(GameObject), false);
        //mapChipObj_Prefab[1] = (GameObject)EditorGUILayout.ObjectField("�v���C���[", obj, typeof(GameObject), true);
        //mapChipObj_Prefab[2] = (GameObject)EditorGUILayout.ObjectField("��", obj, typeof(GameObject), true);
        //mapChipObj_Prefab[3] = (GameObject)EditorGUILayout.ObjectField("�l�R��", obj, typeof(GameObject), true);
    }

    
    private void OnGUI()
    {

        EditorGUILayout.LabelField("�X�e�[�W�t�@�C��");
        EditorGUILayout.LabelField(filepath);
        //list.DoLayoutList();
        //EditorGUILayout.ObjectField("�n��", mapChipObj_Prefab[0].gameObject, typeof(GameObject), true);
        mapChipObj_Prefab = new GameObject[4];
        SetMaptipObject();
        if (GUILayout.Button("�t�@�C�����J��"))
        {
            filepath = EditorUtility.OpenFilePanel("aaa", "", "csv");

            if (filepath == "") return;

            
        }

        if(GUILayout.Button("�ǂݍ���"))
        {
            ReadCSV();


        }
    }

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
}


//public class StageCreate : EditorWindow
//{
//    MapchipObjectSet mapchip;
//    int lineLength;//�s
//    int rowLength;//��
//    int count;//�ݒu��
//    GameObject[] mapChipObj;
//    private string[,] csvDateArray;//����̓�d�z��ɋ�؂�ꂽ�f�[�^���i�[�����[�s][��]�œǂݎ���         
//    ReorderableList obj;
//    bool node = false;

//    GameObject wrapper;// �����̂��߂̃��b�p�[�I�u�W�F�N�g ����̎q�I�u�W�F�N�g�Ƃ��Đ��������
//    Renderer rend;
//    Renderer[] rends;
//    [UnityEditor.MenuItem("Tools/Stagecreate ")]

//    static void Init()
//    {
//        Create window = (Create)GetWindow(typeof(Create), true, "Duplicate Objects");//�V�����E�B���h�E���
//        window.Show();
//    }
//    private void OnGUI()
//    {
//        EditorGUI.BeginChangeCheck();
//        ReadCsv();
//        mapChipObj = new GameObject[9];
//        rends = new Renderer[9];
//        EditorGUILayout.LabelField("�ǂݎ����CSV�̍s" + rowLength + "  ��" + lineLength);
//        EditorGUILayout.BeginVertical(GUI.skin.box);
//        mapchip = (MapchipObjectSet)EditorGUILayout.ObjectField("�}�b�v�`�b�v�Z�b�g�X�N�I�u", mapchip, typeof(MapchipObjectSet), true);

//        EditorGUILayout.EndVertical();
//        if (mapchip == null) return;
//        objectlist();

//        if (EditorGUI.EndChangeCheck())// �ȑO�̕ύX���b�p�[��j�����܂��E�V�������̂��쐬����O�Ƀ`�F�b�N
//        {
//            Rollback();
//            Apply();
//        }
//        EditorGUILayout.BeginHorizontal(GUI.skin.box);

//        GUIStyle nodeStyle1 = node ? (GUIStyle)"flow node hex 2 on" : (GUIStyle)"flow node hex 2";
//        GUIStyle nodeStyle2 = node ? (GUIStyle)"flow node hex 6 on" : (GUIStyle)"flow node hex 6";
//        if (GUILayout.Button("�@ ���� �@", nodeStyle1)) Close();
//        if (GUILayout.Button("�L�����Z��", nodeStyle2))//�����߂��ďI��
//        {
//            Rollback();
//            Close();
//        }
//        EditorGUILayout.EndHorizontal();
//    }
//    private void ReadCsv()//CSV�ǂݎ��
//    {
//        if (GUILayout.Button("�ǂݍ���"))//Editor�E�B���h�E�̓ǂݍ��݃{�^���������ꂽ��
//        {
//            var path = EditorUtility.OpenFilePanel("aaa", "", "csv");//�G�N�X�v���[���[�J���ăt�@�C���̃p�X���擾

//            if (path == "")
//            {
//                return;
//            }
//            StreamReader sr = new StreamReader(path);//�ǂݍ��񂾃t�@�C�����X�g���[���ɒu��������

//            string strStream = sr.ReadToEnd();//�X�g���[����string�ɕς���

//            System.StringSplitOptions options = System.StringSplitOptions.RemoveEmptyEntries; // StringSplitOption��ݒ� "�J���}�ƃJ���}�ɉ����Ȃ�������i�[���Ȃ����Ƃɂ���"

//            string[] line = strStream.Split(new char[] { '\r', '\n' }, options);//�s�ŕ�����

//            char[] spilter = new char[1] { ',' };//��؂镶����ݒ�@���̏ꍇ�J���}��؂�Ȃ̂ŃJ���}��ݒ肷��

//            lineLength = line.Length;// �s�̐��ݒ�
//            rowLength = line[0].Split(spilter, options).Length;

//            csvDateArray = new string[lineLength, rowLength];//�f�[�^�i�[�p�z��̏�����

//            for (int i = 0; i < lineLength; i++)
//            {
//                string[] sDate = line[i].Split(spilter, options);
//                for (int j = 0; j < rowLength; j++)
//                {
//                    csvDateArray[i, j] = sDate[j];
//                }
//            }
//            Debug.Log(lineLength);
//            Debug.Log(rowLength);
//        }
//    }
//    private void objectlist()
//    {
//        mapChipObj[0] = mapchip.field;
//        mapChipObj[1] = mapchip.forest;
//        mapChipObj[2] = mapchip.water;
//        mapChipObj[3] = mapchip.wilderness;
//        mapChipObj[4] = mapchip.mountain;
//        mapChipObj[5] = mapchip.dontEnter;
//        mapChipObj[6] = mapchip.pBase;
//        mapChipObj[7] = mapchip.pMainBase;
//        mapChipObj[8] = mapchip.eBase;

//        for (int i = 0; i < rends.Length; i++)
//        {
//            rends[i] = mapChipObj[i].GetComponentInChildren<Renderer>();
//        }
//    }
//    private void Apply()
//    {
//        wrapper = new GameObject("stage");//�V�������b�p�[ ����̎q�I�u�W�F�N�g�ɐ�������邼

//        GameObject obj;
//        Renderer rend;
//        for (int i = 0; i <= lineLength; i++)
//        {
//            for (int j = 0; j < rowLength; j++)
//            {
//                int caseNum = int.Parse(csvDateArray[i, j]);
//                obj = mapChipObj[caseNum];
//                rend = obj.GetComponentInChildren<Renderer>();
//                //GameObject�Ƃ��ăN���[�����쐬
//                GameObject clone = Instantiate(obj, new Vector3(0, 0, 0), obj.transform.rotation) as GameObject;
//                //�N���[���̊Ԋu���̐ݒ�
//                Vector3 offset = new Vector3(rend.bounds.size.x * obj.transform.position.x + j,
//                                            rend.bounds.size.y * obj.transform.position.y,
//                                            rend.bounds.size.z * obj.transform.position.z + i)
//                                            ;
//                clone.transform.Translate(offset);//�Ԋu�ݒ��K�p
//                clone.transform.SetParent(wrapper.transform);
//                count++;
//            }
//        }
//    }
//    private void Rollback()
//    {
//        DestroyImmediate(wrapper);//�����߂�
//    }
//    void OnDestroy()
//    {
//        if (count == 0) Rollback();
//    }
//}