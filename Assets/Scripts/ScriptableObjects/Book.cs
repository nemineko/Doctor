using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Book", order = 1)]
public class Book : ScriptableObject
{
    public Sprite coverImage;  // 背表紙の画像
    public List<string> pages; // ページごとのテキストを追加
    public List<string> treatment; // 治療法一覧
    public bool isSpecial;     // どちらの棚に本を置くか
}

