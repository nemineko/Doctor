using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class PopupManager : MonoBehaviour
{
    public GameObject popupWindow;   // ポップアップウィンドウのプレハブ
    public TextMeshProUGUI popupText;　　　　　 // ポップアップウィンドウのテキスト
    public List<Button> pageButtons; // ページめくりボタン
    private List<string> pages;      // スクリプタブルオブジェクトのテキスト
    private int currentPageIndex;    // 現在のページ番号

    void Update()
    {
        // 右クリックでポップアップを非表示にする処理
        if (Input.GetMouseButtonDown(1) && popupWindow.activeSelf)
        {
            ClosePopup();
        }
    }
    public void ShowPopup(Book book)
    {
        pages = book.pages;
        currentPageIndex = 0;
        UpdatePopup();
        popupWindow.SetActive(true);
    }

    void UpdatePopup()
    {
        popupText.text = pages[currentPageIndex];
        UpdatePageButtons();
    }

    void UpdatePageButtons()
    {
        // 前のページボタン
        pageButtons[0].gameObject.SetActive(currentPageIndex > 0);
        // 次のページボタン
        pageButtons[1].gameObject.SetActive(currentPageIndex < pages.Count - 1);
    }

    public void NextPage()
    {
        if (currentPageIndex < pages.Count - 1)
        {
            currentPageIndex++;
            UpdatePopup();
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePopup();
        }
    }

    public void ClosePopup()
    {
        popupWindow.SetActive(false);
    }
}