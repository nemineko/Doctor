using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class BookShelf : MonoBehaviour
{
    public Transform bookShelfTransform;
    public GameObject bookPrefab;
    public List<Book> books;
    public PopupManager popupManager; // ポップアップマネージャーを参照

    void Start()
    {
        DisplayBooks();
    }

    public void DisplayBooks()
    {
        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(bookPrefab, bookShelfTransform);
            BookPrefab bookPrefabScript = bookObject.GetComponent<BookPrefab>();
            bookPrefabScript.Initialize(book);
            bookObject.GetComponent<Button>().onClick.AddListener(() => popupManager.ShowPopup(book)); // クリックイベントを追加
        }
    }

}
