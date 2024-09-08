using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;



public class BookShelf : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform bookShelfTransform; // �{�I�S�̂�RectTransform
    [SerializeField] RectTransform bookArea1; // �{�̃G���A1
    [SerializeField] RectTransform bookArea2; // �{�̃G���A2
    [SerializeField] GameObject bookPrefab; // �{�̃v���n�u
    [SerializeField] PopupManager popupManager; // PopupManager���Q��
    [SerializeField] RectTransform stockArea; // �{�̃X�g�b�N�G���A
    [SerializeField] RectTransform treatmentArea; // ���ÃG���A
    [SerializeField] OperationManager operationManager; // OperationManager���Q��

    [SerializeField] List<Book> books; // �\������{�̃��X�g

    private Vector2 originalPosition; // �{�I�̌��̈ʒu
    private Vector2 movePosition = new Vector2(-375, 130); // �}�E�X�I�[�o�[���̈ʒu

    void Awake()
    {
        originalPosition = bookShelfTransform.anchoredPosition; // �{�I�̌��̈ʒu��ۑ�
    }

    void Start()
    {
        DisplayBooks();
    }

    public void DisplayBooks()
    {
        foreach (Book book in books)
        {
            RectTransform targetArea = book.isSpecial ? bookArea1 : bookArea2; // �{��u���G���A������
            GameObject bookObject = Instantiate(bookPrefab, targetArea);
            BookPrefab bookPrefabScript = bookObject.GetComponent<BookPrefab>();
            bookPrefabScript.Initialize(book);
            bookObject.GetComponent<Button>().onClick.AddListener(() => popupManager.ShowPopup(book));

            // BookShelf����DragAndDropHandler�ɎQ�Ƃ�n��
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
