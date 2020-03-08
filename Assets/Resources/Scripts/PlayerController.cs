using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float WalkSpeed;
    [SerializeField]
    private float RunSpeed;
    [SerializeField]
    private float MouseSensitivity;

    private float CurrentRotateY;
    private float PreSpeed;
    private Rigidbody PlayerBody;

    private void Start()
    {
        PreSpeed = WalkSpeed;
        PlayerBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        TryRotate();
        TryRun();
        Move();
    }

    private void Move()
    {
        float _x = Input.GetAxisRaw("Vertical");
        float _z = Input.GetAxisRaw("Horizontal");

        Vector3 _hor = transform.forward * _x;
        Vector3 _ver = transform.right * _z;

        PlayerBody.velocity = (_hor + _ver).normalized * PreSpeed * Time.deltaTime;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            PreSpeed = RunSpeed;
        else
            PreSpeed = WalkSpeed;
    }

    private void TryRotate()
    {
        // 마우스 X축(좌, 우)에 대한 이동 값 반환
        float _yRotation = Input.GetAxisRaw("Mouse X");
        // 마우스 이동 값 * 마우스의 민감도 -> Character의 회전값 정의
        CurrentRotateY += _yRotation * MouseSensitivity;
        if (CurrentRotateY >= 360)
            CurrentRotateY = 0;
        // Character의 지역 Y 축에 대한 회전 값 정의 -> 캐릭터 방향 회전
        transform.localEulerAngles = new Vector3(0, CurrentRotateY, 0);
    }
}
