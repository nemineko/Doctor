using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Book", order = 1)]
public class Book : ScriptableObject
{
    public int bookID;         // 本のID
    public Sprite coverImage;  // 背表紙の画像
    [TextArea(3, 10)]          // ページごとのテキストを追加
    public List<string> pages;
    [TextArea(2, 6)]           // 治療法一覧
    public List<string> treatment;
    public bool isSpecial;     // どちらの棚に本を置くか
}

