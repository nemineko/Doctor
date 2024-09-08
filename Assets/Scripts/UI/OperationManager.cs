using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OperationManager : MonoBehaviour
{
    [SerializeField] GameObject parts; // parts�I�u�W�F�N�g
    [SerializeField] TMP_Dropdown dropdown; // parts����Dropdown

    public void ShowParts(Book theBook)
    {
        // parts��\��
        parts.SetActive(true);

        // Dropdown��options��treatment���X�g��ǉ�
        dropdown.options.Clear();
        foreach (string treatment in theBook.treatment)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(treatment));
        }
    }

    public void OnYesButtonClick()
    {
        // �V�i���I��i�s�����鏈��
        Debug.Log("�V�i���I���i�s���܂�");
        
    }

    public void OnNoButtonClick()
    {
        // �����N����Ȃ�����
        Debug.Log("�����N����܂���");
    }
}
