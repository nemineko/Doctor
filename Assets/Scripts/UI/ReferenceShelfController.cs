using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReferenceShelfController : MonoBehaviour
{
    [Header("UI Toolkit")]
    [SerializeField] private UIDocument document;
    [SerializeField] private VisualTreeAsset bookItemUxml;

    [Header("Books (表示したい本のリスト)")]
    [SerializeField] private List<Book> booksToShow = new();

    [Header("Layout (レイアウト設定)")]
    [SerializeField] private int columnsPerRow = 10; // 1段に入れる冊数（固定）
    [SerializeField] private float bookWidth = 30f;  // 使わなくてもOK（将来用）
    [SerializeField] private float bookHeight = 100f;

    [Header("Shelf padding (枠に被るなら調整)")]
    [SerializeField] private float shelfPaddingTop = 0f;
    [SerializeField] private float shelfPaddingBottom = 0f;

    // Hover側が参照する「展開時の高さ」
    public float ExpandedShelfHeight { get; private set; }

    private VisualElement referenceSlots;
    private VisualElement referenceShelf;

    void Awake()
    {
        if (document == null) document = GetComponent<UIDocument>();

        var root = document.rootVisualElement;
        referenceSlots = root.Q<VisualElement>("referenceSlots");
        referenceShelf = root.Q<VisualElement>("referenceShelf");
    }

    void Start()
    {
        RefreshShelf();
    }

    public void RefreshShelf()
    {
        if (referenceSlots == null)
        {
            Debug.LogError("referenceSlots が見つかりません。UXMLに name=\"referenceSlots\" があるか確認してください。");
            return;
        }

        if (referenceShelf == null)
        {
            Debug.LogError("referenceShelf が見つかりません。UXMLに name=\"referenceShelf\" があるか確認してください。");
            return;
        }

        if (bookItemUxml == null)
        {
            Debug.LogError("bookItemUxml が未設定です。Inspectorで BookItem.uxml を割り当ててください。");
            return;
        }

        // いったん棚の中身を全部クリア
        referenceSlots.Clear();

        // 本の数だけ生成
        foreach (var book in booksToShow)
        {
            if (book == null) continue;

            VisualElement item = bookItemUxml.CloneTree();

            Image cover = item.Q<Image>("coverImage");
            if (cover == null)
            {
                Debug.LogError("BookItem.uxml 内に name=\"coverImage\" の ui:Image が見つかりません。");
                continue;
            }

            cover.image = book.coverImage != null ? book.coverImage.texture : null;

            // 高さは計算と一致させたいので指定（幅はUSSの30pxに任せる前提）
            item.style.height = bookHeight;

            item.RegisterCallback<ClickEvent>(_ =>
            {
                Debug.Log($"本クリック: ID={book.bookID}");
            });

            referenceSlots.Add(item);
        }

        // ===== 展開時の棚の高さを計算して保存 =====
        int count = booksToShow.Count;
        int cols = Mathf.Max(1, columnsPerRow);

        // 何段必要か（0冊でも最低1段）
        int rows = Mathf.CeilToInt(count / (float)cols);
        rows = Mathf.Max(1, rows);

        ExpandedShelfHeight = rows * bookHeight + shelfPaddingTop + shelfPaddingBottom;

        // ※ ここでは referenceShelf の高さは触らない（Hover側が制御する）
        // referenceSlots の高さも、Hoverで一緒に変えたいなら Hover側でセットするのが安全
    }

    public void SetBooks(List<Book> newBooks)
    {
        booksToShow = newBooks ?? new List<Book>();
        RefreshShelf();
    }
}
