# 通用 API

以下除事件监听之外的方法都基于 `JAnalyticsBinding` 对象进行调用。


+ ***SetDebugMode(boolean enable)***
+ 接口说明：
+ 设置是否开启debug模式。true则会打印更多的日志信息。建议在init接口之前调用。
+ 参数说明：
+ enable：debug开关 

<a name="pageflow"></a>
##页面流统计 API

+ ***OnPageStart(String pageName)***
+ 接口说明：
+ 页面启动接口。在页面的相关生命周期内调用，和onPageEnd需要成对调用，在Unity中开发者根据业务等自定义页面开始和结束。
+ 参数说明：
+ pageName：页面名称 


+ ***OnPageEnd(String pageName)***
+ 接口说明：
+ 页面结束接口。在页面的相关生命周期内调用，和onPageStart需要成对调用，在Unity中开发者根据业务等自定义页面开始和结束。
+ 参数说明：
+ pageName：页面名称 


**关于页面流做如下说明：**

1. 在android 4.0及以上版本，默认上报activity页面流。android 4.0以下需要开发者自己调用onPageStart和onPageEnd方法上报activity页面流。

2. 在Unity中开发者根据业务等自定义页面开始和结束。

##自定义事件统计 API

+ ***OnEvent(Event event)***
+ 接口说明：
+ 自定义事件。通过传入不同的事件模型来进行各种事件的统计，具体的事件模型请查看事件模型介绍
+ 参数说明：
+ event:事件模型，支持CountEvent(计数事件)、CalculateEvent(计算事件)、RegisterEvent(注册事件)、LoginEvent(登录事件)、BrowseEvent(浏览事件)、PurchaseEvent(购买事件)

**关于自定义事件做如下说明：**

1. 字符串字段（key与 value）限制大小不超过256字节，超过限制的key或value该事件将会被丢弃.
2. 自定义键值对数目不能超过10个，超过10个限制该事件将会被丢弃.

调用示例：

~~~
CountEvent cEvent = new CountEvent("eventId","eventName");
JAnalyticsBinding.onEvent(context, cEvent);
~~~
<a name="times"></a>
##计数事件模型
+ ***CountEvent***

该模型是自定义计数事件模型，可以设置参数进行数据上报。

参数说明：

| 参数名称 | 参数类型 | 参数说明 |
|:-------:|:------:|:-------:|
| eventId | String |事件Id(非空)|
| extMap | Map | 扩展参数 |

调用示例:

~~~
CountEvent cEvent = new CountEvent("test1_event_id");
cEvent.addKeyValue("key1","value1").addKeyValue("key2","value2");
~~~

**注意：**

自定义计数事件模型中扩展参数中不能使用以下 key 值：
event_id
此类 key 已被模型使用，如果使用则会导致统计到的数据不准确.
<a name="count"></a>
##计算事件模型
+ ***CalculateEvent***

该模型是自定义计算事件模型，计算事件会通过相同的事件不同的值进行累加，可以设置参数进行数据上报。

参数说明：

|参数名称|参数类型|参数说明|
|:------:|:----:|:-----:|
|eventId|String|事件Id(非空)|
|eventValue| double |事件的值(非空)|
|extMap|Map|扩展参数|

调用示例:

~~~
CalculateEvent cEvent = new CalculateEvent("test2_event_id","test2_event_value");
cEvent.addKeyValue("key1","value1").addKeyValue("key2","value2");
~~~

**注意：**

自定义计算事件模型中扩展参数中不能使用以下 key 值：
event_id
event_value
此类 key 已被模型使用，如果使用则会导致统计到的数据不准确.
<a name="login"></a>
##登录事件模型
+ ***LoginEvent***

该模型是登录事件模型，可以设置参数进行数据上报。

参数说明：

|参数名称|参数类型|参数说明|
|:-----:|:-----:|:----:|
|loginMethod|    String|登录方式(非空)|
|loginSuccess|boolean|登录是否成功(非空)|
|extMap|Map|扩展参数|

调用示例:

~~~
LoginEvent lEvent = new LoginEvent("qq",true);
lEvent.addKeyValue("key1","value1").addKeyValue("key2","value2");
~~~

**注意：**

登录事件模型中扩展参数中不能使用以下 key 值：
login_method
login_success
此类 key 已被模型使用，如果使用则会导致统计到的数据不准确.
<a name="register"></a>
##注册事件模型
+ ***RegisterEvent***

该模型是注册事件模型，可以设置参数进行数据上报。

参数说明：

|参数名称|参数类型|参数说明|
|:-----:|:----:|:-----:|
|registerMethod|    String    |注册方式(非空)|
|registerSuccess|boolean|注册是否成功(非空)|
|extMap|Map|扩展参数|

调用示例:

~~~
RegisterEvent rEvent = new RegisterEvent("sina",true);
rEvent.addKeyValue("key1","value1").addKeyValue("key2","value2");
~~~

**注意：**

注册事件模型中扩展参数中不能使用以下 key 值:
register_method
register_success
此类 key 已被模型使用，如果使用则会导致统计到的数据不准确.
<a name="content"></a>
##浏览事件模型
+ ***BrowseEvent***

该模型是浏览事件模型，可以设置参数进行数据上报。

参数说明：

|参数名称|参数类型|参数说明|
|:-----:|:----:|:-----:|
|browseId|String    |浏览内容id|
|browseName|String|内容名称(非空)|
|browseType|String|内容类型|
|browseDuration|long|浏览时长，单位秒|
|extMap|Map|扩展参数|

调用示例:

~~~
BrowseEvent bEvent = new BrowseEvent("browse_id","深圳热点新闻","news",30);
bEvent.addKeyValue("key1","value1").addKeyValue("key2","value2");
~~~

**注意：**

浏览事件模型中扩展参数中不能使用以下 key 值：
browse_content_id
browse_name
browse_type
browse_duration
此类 key 已被模型使用，如果使用则会导致统计到的数据不准确.
<a name="purchase"></a>
##购买事件模型
+ ***PurchaseEvent***

该模型是购买事件模型，可以设置参数进行数据上报。

参数说明：

|参数名称|参数类型|参数说明|
|:-----:|:----:|:-----:|
|purchaseGoodsid|String    |商品id|
|purchaseGoodsName|String|    商品名称|
|purchasePrice|double|购买价格(非空)|
|purchaseSuccess|boolean|购买是否成功(非空)|
|purchaseCurrency|Currency|货币类型，一个枚举类|
|purchaseGoodsType|String|商品类型|
|purchaseGoodsCount|int    |商品数量|
|extMap|Map|扩展参数|

调用示例:

~~~
PurchaseEvent pEvent = new PurchaseEvent("goodsId","篮球",300,true,Currency.CNY,"sport",1);
pEvent.addKeyValue("key1","value1").addKeyValue("key2","value2");
~~~

**注意：**

购买事件模型中扩展参数中不能使用以下 key 值：
purchase_goods_id
purchase_goods_name
purchase_price
purchase_currency
purchase_goods_type
purchase_quantity
此类 key 已被模型使用，如果使用则会导致统计到的数据不准确.

##统计上报周期API 

+ ***SetAnalyticsReportPeriod(int period)***
+ 接口说明：
+ 设置统计上报的自动周期，未调用前默认即时上报
+ 参数说明：
+ period：周期，单位秒，最小10秒，最大1天，超出范围会打印调用失败日志。传0表示统计数据即时上报



+ ***IdentifyAccount(Account account,JAnalyticsCallBack callBack);***
+ 接口说明：
+ 开发者可以为用户增加账户信息，使统计数据可以以账户维度做统计分析
+ 参数说明：
+ account：账号信息，具体可看文档中的‘账户维度模型介绍 ’ 
+ callBack：用与接收反回结果，具体可看文档中的‘账户维度模型介绍 ’

+ ***DetachAccount(JAnalyticsCallBack callBack);***
+ 接口说明：
+ 解除账户维度统计与IdentifyAccount(Account account,JAnalyticsCallBack callBack);
+ 参数说明：
+ callBack：用与接收反回结果，具体可看文档中的‘账户维度模型介绍 ’



##账户维度模型介绍  

开发者可以为用户增加账户信息，使统计数据可以以账户维度做统计分析
现开发的属性有：

|中文名|英文名|类型|鉴权/备注|
|:-----:|:----:|:-----:|:-----:|
|账号ID|accountID|String    ||
|账号创建时间|creationTime|long|时间戳|
|姓名|name|String||
|性别|sex|int|0未知 1男 2女/不能为其他数字，默认为0|
|是否付费|paid|int|0未知 1是 2否/不能为其他数字，默认为0|
|出生年月|birthdate|long|yyyyMMdd格式校验|
|手机号码|phone|String|手机号码校验|
|电子邮件|email|String|邮箱格式校验|
|新浪微博ID|weiboID|String||
|微信ID|wechatID|String||
|QQ ID|qqID|String||
|自定义维度|extra    |key-value|key只能为字符串，value只能为字符串或数字类型或null类型； 当value设置为空类型时，将该key从服务器上删除 key不能使用极光内部namespace（符号$）|

具体使用方法，是先调用cn.jiguang.analytics.android.api.Account设置属性，  
再调用JAnalyticsInterface.identifyAccount(context, account, callback)登记账户信息  
也可以只设置部分属性，再次调用identifyAccount修改账户信息  
调用示例：

~~~
Account account = new Account("account001");    //account001为账号id
account.setCreationTime(1513749859L);        //账户创建的时间戳
account.setName("张三");
account.setSex(1);
account.setPaid(1);
account.setBirthdate("19880920");       //"19880920"是yyyyMMdd格式的字符串
account.setPhone("13800000000");
account.setEmail("support@jiguang.cn");
account.setExtraAttr("attr1","value1");  //key如果为空，或者以极光内部namespace(符号$)开头，会设置失败并打印日志

int sequence = callbackId++;  //唯一标识，有回调时一起带回
string gameObjectNameBack = gameObject.name; //回调反回给的 gameObject名字
string gameObjectMethodBack = "AccountCallBack";// 回调反回给的 gameObject中方法句的名称，而且这个方法一定是只有一个string 参数
JAnalyticsCallBack callBack = new JAnalyticsCallBack(sequence, gameObjectNameBack, gameObjectMethodBack);//结果会回调gameObjectNameBack中的gameObjectMethodBack方法

JAnalyticsBinding.IdentifyAccount(account, callBack);
~~~

JAnalyticsCallBack是回调方法，可以根据返回的code和msg获取调用成功/失败的信息

###错误码
|code|message|备注|
|:-----:|:----:|:-----:|
|0|调用成功||
|1001|account_id can not be empty|accountID为关键参数，不能填写null或""|
|1002|detach failed because account_id is empty|当前没有绑定accountID时调用了解绑接口|
|1003|operation is too busy|10s内请求频率不能超过30次
|1004|account_id is too long, please make it less than 255 characters|accountID长度不能超过255字符|
|1005|failed, please call JAnalyticsInterface.init(context) first|SDK尚未初始化，应先调用init()方法|
|1101|the value of $sex should be in [0,2]|0未知 1男 2女/不能为其他数字，默认为0|
|1101|the value of $birthdate should be date as yyyyMMdd|yyyyMMdd格式校验|
|1101|the value of $paid should be in [0,2]|0未知 1是 2否/不能为其他数字，默认为0|
|1101|the value of $phone is NOT a phone number|电话号码格式校验（含国际号码）|
|1101|the value of $email is NOT email address|邮箱格式校验|
|1101|the key={key} in extra is invalid|自定义属性key不能为空，不能使用极光内部namespace(符号$)|
|1101|the value of {key} in extra should be String or Number|自定义属性value只能为字符串或数字类型或null类型|




如果要解绑当前用户信息，调用JAnalyticsBinding.DetachAccount(callback);  
调用示例：

~~~
int sequence = callbackId++;  //唯一标识，有回调时一起带回
string gameObjectNameBack = gameObject.name; //回调反回给的 gameObject名字
string gameObjectMethodBack = "AccountCallBack";// 回调反回给的 gameObject中方法句的名称，而且这个方法一定是只有一个string 参数
JAnalyticsCallBack callBack = new JAnalyticsCallBack(sequence, gameObjectNameBack, gameObjectMethodBack);//结果会回调gameObjectNameBack中的gameObjectMethodBack方法

JAnalyticsBinding.DetachAccount(callBack);
~~~
