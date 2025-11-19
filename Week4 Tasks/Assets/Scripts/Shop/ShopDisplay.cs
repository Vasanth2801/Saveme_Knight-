using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    public Shop shopItem;

    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private Image itemIcon;

    private void Start()
    {
        itemName.text = shopItem.itemName;
        itemIcon.sprite = shopItem.itemIcon;
        itemPrice.text = shopItem.itemPrice.ToString();
        itemDescription.text = shopItem.itemDescription;
    }
}