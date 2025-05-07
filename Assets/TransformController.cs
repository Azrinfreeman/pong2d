using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransformController : MonoBehaviour
{
    public Transform childrenContentPrefab;
    public Transform contentHeight;

    public int children;
    public List<Transform> listClones;

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
            420 * listClones.Count,
            236
        );
    }

    void OnEnable()
    {
        children = 0;
        contentHeight = transform;

        children = listClones.Count;

        contentHeight.GetComponent<RectTransform>().sizeDelta = new Vector2(
            800,
            250 * listClones.Count
        );

        AddPrefab();
    }

    void OnDisable()
    {
        for (int i = 0; i < listClones.Count; i++)
        {
            //                Debug.Log("wr");
            Destroy(listClones[i].gameObject);
        }
        listClones.Clear();
    }

    private void AddPrefab()
    {
        for (int i = 0; i < FlagController.instance.flagImages.Count; i++)
        {
            //if not empty add to content
            Transform content = Instantiate(
                childrenContentPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            );
            content.SetParent(transform);
            content.localScale = new Vector3(1, 1, 1);
            content.localPosition = new Vector3(0, 0, 0);

            //set image and name
            content.transform.GetChild(0).GetComponent<Image>().sprite = FlagController
                .instance.flagImages[i]
                .GetComponent<Image>()
                .sprite;
            content.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = FlagController
                .instance.flagImages[i]
                .name.ToString();
            listClones.Add(content);
        }
    }
}
