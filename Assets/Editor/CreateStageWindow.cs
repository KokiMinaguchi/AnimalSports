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
    int count;//設置個数
    [SerializeField]
    private GameObject[] _stagePartsPrefab = new GameObject[2];

    [SerializeField]
    private GameObject[] _stageCollisionPrefab = new GameObject[2];
    //[SerializeField]
    //private StageObject _stageObject;
    private string filepath;

    int lineLength;//行
    int rowLength;//列
    private string[,] csvDateArray;//これの二重配列に区切られたデータが格納される[行][列]で読み取れる

    //ReorderableList list;

    [MenuItem("CreateStage/Window")]
    // 静的にするのを忘れない
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
        //mapChipObj_Prefab[0] = (GameObject)EditorGUILayout.ObjectField("地面", mapChipObj_Prefab[0], typeof(GameObject), false);
        //mapChipObj_Prefab[1] = (GameObject)EditorGUILayout.ObjectField("プレイヤー", obj, typeof(GameObject), true);
        //mapChipObj_Prefab[2] = (GameObject)EditorGUILayout.ObjectField("魚", obj, typeof(GameObject), true);
        //mapChipObj_Prefab[3] = (GameObject)EditorGUILayout.ObjectField("ネコ缶", obj, typeof(GameObject), true);
    }

    
    private void OnGUI()
    {
        EditorGUILayout.LabelField("ステージオブジェクト配置");
        EditorGUILayout.LabelField("ステージファイル");
        EditorGUILayout.LabelField(filepath);
        //list.DoLayoutList();
        //_stagePartsPrefab = new GameObject[2];
        //for (int partsCnt = 0; partsCnt < 2; partsCnt++)
        //{
        //    _stagePartsPrefab[partsCnt] = EditorGUILayout
        //                .ObjectField("Original Prefab", _stagePartsPrefab[partsCnt], typeof(GameObject), true) as GameObject;
        //}
        _stagePartsPrefab[0] = EditorGUILayout.ObjectField(
            "ノーマルパネル", _stagePartsPrefab[0], typeof(GameObject), true) as GameObject;

        _stageCollisionPrefab[0] = EditorGUILayout.ObjectField(
            "ノーマルパネル用当たり判定メッシュ", _stageCollisionPrefab[0], typeof(GameObject), true) as GameObject;

        SetMaptipObject();
        if (GUILayout.Button("ファイルを開く"))
        {
            filepath = EditorUtility.OpenFilePanel("aaa", "", "csv");

            if (filepath == "") return;
        }

        if(GUILayout.Button("読み込み"))
        {
            ReadCSV();
            ApplyStageObject();
        }

        EditorGUILayout.LabelField("ステージの当たり判定用メッシュを作成して結合");
        if (GUILayout.Button("作成して配置"))
        {
            ReadCSV();
            ApplyStageCollision();
            CombineCollisionMesh();
        }
    }

    /// <summary>
    /// CSVファイル読み込み
    /// </summary>
    private void ReadCSV()
    {
        StreamReader sr = new StreamReader(filepath);//読み込んだファイルをストリームに置き換える

        string strStream = sr.ReadToEnd();//ストリームをstringに変える

        System.StringSplitOptions options = System.StringSplitOptions.RemoveEmptyEntries; // StringSplitOptionを設定 "カンマとカンマに何もなかったら格納しないことにする"

        string[] line = strStream.Split(new char[] { '\r', '\n' }, options);//行で分ける

        char[] spilter = new char[1] { ',' };//区切る文字を設定　この場合カンマ区切りなのでカンマを設定する

        lineLength = line.Length;// 行の数設定
        rowLength = line[0].Split(spilter, options).Length;

        csvDateArray = new string[lineLength, rowLength];//データ格納用配列の初期化

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
    /// ステージオブジェクト配置
    /// </summary>
    private void ApplyStageObject()
    {
        parent = new GameObject("stage");//新しいラッパー これの子オブジェクトに生成されるぞ

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
                //GameObjectとしてPrefabを作成
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                //Prefabの間隔等の設定
                Vector3 offset = new Vector3(rend.size.x * (obj.transform.position.x + i),
                                            rend.size.y * obj.transform.position.y,
                                            rend.size.z * (obj.transform.position.z - j));
                clone.transform.Translate(offset);//間隔設定を適用
                clone.transform.SetParent(parent.transform);
                count++;
            }
        }
    }

    /// <summary>
    /// ステージの当たり判定用メッシュ配置
    /// </summary>
    private void ApplyStageCollision()
    {
        parent = new GameObject("stageCollision");//新しいラッパー これの子オブジェクトに生成されるぞ
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
                //GameObjectとしてクローンを作成
                GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                //クローンの間隔等の設定
                Vector3 offset = new Vector3(obj.transform.localScale.x * (obj.transform.position.x + i),
                                            obj.transform.localScale.y * obj.transform.position.y,
                                            obj.transform.localScale.z * (obj.transform.position.z - j));
                clone.transform.Translate(offset);//間隔設定を適用
                clone.transform.SetParent(parent.transform);
                count++;
            }
        }
    }

    /// <summary>
    /// 当たり判定用メッシュを結合
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

    // 作成できたらステージの子オブジェクトに配置＆AddComponent（MeshCollider）など設定
}