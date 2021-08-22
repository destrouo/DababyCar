using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class Donate : MonoBehaviour
{
    public GameObject Donate_Panel;
    public Text T_Coins;

    // Start is called before the first frame update
    void Start()
    {
        PurchaseManager.OnPurchaseConsumable += PurchaseManager_OnPurchaseConsumable;
        
    }

    private void PurchaseManager_OnPurchaseConsumable(PurchaseEventArgs args)
    {
        int Coins = PlayerPrefs.GetInt("Coins");
        switch (args.purchasedProduct.definition.id)
        {
            case "c_prod0":
                //Zvyk  

                Coins = Coins + 500;
                PlayerPrefs.SetInt("Coins", Coins);
                T_Coins.text = Coins.ToString();

                //Add Coins and update
                break;
            case "c_prod1":
                //Zvyk  

                Coins = Coins + 1500;
                PlayerPrefs.SetInt("Coins", Coins);
                T_Coins.text = Coins.ToString();

                break;
            case "c_prod2":
                //Zvyk  

                Coins = Coins + 6000;
                PlayerPrefs.SetInt("Coins", Coins);
                T_Coins.text = Coins.ToString();

                break;
            case "c_prod3":
                //Zvyk  

                Coins = Coins + 11488;
                PlayerPrefs.SetInt("Coins", Coins);
                T_Coins.text = Coins.ToString();

                break;
                //Gives you 1500 coins
        }
    }
    // Update is called once per frame

    public void OnShowDonate()
    {
        Donate_Panel.SetActive(true);
    }
    public void OnCloseDonate()
    {
        Donate_Panel.SetActive(false);
    }
    void Update()
    {
        
    }
}
