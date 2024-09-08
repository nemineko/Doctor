using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OperationManager : MonoBehaviour
{
    [SerializeField] GameObject parts; // partsオブジェクト
    [SerializeField] TMP_Dropdown dropdown; // parts内のDropdown

    public void ShowParts(Book theBook)
    {
        // partsを表示
        parts.SetActive(true);

        // Dropdownのoptionsにtreatmentリストを追加
        dropdown.options.Clear();
        foreach (string treatment in theBook.treatment)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(treatment));
        }
    }

    public void OnYesButtonClick()
    {
        // シナリオを進行させる処理
        Debug.Log("シナリオが進行します");
        
    }

    public void OnNoButtonClick()
    {
        // 何も起こらない処理
        Debug.Log("何も起こりません");
    }
}
