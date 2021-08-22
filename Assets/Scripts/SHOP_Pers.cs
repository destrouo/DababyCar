using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SHOP_Pers : MonoBehaviour
{
    public Text T_Coins;
    public Text Price;

    public Text T_NoMoreMoney;

    public AudioSource GameMusic;

    public GameObject RANGE;
    public GameObject Shop_Pannel;

    public int[] Wheels_cost;

    private int[] StockWheels;
    public GameObject WheelsShop;
    public GameObject[] Wheels;
    

    public Button buyBttn;
    public Button EquBttn;


    private int index;
    private string groupOfItems;

    public AudioSource Bought;
    public AudioSource Equip;
    public AudioSource Fart;
    public AudioSource Swish;


    // IF FIRST START Add In Array 0 integers.

    private void Awake()
    {
        
        Shop_Pannel.SetActive(false);
        T_Coins.text = PlayerPrefs.GetInt("Coins").ToString();
        WheelsShop.SetActive(false);
        buyBttn.gameObject.SetActive(false);
        EquBttn.gameObject.SetActive(false);
        Price.gameObject.SetActive(false);

        StockWheels = PlayerPrefsX.GetIntArray("StockWheels");

    }

    void Start()
    {

        if (DataHolder._Music == true)
            GameMusic.GetComponent<AudioSource>().Play();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
   
    //MB Save 1 button
    public void BuyItem()
    {

        switch(groupOfItems)
        {
            case "Wheels":
                int Coins = PlayerPrefs.GetInt("Coins");
                if (Coins > Wheels_cost[index])
                {
                    //sound
                    Bought.GetComponent<AudioSource>().Play();


                    Coins = Coins - Wheels_cost[index];
                    T_Coins.text = Coins.ToString();

                    PlayerPrefs.SetInt("Coins", Coins);

                    if (!StockWheels.Contains(index))
                    {
                        StockWheels = StockWheels.Concat(new int[] { index }).ToArray();

                        PlayerPrefsX.SetIntArray("StockWheels", StockWheels);

                    }

                    buyBttn.gameObject.SetActive(false);
                    EquBttn.gameObject.SetActive(true);
                    EquBttn.interactable = false;

                    PlayerPrefs.SetInt("EquipWheels", index);
                }
                else
                { 
                    T_NoMoreMoney.gameObject.SetActive(true);
                    StartCoroutine(LateCall());
                    // Sound FART
                    Fart.GetComponent<AudioSource>().Play();
                }    

                break;
            case "Car":
                
                break;

        }


    }

    public void EquipItem()
    {
        switch (groupOfItems)
        {
            case "Wheels":

                    Equip.GetComponent<AudioSource>().Play();

                    PlayerPrefs.SetInt("EquipWheels", index);

                    EquBttn.interactable = false;

                break;
            case "Car":

                break;

        }


    }

    public void WheelsScroll_Right()
    {
        if (index < (Wheels.Length - 1))
        {
            T_NoMoreMoney.gameObject.SetActive(false);

            Swish.GetComponent<AudioSource>().Play();
            index++;
            EquBttn.interactable = true;

            // ЕСЛИ МЫ УЖЕ КУПИЛИ ПРЫКОЛ
            if (StockWheels.Contains(index))
            {
                buyBttn.gameObject.SetActive(false);
                EquBttn.gameObject.SetActive(true);

                Price.text = "bought";
            }
            else
            {
                buyBttn.gameObject.SetActive(true);
                EquBttn.gameObject.SetActive(false);
                Price.text = Wheels_cost[index].ToString();
            }

            for (int i = 0; i < Wheels.Length; i++)
            {
                Wheels[i].SetActive(false);
            }

            Wheels[index].SetActive(true);

        }
    }
    public void WheelsScroll_Left()
    {
        if (index > 0)
        {
            T_NoMoreMoney.gameObject.SetActive(false);

            Swish.GetComponent<AudioSource>().Play();
            index--;
            EquBttn.interactable = true;

            // ЕСЛИ МЫ УЖЕ КУПИЛИ ПРЫКОЛ
            if (StockWheels.Contains(index))
            {
                buyBttn.gameObject.SetActive(false);
                EquBttn.gameObject.SetActive(true);

                Price.text = "bought";
            }
            else
            {
                buyBttn.gameObject.SetActive(true);
                EquBttn.gameObject.SetActive(false);
                Price.text = Wheels_cost[index].ToString();
            }

            for (int i = 0; i < Wheels.Length; i++)
            {
                Wheels[i].SetActive(false);
            }

            Wheels[index].SetActive(true);

        }
    }
    public void OnClickShop_WHEELS()
    {
        RANGE.SetActive(false);
        Shop_Pannel.SetActive(true);
        WheelsShop.SetActive(true);
        index = 0;
        groupOfItems = "Wheels";

        //Wheels
        for (int i = 0; i < Wheels.Length; i++)
        {
            Wheels[i].SetActive(false);
        }
        Wheels[0].SetActive(true);

        buyBttn.gameObject.SetActive(false);
        EquBttn.gameObject.SetActive(true);
        EquBttn.interactable = true;

        Price.text = "bought";
        Price.gameObject.SetActive(true);
        

        //Wheels Active
    }

    public void OnClickShop_CAR()
    {

        RANGE.SetActive(false);
        Shop_Pannel.SetActive(true);

        //CAR Active
    }

    public void OnClickShop_HEADWEAR()
    {

        RANGE.SetActive(false);
        Shop_Pannel.SetActive(true);

        //HEADWEAR Active
    }

    public void OnClickShop_ACCESSORIES()
    {

        RANGE.SetActive(false);
        Shop_Pannel.SetActive(true);

        //ACCESSORIES Active
    }

    public void OnClickShop_EXIT()
    {
        Shop_Pannel.SetActive(false);
        RANGE.SetActive(true);
        WheelsShop.SetActive(false);
        buyBttn.gameObject.SetActive(false);
        EquBttn.gameObject.SetActive(false);
        Price.gameObject.SetActive(false);
    }


        IEnumerator LateCall()
    {

        yield return new WaitForSeconds(2f);

        //Do Function here...
        T_NoMoreMoney.gameObject.SetActive(false);
    }

}
