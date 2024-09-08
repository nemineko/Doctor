using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BookPrefab : MonoBehaviour
{
    public Image coverImage;

    public void Initialize(Book book)
    {
        coverImage.sprite = book.coverImage;
    }

}
