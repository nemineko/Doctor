using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Book", order = 1)]
public class Book : ScriptableObject
{
    public string title;       // タイトル
    //public string author;
    public Sprite coverImage;  // 背表紙の画像
    public List<string> pages; // ページごとのテキストを追加
}

