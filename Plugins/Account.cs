using System.Collections.Generic;

public class Account
{
    private static readonly string accountID_account_key = "accountID";
    private static readonly string creationTime_account_key = "creationTime";
    private static readonly string name_account_key = "name";
    private static readonly string sex_account_key = "sex";
    private static readonly string paid_account_key = "paid";
    private static readonly string birthdate_account_key = "birthdate";
    private static readonly string phone_account_key = "phone";
    private static readonly string email_account_key = "email";
    private static readonly string weiboID_account_key = "weiboID";
    private static readonly string wechatID_account_key = "wechatID";
    private static readonly string qqID_account_key = "qqID";

    Dictionary<string, string> _accountDic = new Dictionary<string, string>();
    Dictionary<string, string> _extraDic = new Dictionary<string, string>();


    public Account(string accountID)
    {
        _accountDic.Add(accountID_account_key, accountID);
    }


    public void SetCreationTime(long creationTime)
    {
        _accountDic.Add(creationTime_account_key,
                      creationTime+"");
    }
    public void SetName(string name)
    {
        _accountDic.Add(name_account_key, name);
    }
    public void SetSex(int sex)
    {
        _accountDic.Add(sex_account_key, sex+"");
    }
    public void SetPaid(int paid)
    {
        _accountDic.Add(paid_account_key, paid+"");
    }
    public void SetBirthdate(string birthdate)
    {
        _accountDic.Add(birthdate_account_key, birthdate);
    }
    public void SetPhone(string phone)
    {
        _accountDic.Add(phone_account_key, phone);
    }
    public void SetEmail(string email)
    {
        _accountDic.Add(email_account_key, email);
    }
    public void SetWeiboID(string weiboID)
    {
        _accountDic.Add(weiboID_account_key, weiboID);
    }
    public void SetWechatID(string wechatID)
    {
        _accountDic.Add(wechatID_account_key, wechatID);
    }
    public void SetQqID(string qqID)
    {
        _accountDic.Add(qqID_account_key, qqID);
    }

    public void SetExtraAttr(string key,string value)
    {
        _extraDic.Add(key, value);
    }

    public Dictionary<string, string> GetAccountDic()
    {
        return _accountDic;
    }

    public Dictionary<string, string> GetExtraDic()
    {
        return _extraDic;
    }
}
