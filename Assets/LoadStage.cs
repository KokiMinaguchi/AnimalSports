using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadStage : MonoBehaviour
{
    GameObject parent;
    int lineLength;//行
    int rowLength;//列
    int count;//設置個数
    private string[,] csvDateArray;//これの二重配列に区切られたデータが格納される[行][列]で読み取れる
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

    private void ReadCsv()//CSV読み取り
    {
        //if (GUILayout.Button("読み込み"))//Editorウィンドウの読み込みボタンが押されたら
        {
            //var path = EditorUtility.OpenFilePanel("aaa", "", "csv");//エクスプローラー開いてファイルのパスを取得
            var path = EditorUtility.OpenFilePanel("aaa", "", "csv");//エクスプローラー開いてファイルのパスを取得

            if (path == "")
            {
                return;
            }
            StreamReader sr = new StreamReader(path);//読み込んだファイルをストリームに置き換える

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
        parent = new GameObject("stage");//新しいラッパー これの子オブジェクトに生成されるぞ

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
                //GameObjectとしてクローンを作成
                GameObject clone = Instantiate(obj, new Vector3(0, 0, 0), obj.transform.rotation) as GameObject;
                //クローンの間隔等の設定
                Vector3 offset = new Vector3(rend.bounds.size.x * obj.transform.position.x + j,
                                            rend.bounds.size.y * obj.transform.position.y - i,
                                            rend.bounds.size.z * obj.transform.position.z);
                clone.transform.Translate(offset);//間隔設定を適用
                clone.transform.SetParent(parent.transform);
                count++;
            }
        }
    }
    //private void Rollback()
    //{
    //    DestroyImmediate(wrapper);//巻き戻し
    //}
    //void OnDestroy()
    //{
    //    if (count == 0) Rollback();
    //}
}
