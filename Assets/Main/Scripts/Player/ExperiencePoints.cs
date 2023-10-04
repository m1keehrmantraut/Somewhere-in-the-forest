using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperiencePoints : MonoBehaviour
{
    [SerializeField] private TMP_Text PointsText;
    private PlayerHealth _playerHealth;

    private Shooting _shooting;
    private MeleeCombat _melee;
    [SerializeField] private Bullet _bullet;

    [SerializeField] private Image upgradeButton;

    [SerializeField] private Sprite pointsButton;
    [SerializeField] private Sprite defaultButton;

    [SerializeField] private Sprite[] maxLevelSprites;
    [SerializeField] private Image[] buttonImages;

    [SerializeField] private Image[] bubbleButton;
    [SerializeField] private Sprite[] bubbleSprites;

    private int experience = 0;

    private int healthLevel;
    private int strenthLevel;
    private int manaLevel;

    private void Start()
    {
        _playerHealth = gameObject.GetComponent<PlayerHealth>();
        _shooting = GameObject.FindWithTag("ShootPoint").GetComponent<Shooting>();
        _melee = GameObject.FindWithTag("AttackPoint").GetComponent<MeleeCombat>();
        UpdateExp();
    }

    public void ChangeExperience(int count)
    {
        experience += count;
        UpdateExp();
    }

    public void UpgradeHealth()
    {
        if (experience > 0 && healthLevel < 5)
        {
            _playerHealth.maxHealth += 10;
            _playerHealth.UpdateMaxHealth();
            ChangeExperience(-1);
            ChangeBubbles(0, healthLevel);
            healthLevel++;
        }

        if (healthLevel == 5)
        {
            ChangeSpriteButton(0);
        }
        
    }
    
    public void UpgradePower(float amount)
    {
        if (experience > 0 && strenthLevel < 5)
        {
            _melee.IncreaseBoost(amount);
            _bullet.IncreaseBoost(amount);
            ChangeExperience(-1);
            ChangeBubbles(1, strenthLevel + 5);
            strenthLevel++;
        }

        if (strenthLevel == 5)
        {
            ChangeSpriteButton(1);
        }
        
    }

    public void UpgradeMana(float amount)
    {
        if (experience > 0 && manaLevel < 5)
        {
            _shooting.IncreaseMana(amount);
            ChangeExperience(-1);
            ChangeBubbles(2, manaLevel + 10);
            manaLevel++;
        }

        if (manaLevel == 5)
        {
            ChangeSpriteButton(2);
        }
        
    }
    
    void UpdateExp()
    {
        PointsText.text = experience.ToString();
        
        if (experience > 0)
        {
            upgradeButton.sprite = pointsButton;
        }
        else
        {
            upgradeButton.sprite = defaultButton;
        }
    }

    private void ChangeSpriteButton(int buttonID)
    {
        buttonImages[buttonID].sprite = maxLevelSprites[buttonID];
    }

    private void ChangeBubbles(int bubbleID, int buttonID)
    {
        bubbleButton[buttonID].sprite = bubbleSprites[bubbleID];
    }
}
