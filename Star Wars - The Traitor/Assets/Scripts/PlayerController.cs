using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class PlayerController : MonoBehaviour
{
    public float stopTime = 1f; // Время, за которое персонаж остановится (в секундах)
    public float moveSpeed = 12f; // Скорость движения персонажа
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

       
    }
}