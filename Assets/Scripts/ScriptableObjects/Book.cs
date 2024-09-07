using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Book", order = 1)]
public class Book : ScriptableObject
{
    public string title;       // �^�C�g��
    //public string author;
    public Sprite coverImage;  // �w�\���̉摜
    public List<string> pages; // �y�[�W���Ƃ̃e�L�X�g��ǉ�
}

