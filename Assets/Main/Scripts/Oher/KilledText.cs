using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class KilledText : MonoBehaviour
{
    public UnityEvent EndOfEvent;
    
    private int enemies = 7;

    [SerializeField] private TextMeshProUGUI remainText;
    private void Start()
    {
        GlobalEventManager.OnEnemyKilled.AddListener(EnemyKilled);
        remainText.text = "Remain : " + enemies;
    }

    private void EnemyKilled(int remainingEnemies)
    {
        enemies -= remainingEnemies;
        if (enemies == 0)
        {
            EndOfEvent.Invoke();
            Destroy(gameObject);
        }
        remainText.text = "Remain : " + enemies;
    }
    
}
