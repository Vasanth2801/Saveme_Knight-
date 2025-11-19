using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int money = 200;
    [SerializeField] private TextMeshProUGUI moneyText;
    int shopIndex = 0;
    [SerializeField] private Button[] shopButtons;
    bool isPurchased = false;

    private void Awake()
    {
        Debug.Log(shopButtons.Length);
    }

    void Update()
    {
        moneyText.text = "Money: " + money.ToString();

        if(shopButtons != null && shopButtons.Length > 3)
        {
            shopButtons[0].interactable = money >= 50;
            shopButtons[1].interactable = money >= 30;
            shopButtons[2].interactable = money >= 70;
        }
    }

    public void BuyHealth()
    {
        shopIndex = 0;
        if(isPurchased)
        {
            money -= 50;
            Debug.Log("Health Purchased");
            Debug.Log("Remaining Money: " + money);
        }
    }

    public void BuyDamage()
    {
        shopIndex = 1;
        if(isPurchased)
        {
            money -= 30;
            Debug.Log("Damage Purchased");
            Debug.Log("Remaining Money: " + money);
        }
    }

    public void BuyShield()
    {
        shopIndex = 2;
        if(isPurchased)
        {
            money -= 70;
            Debug.Log("Shield Purchased");
            Debug.Log("Remaining Money: " + money);
        }
    }
}
