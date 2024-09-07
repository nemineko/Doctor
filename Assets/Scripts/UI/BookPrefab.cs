using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BookPrefab : MonoBehaviour
{
    public Text titleText;
    public Text authorText;
    public Image coverImage;

    public void Initialize(Book book)
    {
        titleText.text = book.title;
        //authorText.text = book.author;
        coverImage.sprite = book.coverImage;
    }

}
