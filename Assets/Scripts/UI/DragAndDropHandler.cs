using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Vector2 dragOffset;

    private RectTransform bookShelfTransform; // 本棚
    private RectTransform stockArea; // 本のストックエリア
    private RectTransform treatmentArea; // 治療エリア
    private PopupManager popupManager; // ポップアップマネージャー
    private OperationManager operationManager; // プレイヤー操作エリア
    private Book book; // 選択された本

    // BookShelfからDragAndDropHandlerに参照を渡す
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
        // ドラッグ開始時の処理
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;

        // ポップアップウィンドウを閉じる
        popupManager.ClosePopup();

        // 本を手前に表示  
        rectTransform.SetParent(bookShelfTransform, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        // ドラッグ中の処理
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bookShelfTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        rectTransform.anchoredPosition = localPoint + dragOffset;


        // ドロップできるエリアと本が重なったときだけに、本を1.5倍大きく表示させる
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
        // ドラッグ終了時の処理
        canvasGroup.blocksRaycasts = true;

        
        if (RectTransformUtility.RectangleContainsScreenPoint(stockArea, Input.mousePosition))
        {
            // 本を本棚にドロップ
            rectTransform.SetParent(bookShelfTransform, false);

            // 小さくする
            rectTransform.localScale = Vector3.one * 0.5f;

            // 本棚の中心に配置
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
        // ドロップできるエリア以外にドロップされた場合、元の位置に戻す
        rectTransform.anchoredPosition = originalPosition;

        // 大きさを元に戻す
        rectTransform.localScale = Vector3.one;
    }
}

