using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;



public class BookShelf : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform bookShelfTransform; // 本棚全体のRectTransform
    [SerializeField] RectTransform bookArea1; // 本のエリア1
    [SerializeField] RectTransform bookArea2; // 本のエリア2
    [SerializeField] GameObject bookPrefab; // 本のプレハブ
    [SerializeField] PopupManager popupManager; // PopupManagerを参照
    [SerializeField] RectTransform stockArea; // 本のストックエリア
    [SerializeField] RectTransform treatmentArea; // 治療エリア
    [SerializeField] OperationManager operationManager; // OperationManagerを参照

    [SerializeField] List<Book> books; // 表示する本のリスト

    private Vector2 originalPosition; // 本棚の元の位置
    private Vector2 movePosition = new Vector2(-375, 130); // マウスオーバー時の位置

    void Awake()
    {
        originalPosition = bookShelfTransform.anchoredPosition; // 本棚の元の位置を保存
    }

    void Start()
    {
        DisplayBooks();
    }

    public void DisplayBooks()
    {
        foreach (Book book in books)
        {
            RectTransform targetArea = book.isSpecial ? bookArea1 : bookArea2; // 本を置くエリアを決定
            GameObject bookObject = Instantiate(bookPrefab, targetArea);
            BookPrefab bookPrefabScript = bookObject.GetComponent<BookPrefab>();
            bookPrefabScript.Initialize(book);
            bookObject.GetComponent<Button>().onClick.AddListener(() => popupManager.ShowPopup(book));

            // BookShelfからDragAndDropHandlerに参照を渡す
            DragAndDropHandler dragHandler = bookObject.GetComponent<DragAndDropHandler>();
            dragHandler.Initialize(popupManager, stockArea, treatmentArea, targetArea, operationManager, book);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        bookShelfTransform.anchoredPosition = movePosition;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        bookShelfTransform.anchoredPosition = originalPosition;
    }
}
