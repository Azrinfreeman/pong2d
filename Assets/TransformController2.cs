using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TransformController2 : MonoBehaviour
{
    //public static TransformController instance;
    public List<string> userCollection;
    public List<string> userIdCollection;
    public Transform childrenContentPrefab;

    private void Awake()
    {
        //  instance = this;
    }

    public Transform contentHeight;

    public int children;

    public List<Transform> listClones;

    public bool isRank;
    public bool isPlay;

    [Header("For Ranking")]
    public RankingList rankings = new RankingList();

    [Header("For Players")]
    public PlayerInput playerInput;

    [System.Serializable]
    public class Ranking
    {
        public string nama;
        public int score;

        public string id_ingame;
    }

    [System.Serializable]
    public class RankingList
    {
        public Ranking[] ranking;
    }

    // Start is called before the first frame update
    void Start()
    {
        children = 0;
        contentHeight = transform;
    }

    // Update is called once per frame
    void Update()
    {
        children = listClones.Count;

        contentHeight.GetComponent<RectTransform>().sizeDelta = new Vector2(
            800,
            150 * listClones.Count
        );
    }

    private void OnDisable()
    {
        if (isRank)
        {
            for (int i = 0; i < listClones.Count; i++)
            {
                //                Debug.Log("wr");
                Destroy(listClones[i].gameObject);
            }
            listClones.Clear();
        }
    }

    private void OnEnable()
    {
        children = 0;
        contentHeight = transform;

        children = listClones.Count;

        contentHeight.GetComponent<RectTransform>().sizeDelta = new Vector2(
            800,
            250 * listClones.Count
        );
        if (!isRank)
        {
            AddPrefab();
        }
        else
        {
            AddPrefabRanking();
        }
    }

    public void AddPrefabRanking()
    {
        StartCoroutine(fetchRanking());
    }

    IEnumerator fetchRanking()
    {
        Debug.Log("fetchRanking");
        userIdCollection.Clear();
        userCollection.Clear();
        using (
            UnityWebRequest www = UnityWebRequest.Get(
                "https://app-hanana.com/pong2d/fetchRanking.php"
            )
        )
        {
            yield return www.SendWebRequest();
            if (
                www.result == UnityWebRequest.Result.ConnectionError
                || www.result == UnityWebRequest.Result.ProtocolError
            )
            {
                Debug.Log(www.error);
                Debug.Log("error server");
            }
            else
            {
                //     Debug.Log(www.downloadHandler.text);
                rankings = JsonUtility.FromJson<RankingList>(www.downloadHandler.text);
                for (int i = 0; i < rankings.ranking.Length; i++)
                {
                    Transform content = Instantiate(
                        childrenContentPrefab,
                        new Vector3(0, 0, 0),
                        Quaternion.identity
                    );
                    content.SetParent(transform);
                    content.localScale = new Vector3(1, 1, 1);
                    content.localPosition = new Vector3(0, 0, 0);

                    listClones.Add(content);
                }
                int l = 1;
                for (int i = 0; i < rankings.ranking.Length; i++)
                {
                    //name
                    //                    Debug.Log(l.ToString() + ". ");

                    //name
                    Debug.Log(rankings.ranking[i].nama);
                    //score
                    //                Debug.Log(rankings.ranking[i].score.ToString());
                    if (
                        rankings.ranking[i].nama.Equals(PlayerPrefs.GetString("CurrentPlayer_"))
                        && rankings
                            .ranking[i]
                            .id_ingame.Equals(PlayerPrefs.GetString("CurrentPlayerid_"))
                    )
                    {
                        listClones[i].transform.GetChild(0).GetComponent<Image>().color =
                            new Color32(144, 0, 255, 255);
                    }
                    listClones[i]
                        .transform.GetChild(0)
                        .transform.GetChild(0)
                        .GetComponent<TextMeshProUGUI>()
                        .text = l.ToString() + ". ";
                    listClones[i]
                        .transform.GetChild(0)
                        .transform.GetChild(1)
                        .GetComponent<TextMeshProUGUI>()
                        .text = rankings.ranking[i].nama;
                    listClones[i]
                        .transform.GetChild(0)
                        .transform.GetChild(2)
                        .GetComponent<TextMeshProUGUI>()
                        .text = rankings.ranking[i].score.ToString();
                    l++;
                }
            }
        }
    }

    public void AddPrefab()
    {
        userIdCollection.Clear();
        userCollection.Clear();
        for (int i = 0; i < 20; i++)
        {
            //Debug.Log(i);
            //Debug.Log(PlayerPrefs.GetString("Player_" + i));

            string str = PlayerPrefs.GetString("Player_" + i);
            if (string.IsNullOrEmpty(str) == true)
            {
                // Debug.Log("null");
            }
            else
            {
                //  Debug.Log("addedd");
                //  Debug.Log(i);

                userCollection.Add("Player_" + i);
                userIdCollection.Add("Playerid_" + i);
                //if not empty add to content
                Transform content = Instantiate(
                    childrenContentPrefab,
                    new Vector3(0, 0, 0),
                    Quaternion.identity
                );
                content.SetParent(transform);
                content.localScale = new Vector3(1, 1, 1);
                content.localPosition = new Vector3(0, 0, 0);
                if (playerInput)
                {
                    if (playerInput.playerInt == 1)
                    {
                        content.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        content.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                int l = i + 1;
                content
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .GetComponent<TextMeshProUGUI>()
                    .text = userCollection.Count.ToString();
                listClones.Add(content);
            }
        }
    }

    public void ClearChildrenAndDismiss()
    {
        for (int i = 0; i < listClones.Count; i++)
        {
            //            Debug.Log("wr");
            Destroy(listClones[i].gameObject);
        }
        listClones.Clear();
        transform
            .parent.transform.parent.transform.parent.GetComponent<Animator>()
            .Play("dismissPlayerSelect");
    }

    public void DismissPlayerSelect()
    {
        for (int i = 0; i < listClones.Count; i++)
        {
            Debug.Log("wr");
            Destroy(listClones[i].gameObject);
        }
        listClones.Clear();
        transform
            .parent.transform.parent.transform.parent.GetComponent<Animator>()
            .Play("dismissPlayerSelect");
    }
}
