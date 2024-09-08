using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Vector2 dragOffset;

    private RectTransform bookShelfTransform; // �{�I
    private RectTransform stockArea; // �{�̃X�g�b�N�G���A
    private RectTransform treatmentArea; // ���ÃG���A
    private PopupManager popupManager; // �|�b�v�A�b�v�}�l�[�W���[
    private OperationManager operationManager; // �v���C���[����G���A
    private Book book; // �I�����ꂽ�{

    // BookShelf����DragAndDropHandler�ɎQ�Ƃ�n��
    public void Initialize(PopupManager popupManager, RectTransform stockArea, RectTransform treatmentArea,
        RectTransform bookShelfTransform, OperationManager operation, Book theBook)
    {
        this.popupManager = popupManager;
        this.stockArea = stockArea;
        this.treatmentArea = treatmentArea;
        this.bookShelfTransform = bookShelfTransform;
        this.operationManager = operation;
        this.book = theBook;
        print("book : " + book);
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�J�n���̏���
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;

        // �|�b�v�A�b�v�E�B���h�E�����
        popupManager.ClosePopup();

        // �{����O�ɕ\��  
        rectTransform.SetParent(bookShelfTransform, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        // �h���b�O���̏���
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bookShelfTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        rectTransform.anchoredPosition = localPoint + dragOffset;


        // �h���b�v�ł���G���A�Ɩ{���d�Ȃ����Ƃ������ɁA�{��1.5�{�傫���\��������
        if (RectTransformUtility.RectangleContainsScreenPoint(stockArea, Input.mousePosition) ||
            RectTransformUtility.RectangleContainsScreenPoint(treatmentArea, Input.mousePosition)||
            RectTransformUtility.RectangleContainsScreenPoint(bookShelfTransform, Input.mousePosition))
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
            // �{��{�I�Ƀh���b�v
            rectTransform.SetParent(bookShelfTransform, false);

            // ����������
            rectTransform.localScale = Vector3.one * 0.5f;

            // �{�I�̒��S�ɔz�u
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(treatmentArea, Input.mousePosition))
        {
            BackToShelf();
            operationManager.ShowParts(book);
        }
        else
        {
            BackToShelf();

        }

    }
    private void BackToShelf()
    {
        // �h���b�v�ł���G���A�ȊO�Ƀh���b�v���ꂽ�ꍇ�A���̈ʒu�ɖ߂�
        rectTransform.anchoredPosition = originalPosition;

        // �傫�������ɖ߂�
        rectTransform.localScale = Vector3.one;
    }
}

