using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Book", order = 1)]
public class Book : ScriptableObject
{
    public Sprite coverImage;  // �w�\���̉摜
    public List<string> pages; // �y�[�W���Ƃ̃e�L�X�g��ǉ�
    public List<string> treatment; // ���Ö@�ꗗ
    public bool isSpecial;     // �ǂ���̒I�ɖ{��u����
}

