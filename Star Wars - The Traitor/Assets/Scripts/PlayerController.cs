using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 12f; // Скорость движения персонажа
    public Animator animator; // Ссылка на компонент Animator

    private float currentSpeed; // Текущая скорость движения персонажа
    private bool isMoving; // Флаг для отслеживания состояния движения

    void Start()
    {
        animator = GetComponent<Animator>(); // Получаем компонент Animator
    }

    void Update()
    {
        // Получаем ввод с клавиатуры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Вычисляем вектор направления движения
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Применяем движение к персонажу
        transform.Translate(movement, Space.Self);

        // Вычисляем текущую скорость движения персонажа
        currentSpeed = new Vector2(horizontalInput, verticalInput).magnitude;

        // Обновляем параметр анимации "Speed" в аниматоре
        animator.SetFloat("Speed", currentSpeed);

        // Проверяем, если персонаж начинает движение, меняем анимацию
        if (currentSpeed > 0.1f && !isMoving)
        {
            isMoving = true;
            animator.SetBool("IsMoving", true);
        }
        // Проверяем, если персонаж останавливается, меняем анимацию
        else if (currentSpeed <= 0.1f && isMoving)
        {
            isMoving = false;
            animator.SetBool("IsMoving", false);
            animator.CrossFade("Rifle_Idle", 0.15f);
        }

        // Получаем ввод с мыши
        float mouseX = Input.GetAxis("Mouse X");

        // Поворачиваем персонаж по оси Y в соответствии с вращением мыши
        transform.Rotate(Vector3.up, mouseX);

        MovingAnimationSetter("MovingForward", "Rifle_Walk",KeyCode.W, 12f);
        MovingAnimationSetter("MovingBack", "Rifle_Walk_Back", KeyCode.S, 12f);
        MovingAnimationSetter("MovingRight", "Rifle_Walk_Right", KeyCode.D, 3f);
        MovingAnimationSetter("MovingLeft", "Rifle_Walk_Left", KeyCode.A, 3f);


        if (Input.GetKey(KeyCode.T))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    private void MovingAnimationSetter(string animationFlag,string animationName, KeyCode keyCode, float moveSpeedNew)
    {
        if (Input.GetKeyDown(keyCode))
        {

            animator.SetBool(animationFlag, true);
            animator.CrossFade(animationName, 0.15f);
            moveSpeed = moveSpeedNew;
        }
        else if (Input.GetKeyUp(keyCode))
        {
            animator.SetBool(animationFlag, false);
            moveSpeed = 12f;
        }

    }

}