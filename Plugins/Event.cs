using System;
using System.Collections.Generic;

namespace JAnalytics
{
    public abstract class Event
    {

        public const int CountEventType = 1;
        public const int CalculateEventType = 2;
        public const int LoginEventType = 3;
        public const int RegisterEventType = 4;
        public const  int BrowseEventType = 5;
        public const int PurchaseEventType = 6;

        Dictionary<string, string> extraDic = new Dictionary<string, string>();
        int type;
       
        public Event(int type)
        {
            this.type = type;
        }

        public int getType()
        {
            return type;
        }

        public void addKeyValue(string key,string value)
        {
            extraDic.Add(key,value);
        }
        public Dictionary<string,string> getExtraDic()
        {
            return extraDic;
        }
    }

    public class CountEvent : Event
    {
        string eventId;
        public CountEvent(string eventId) :base(CountEventType)
        {
            this.eventId = eventId;
        }
        public string getEventId()
        {
            return eventId;
        }
    }

    public class CalculateEvent : Event
    {
        string eventId; 
        double eventValue;
        public CalculateEvent(string eventId,double eventValue) : base(CalculateEventType)
        {
            this.eventId = eventId;
            this.eventValue = eventValue;
        }

        public string getEventId()
        {
            return eventId;
        }

        public double getEventValued()
        {
            return eventValue;
        }
    }


    public class LoginEvent : Event
    {
        string loginMethod;
        bool loginSuccess;
        public LoginEvent(string loginMethod, bool loginSuccess) : base(LoginEventType)
        {
            this.loginMethod = loginMethod;
            this.loginSuccess = loginSuccess;
        }
        public string getLoginMethod()
        {
            return loginMethod;
        }
        public bool geLoginSuccess()
        {
            return loginSuccess;
        }
    }


    public class RegisterEvent : Event
    {
        string registerMethod; 
        bool registerSuccess;
        public RegisterEvent(string registerMethod, bool registerSuccess) : base(RegisterEventType)
        {
            this.registerMethod = registerMethod;
            this.registerSuccess = registerSuccess;
        }
        public string getRegisterMethod()
        {
            return registerMethod;
        }
        public bool getRegisterSuccess()
        {
            return registerSuccess;
        }
    }

    public class BrowseEvent : Event
    {
        string browseId; string browseName; string browseType; long browseDuration;
        public BrowseEvent(string browseId, string browseName,string browseType,long browseDuration) : base(BrowseEventType)
        {
            this.browseId = browseId;
            this.browseName = browseName;
            this.browseType = browseType;
            this.browseDuration = browseDuration;
        }
        public string getBrowseId()
        {
            return browseId;
        }
        public string getBrowseName()
        {
            return browseName;
        }
        public string getBrowseType()
        {
            return browseType;
        }
        public long getBrowseDuration()
        {
            return browseDuration;
        }
    }


    public class PurchaseEvent : Event
    {
        public static readonly int CNY = 0;
        public static readonly int USD = 1;

        string purchaseGoodsid; 
        string purchaseGoodsName; 
        double purchasePrice; 
        bool purchaseSuccess;
        Currency purchaseCurrency; 
        string purchaseGoodsType;
        int purchaseGoodsCount;
        public PurchaseEvent(string purchaseGoodsid, string purchaseGoodsName,double purchasePrice,bool purchaseSuccess,
             Currency purchaseCurrency, string purchaseGoodsType,int purchaseGoodsCount) : base(PurchaseEventType)
        {
            this.purchaseGoodsid = purchaseGoodsid;
            this.purchaseGoodsName = purchaseGoodsName;
            this.purchasePrice = purchasePrice;
            this.purchaseSuccess = purchaseSuccess;
            this.purchaseCurrency = purchaseCurrency;
            this.purchaseGoodsType = purchaseGoodsType;
            this.purchaseGoodsCount = purchaseGoodsCount;
        }
        public string getPurchaseGoodsid()
        {
            return purchaseGoodsid;
        }
        public string getPurchaseGoodsName()
        {
            return purchaseGoodsName;
        }
        public double getPurchasePrice()
        {
            return purchasePrice;
        }
        public bool getPurchaseSuccess()
        {
            return purchaseSuccess;
        }
        public Currency getPurchaseCurrency()
        {
            return purchaseCurrency;
        }

        public int getPurchaseCurrencyInt()
        {
            if(Currency.USD == purchaseCurrency)
            {
                return USD;
            }
            return CNY;
        }

        public string getPurchaseGoodsType()
        {
            return purchaseGoodsType;
        }
        public int getPurchaseGoodsCount()
        {
            return purchaseGoodsCount;
        }
    }

}