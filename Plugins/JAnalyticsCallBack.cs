using System;
public class JAnalyticsCallBack
{
    int sequence;
    string gameObjectName;
    string gameObjectMethod;
    public JAnalyticsCallBack(int sequence, string gameObjectName, string gameObjectMethod)
    {
        this.sequence = sequence;
        this.gameObjectName = gameObjectName;
        this.gameObjectMethod = gameObjectMethod;
    }
    public string getGameObjectName()
    {
        return gameObjectName;
    }
    public string getGameObjectMethode()
    {
        return gameObjectMethod;
    }
    public int getSequence()
    {
        return sequence;
    }
}
