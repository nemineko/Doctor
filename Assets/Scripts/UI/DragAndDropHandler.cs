using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Vector2 dragOffset;

    private RectTransform bookshelfArea; // 本棚エリア
    private RectTransform stockArea; // 本のストックエリア
    private RectTransform treatmentArea; // 治療エリア
    private PopupManager popupManager; // ポップアップマネージャー
    private OperationManager operationManager; // プレイヤー操作エリア
    private Book book; // 選択された本

    // BookShelfからDragAndDropHandlerに参照を渡す
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
        // ドラッグ開始時の処理
        canvasGroup.blocksRaycasts = false;

        // ポップアップウィンドウを閉じる
        popupManager.ClosePopup();

        // 現在の親を取得
        RectTransform currentParent = rectTransform.parent as RectTransform;

        // マウスの位置と本の位置のオフセットを計算
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(currentParent, eventData.position, eventData.pressEventCamera, out localPoint);
        dragOffset = rectTransform.anchoredPosition - localPoint;

        // 本を手前に表示  
        rectTransform.SetParent(bookshelfArea, true);
        rectTransform.SetAsLastSibling(); // 本を最前面に移動
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中の処理
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bookshelfArea, eventData.position, eventData.pressEventCamera, out localPoint);
        rectTransform.anchoredPosition = localPoint + dragOffset;

        // ドロップできるエリアと本が重なったときだけに、本を1.5倍大きく表示させる
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
        // ドラッグ終了時の処理
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
            // ドロップできるエリア以外にドロップされた場合、元の位置に戻す
            rectTransform.anchoredPosition = originalPosition;

            // 親を元に戻す
            ChangeParent(bookshelfArea);
        }
    }

    private void ChangeParent(RectTransform area)
    {
        // 親を変更
        rectTransform.SetParent(area, false);

        // 親に配置
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
            // 大きさを元に戻す
            rectTransform.localScale = Vector3.one;

            rectTransform.anchoredPosition = new Vector2(bookX, originalPosition.y);
        }
        else
        {
            // 小さくする
            rectTransform.localScale = Vector3.one * 0.5f;
            rectTransform.anchoredPosition = new Vector2(bookX, bookY);

        }
    }
}
