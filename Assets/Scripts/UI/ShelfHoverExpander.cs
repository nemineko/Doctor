using UnityEngine;
using UnityEngine.UIElements;

public class ShelfHoverExpander : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private float collapsedHeight = 50f; // 折りたたみ時に見せる高さ

    private VisualElement shelf;
    private ReferenceShelfController shelfController;

    void Awake()
    {
        if (document == null) document = GetComponent<UIDocument>();

        var root = document.rootVisualElement;
        shelf = root.Q<VisualElement>("referenceShelf");

        shelfController = GetComponent<ReferenceShelfController>();

        // 初期状態：折りたたみ
        shelf.style.height = collapsedHeight;

        shelf.RegisterCallback<MouseEnterEvent>(_ =>
        {
            // 本の数から計算された「正しい高さ」で展開
            shelf.style.height = shelfController.ExpandedShelfHeight;
        });

        shelf.RegisterCallback<MouseLeaveEvent>(_ =>
        {
            shelf.style.height = collapsedHeight;
        });
    }
}
