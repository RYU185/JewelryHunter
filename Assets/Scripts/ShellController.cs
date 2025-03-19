using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        // deleteTime이 지나면 스스로 파괴
        Destroy(this.gameObject, deleteTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 무언가와 부딛치면 즉시 자폭
        Destroy(this.gameObject);
    }

    // 씬에는 없지만 모든 컴포넌트(=특성)을 가지고있는 파일 상태의 Asset : 프리펩
    // = '포탄'과 같은 소모성 객체들
    // = 여러 스테이지에 반복적으로 사용되는 Asset

    // Update is called once per frame
    void Update()
    {
        
    }
}
