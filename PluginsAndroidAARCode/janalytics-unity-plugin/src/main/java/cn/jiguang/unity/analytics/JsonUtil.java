package cn.jiguang.unity.analytics;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;

public class JsonUtil {
    public static Set<String> jsonToSet(String tagsJsonStr) {
        Set<String> tagSet = new LinkedHashSet<String>();

        try {
            JSONObject itemsJsonObj = new JSONObject(tagsJsonStr);
            JSONArray tagsJsonArr = itemsJsonObj.getJSONArray("Items");

            for (int i = 0; i < tagsJsonArr.length(); i++) {
                tagSet.add(tagsJsonArr.getString(i));
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return tagSet;
    }


//    {"Items":["111","222"]}

    public static String setToJson(Set<String> tagSet) {
        if (null == tagSet || tagSet.size() == 0) {
            return null;
        }

        JSONObject itemsJsonObj = new JSONObject();
        JSONArray tagsJsonArr = new JSONArray();

        for (String tag : tagSet
        ) {
            tagsJsonArr.put(tag);
        }
        try {
            itemsJsonObj.put("Items", tagsJsonArr);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return itemsJsonObj.toString();
    }



    public static Map<String,String> unityJsonToMap(String jsonMap){
        HashMap hashMap = new HashMap();
        try {
//            {"keys":["1"],"values":["a"],"keysNull":["2"]}
            JSONObject itemsJsonObj = new JSONObject(jsonMap);
            JSONArray arrayKey = itemsJsonObj.getJSONArray("keys");
            JSONArray arrayValues = itemsJsonObj.getJSONArray("values");
            JSONArray arrayKeysNull = itemsJsonObj.getJSONArray("keysNull");


            if (null != arrayKey && null != arrayValues){
                int lengthKey = arrayKey.length();
//                int lengthValues = arrayValues.length();
                for (int i =0;i<lengthKey;i++){
                    hashMap.put(arrayKey.getString(i),arrayValues.getString(i));
                }
            }

            if (null != arrayKeysNull){
                for (int i=0; i<arrayKeysNull.length();i++){
                    hashMap.put(arrayKeysNull.getString(i),null);
                }
            }
        } catch (Throwable e) {
            e.printStackTrace();
        }
        JAnalyticsBridge.log("JsonUtil","unityJsonToMap:",hashMap);
        return hashMap;
    }


    public static String toJson(Map<String,String> jsonMap){
        StringBuffer buffer = new StringBuffer();
        buffer.append("{");

        if (!jsonMap.isEmpty()){
            Set<Map.Entry<String, String>> entries = jsonMap.entrySet();
            for (Map.Entry<String, String> e:
                    entries) {
                String key = e.getKey();
                String value = e.getValue();
                buffer.append("\"");
                buffer.append(key);
                buffer.append("\"");
                buffer.append(":");
                buffer.append("\"");
                buffer.append(value);
                buffer.append("\"");
                buffer.append(",");
            }
            buffer.deleteCharAt(buffer.length()-1);
        }

        buffer.append("}");
        return buffer.toString();
    }
}
