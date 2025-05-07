using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignFlag : MonoBehaviour
{
    public PlayerFlagController FlagSelect;

    // Start is called before the first frame update
    void Start()
    {
        FlagSelect =
            transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<PlayerFlagController>();

        if (FlagSelect.FlagPlayer2)
        {
            transform.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, 0f, 0f);
        }
        transform.GetComponent<Button>().onClick.AddListener(() => SetFlag());
    }

    // Update is called once per frame
    void Update() { }

    public void SetFlag()
    {
        if (FlagSelect.PlayerFlag.Equals("Player1"))
        {
            //apply image
            FlagSelect.FlagPlayer1.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                transform.GetChild(0).GetComponent<Image>().sprite;

            //apply name
            FlagSelect
                .FlagPlayer1.GetChild(0)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .GetComponent<TextMeshProUGUI>()
                .text = transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        }
        else if (FlagSelect.PlayerFlag.Equals("Player2"))
        {
            //apply image
            FlagSelect.FlagPlayer2.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                transform.GetChild(0).GetComponent<Image>().sprite;

            //apply name
            FlagSelect
                .FlagPlayer2.GetChild(0)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .GetComponent<TextMeshProUGUI>()
                .text = transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        }
    }
}
