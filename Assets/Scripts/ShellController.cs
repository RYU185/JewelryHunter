using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        // deleteTime�� ������ ������ �ı�
        Destroy(this.gameObject, deleteTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���𰡿� �ε�ġ�� ��� ����
        Destroy(this.gameObject);
    }

    // ������ ������ ��� ������Ʈ(=Ư��)�� �������ִ� ���� ������ Asset : ������
    // = '��ź'�� ���� �Ҹ� ��ü��
    // = ���� ���������� �ݺ������� ���Ǵ� Asset

    // Update is called once per frame
    void Update()
    {
        
    }
}
