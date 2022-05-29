using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private BuyUpgrade[] sales;
        [SerializeField] private int MoneyStars;
        [SerializeField] private Text moneyText;

        private void Start()
        {
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }

            UpdateMoney();
        }
            
        public void UpdateMoney()
        {
            MoneyStars = MapCompletion.Instance.TotalScore;
            MoneyStars -= Upgrades.GetTotalCost();
            moneyText.text = MoneyStars.ToString();
            foreach(var slot in sales)
            {
                slot.CheckCost(MoneyStars);
            }
        }

    }
}
