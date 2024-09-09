using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Vector2 dragOffset;

    private RectTransform bookshelfArea; // �{�I�G���A
    private RectTransform stockArea; // �{�̃X�g�b�N�G���A
    private RectTransform treatmentArea; // ���ÃG���A
    private PopupManager popupManager; // �|�b�v�A�b�v�}�l�[�W���[
    private OperationManager operationManager; // �v���C���[����G���A
    private Book book; // �I�����ꂽ�{

    // BookShelf����DragAndDropHandler�ɎQ�Ƃ�n��
    public void Initialize(PopupManager popupManager, RectTransform stockArea, RectTransform treatmentArea,
        RectTransform bookshelfArea, OperationManager operation, Book theBook)
    {
        this.popupManager = popupManager;
        this.stockArea = stockArea;
        this.treatmentArea = treatmentArea;
        this.bookshelfArea = bookshelfArea;
        this.operationManager = operation;
        this.book = theBook;
        print("book : " + book);
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�J�n���̏���
        canvasGroup.blocksRaycasts = false;

        // �|�b�v�A�b�v�E�B���h�E�����
        popupManager.ClosePopup();

        // ���݂̐e���擾
        RectTransform currentParent = rectTransform.parent as RectTransform;

        // �}�E�X�̈ʒu�Ɩ{�̈ʒu�̃I�t�Z�b�g���v�Z
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(currentParent, eventData.position, eventData.pressEventCamera, out localPoint);
        dragOffset = rectTransform.anchoredPosition - localPoint;

        // �{����O�ɕ\��  
        rectTransform.SetParent(bookshelfArea, true);
        rectTransform.SetAsLastSibling(); // �{���őO�ʂɈړ�
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �h���b�O���̏���
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bookshelfArea, eventData.position, eventData.pressEventCamera, out localPoint);
        rectTransform.anchoredPosition = localPoint + dragOffset;

        // �h���b�v�ł���G���A�Ɩ{���d�Ȃ����Ƃ������ɁA�{��1.5�{�傫���\��������
        if (RectTransformUtility.RectangleContainsScreenPoint(stockArea, Input.mousePosition) ||
            RectTransformUtility.RectangleContainsScreenPoint(treatmentArea, Input.mousePosition) ||
            RectTransformUtility.RectangleContainsScreenPoint(bookshelfArea, Input.mousePosition))
        {
            rectTransform.localScale = Vector3.one * 1.5f;
        }
        else
        {
            rectTransform.localScale = Vector3.one;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �h���b�O�I�����̏���
        canvasGroup.blocksRaycasts = true;

        if (RectTransformUtility.RectangleContainsScreenPoint(stockArea, Input.mousePosition))
        {
            ChangeParent(stockArea);
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(treatmentArea, Input.mousePosition))
        {
            ChangeParent(stockArea);
            operationManager.ShowParts(book);
        }
        else
        {
            // �h���b�v�ł���G���A�ȊO�Ƀh���b�v���ꂽ�ꍇ�A���̈ʒu�ɖ߂�
            rectTransform.anchoredPosition = originalPosition;

            // �e�����ɖ߂�
            ChangeParent(bookshelfArea);
        }
    }

    private void ChangeParent(RectTransform area)
    {
        // �e��ύX
        rectTransform.SetParent(area, false);

        // �e�ɔz�u
        int bookID = book.bookID;
        int bookShelf = book.isSpecial ? 0 : 1;
        float bookWidth = rectTransform.rect.width;
        float bookHeight = rectTransform.rect.height;
        print("bookWidth : " + bookWidth);
        print("bookHeight : " + bookHeight);
        float bookX = bookWidth * bookID;
        float bookY = bookHeight * bookShelf;
        if(area == bookshelfArea)
        {
            // �傫�������ɖ߂�
            rectTransform.localScale = Vector3.one;

            rectTransform.anchoredPosition = new Vector2(bookX, originalPosition.y);
        }
        else
        {
            // ����������
            rectTransform.localScale = Vector3.one * 0.5f;
            rectTransform.anchoredPosition = new Vector2(bookX, bookY);

        }
    }
}
