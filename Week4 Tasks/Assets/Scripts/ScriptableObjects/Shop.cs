using UnityEngine;


[CreateAssetMenu(fileName = "NewShopItem", menuName = "ShopItem")]
public class Shop : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;
    public string itemDescription;
}
