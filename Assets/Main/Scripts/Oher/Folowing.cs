using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folowing : MonoBehaviour
{
    public Transform player; // ссылка на объект-игрока
    public float followDistance = 1f; // расстояние до объекта, которому следует захватить
    public float followSpeed = 10f; // скорость передвижения объекта к игроку
    public float angularSpeed = 100f; // скорость вращения объекта
    
    public int ratio = 1;
    
    private float positionX, positionY, angle = 0f;

    void Start()
    {
        angle = 0;
    }

    private void FixedUpdate()
    {
        // Рассчитываем координаты точки, к которой должен приблизиться объект
        Vector3 targetPosition = player.position + transform.right * followDistance * ratio;

        // Плавно передвигаем объект в направлении координат игрока
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        /*positionX = (float)(player.position.x + Mathf.Cos(angle) * followDistance);
        positionY = (float)(player.position.y + Mathf.Sin(angle) * followDistance);
        transform.position = new Vector2(positionX, positionY);

        angle += angularSpeed * Time.fixedDeltaTime;

        if (angle >= 360)
        {
            angle = 0;
        }*/
        
    }
}
