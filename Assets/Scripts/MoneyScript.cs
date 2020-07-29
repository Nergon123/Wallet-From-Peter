using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
	public GameObject RW;
    public GameObject DW;
    public Image DI;
    public Text[] texts;
    public Text Typeof;
    public Button health;
    public Button card;
    public Button restourant;
    public Button shop;
    public Button Accept;
    public Button Cancel;
    public Button YesR;
    public Button NoR;
    public float[] moneys;
    public Button RESET;
    public string[] strings;
    public Sprite[] sprites;
    public Scrollbar[] scrollbars;
    public InputField input;
    public Text log;
    public Text errorReset;
    public Toggle resetLog;
    public Toggle resetStats;
    public RectTransform content;
    int type;
    /*
     * 0 - Медицина
     * 1 - Карта
     * 2 - Ресторан
     * 3 - Магазин
     * 4 - Все
     */
    
    void Start()
    {
        Cancel.onClick.AddListener(Cancelv);
        Accept.onClick.AddListener(Acceptv);
        health.onClick.AddListener(healthv);
        card.onClick.AddListener(cardv);
        restourant.onClick.AddListener(restourantv);
        shop.onClick.AddListener(shopv);
        RESET.onClick.AddListener(resetv);
        NoR.onClick.AddListener(NoRv);
        YesR.onClick.AddListener(YesRv);
        int i = 0;
        while (i<4) {
        	PlayerPrefs.GetFloat("moneys" + i.ToString(),0.0f);
        	i++;
        }
    }
    
    void YesRv(){
    	int a = 0;
        string status;
        status = "";
        if (resetStats.isOn == true)
        {
            while (a < 4)
            {
                moneys[a] = 0.0f;
                PlayerPrefs.SetFloat("moneys" + a.ToString(), 0.0f);
                a++;
            }
            status = status + "Статистика; ";
        }
        if (resetLog.isOn == true)
        {
            PlayerPrefs.DeleteKey("log");
            status = status + " Події;";
        }

        PlayerPrefs.SetString("log", PlayerPrefs.GetString("log", "Початок подій \n") + System.DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " Скидування данних : " + status + " \n");


        if (resetLog.isOn || resetStats.isOn == true)
        {
            RW.SetActive(false);
        }
        else
        {
            errorReset.text = "Помилка: хотя б один варіант має бути вибраним";
        }
    	
    }
    void NoRv(){
    	RW.SetActive(false);
    }
    void resetv(){
    	RW.SetActive(true);
    }
    void Cancelv()
    {
        DW.SetActive(false);
    }
    void Acceptv()
    {
    	
        moneys[type] =moneys[type] + Convert.ToSingle(Math.Round(float.Parse(input.text,CultureInfo.InvariantCulture),2));
        PlayerPrefs.SetString("log", PlayerPrefs.GetString("log", "Початок подій \n") + System.DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " Поповнення категорії " + strings[type] + " на " + input.text + " грн. \n" );
        input.text = "";
        DW.SetActive(false);
    }
    void healthv()
    {
        type = 0;
        Add();
    }
    void cardv()
    {
        type = 1;
        Add();
    }
    void restourantv()
    {
        type = 2;
        Add();
    }
 
    void shopv() 
    {
        type = 3;
        Add();
    }
    void Add()
    {
        Typeof.text = strings[type];
        DI.sprite = sprites[type];
        DW.SetActive(true);

    }
    private void Update()
    {
        log.text = PlayerPrefs.GetString("log", "Ви ще нічого не робили)");
        content.sizeDelta = new Vector2(0, (int)(((log.text.Length /21)* 45) + 164));
        float a = 0;
        int i = 0;
        while (i < 4)
        {
            a += moneys[i];
            
            double b = Math.Round(a,2);
            moneys[4] = Convert.ToSingle(b);
            i++;
        }
        texts[4].text = moneys[4].ToString() + " грн.";
        i = 0;
        while (i < 4)
        {
        	float f = moneys[4];
        	if(f == 0.0f){
        		f = 0.01f;
        	}

            scrollbars[i].size = (moneys[i] / f);
            Debug.Log((moneys[i] / f).ToString());
            i++;
        }
        i = 0;
        while (i < 4)
        {
        	float f = moneys[4];
        	if(f == 0.0f){
        		f = 0.01f;
        	}
        	texts[i].text = moneys[i].ToString() + " грн. " + ((int)(moneys[i] / f * 100)).ToString() + "%";
            i++;
        }
        i = 0;
        while (i < 4) {
        	PlayerPrefs.SetFloat("moneys" + i.ToString(),moneys[i]);
        	i++;
        }
    }

}
