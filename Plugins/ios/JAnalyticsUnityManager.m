//
//  JAnalyticsUnityManager.m
//  JAnalyticsDemo
//
//  Created by 韦瑞杨 on 2019/7/2.
//  Copyright © 2019 jpush. All rights reserved.
//

#import "JAnalyticsUnityManager.h"
#import "JANALYTICSService.h"



#pragma mark - Utility Function

#if defined(__cplusplus)
extern "C" {
#endif
    extern void       UnitySendMessage(const char* obj, const char* method, const char* msg);
    extern NSString*  CreateNSString (const char* string);
    extern id         APNativeJSONObject(NSData *data);
    extern NSData *   APNativeJSONData(id obj);
#if defined(__cplusplus)
}
#endif



#if defined(__cplusplus)
extern "C" {
#endif
    
    //  static char *MakeHeapString(const char *string) {
    //    if (!string){
    //      return NULL;
    //    }
    //    char *mem = static_cast<char*>(malloc(strlen(string) + 1));
    //    if (mem) {
    //      strcpy(mem, string);
    //    }
    //    return mem;
    //  }
    
    NSString *CreateNSString (const char *string) {
        return [NSString stringWithUTF8String:(string ? string : "")];
    }
    
    
    NSMutableDictionary * APNativeJSONObjectUnityMap(NSData *data) {
        if (!data) {
            return nil;
        }
        
        NSError *error = nil;
        NSDictionary* retId = [NSJSONSerialization JSONObjectWithData:data options:0 error:&error];
        
        if (error) {
            NSLog(@"%s trans data to obj with error: %@", __func__, error);
            return nil;
        }
        NSMutableDictionary *unityMap = [[NSMutableDictionary alloc]init];
        
        //    {"keys":["accountID","name"],"values":["AccountAccount","nnnnn"],"keysNull":[]}
        NSArray *arrayKey  = retId[@"keys"];
        NSArray *arrayValues  = retId[@"values"];
        NSArray *arrayKeysNull  = retId[@"keysNull"];
        
        if (nil != arrayKey) {
            for (int i=0; i<arrayKey.count; i++) {
                NSString* vaV = [arrayValues objectAtIndex:i];
                NSString* vaK =  [arrayKey objectAtIndex:i];
                [unityMap setValue:vaV forKey:vaK];
            }
        }
        if (nil != arrayKeysNull) {
            for (int i=0; i<arrayKeysNull.count; i++) {
                NSString* vaK  = [arrayKeysNull objectAtIndex:i];
                [unityMap setValue:[NSNull null] forKey:vaK];
            }
        }
        
        return unityMap;
    }
    
    id APNativeJSONObject(NSData *data) {
        if (!data) {
            return nil;
        }
        
        NSError *error = nil;
        id retId = [NSJSONSerialization JSONObjectWithData:data options:0 error:&error];
        
        if (error) {
            NSLog(@"%s trans data to obj with error: %@", __func__, error);
            return nil;
        }
        
        return retId;
    }
    
    NSData *APNativeJSONData(id obj) {
        NSError *error = nil;
        NSData *data = [NSJSONSerialization dataWithJSONObject:obj options:0 error:&error];
        if (error) {
            NSLog(@"%s trans obj to data with error: %@", __func__, error);
            return nil;
        }
        return data;
    }
    
    NSString *messageAsDictionary(NSDictionary * dic) {
        NSData *data = APNativeJSONData(dic);
        return [[NSString alloc]initWithData:data encoding:NSUTF8StringEncoding];
    }
    
    
    
    
    
    void sendAccountBack(NSInteger err, NSString *msg,int sequence, NSString *gameObjectNameBack, NSString *gameObjectMethodBack){
        if (err) {
            NSLog(@"detach ERR:%ld|%@", err, msg);
        }else {
            NSLog(@"detach success:%ld",err);
        }
        
        
        NSLog(@"sendAccountBack,%d,%@,%@",sequence,gameObjectNameBack,gameObjectMethodBack);
        
        if (![gameObjectNameBack length]) {
            return;
        }
        
        if (![gameObjectMethodBack length]) {
            return;
        }
        
        NSMutableDictionary *dic = [[NSMutableDictionary alloc] init];
        [dic setObject:[NSNumber numberWithInteger:sequence] forKey:@"sequence"];
        [dic setValue:[NSNumber numberWithInteger:err] forKey:@"code"];
        [dic setValue:msg forKey:@"msg"];
        UnitySendMessage([gameObjectNameBack UTF8String], [gameObjectMethodBack UTF8String], messageAsDictionary(dic).UTF8String);
    }
    
    
    /// <summary>
    /// 地理位置上报
    /// </summary>
    /// <param name="latitude">纬度.</param>
    /// <param name="longitude">经度.</param>
    void _setLatitude(double latitude, double longitude){
        [JANALYTICSService setLatitude:latitude longitude:longitude];
    }
    
    
    /// <summary>
    /// 开启Crash日志收集,默认是关闭状态.
    /// </summary>
    void _crashLogON(){
        [JANALYTICSService crashLogON];
    }
    
    
    /// <summary>
    /// 设置是否开启 Debug 模式。
    /// <para>Debug 模式将会输出更多的日志信息，建议在发布时关闭。建议在init前调用。</para>
    /// </summary>
    /// <param name="enable">true: 开启；false: 关闭。</param>
    void _setDebug(bool enable){
        [JANALYTICSService setDebug:enable];
    }
    
    
    
    
    /// <summary>
    /// 统计page开始.
    /// </summary>
    /// <param name="pageName">被统计页面的名字.</param>
    void _startLogPageView(const char *pageName){
        NSString *nsPageName = CreateNSString(pageName);
        if (![nsPageName length]) {
            return;
        }
        [JANALYTICSService startLogPageView:nsPageName];
    }
    
    /// <summary>
    /// 统计page结束.
    /// </summary>
    /// <param name="pageName">被统计页面的名字.</param>
    void _stopLogPageView(const char *pageName){
        NSString *nsPageName = CreateNSString(pageName);
        if (![nsPageName length]) {
            return;
        }
        [JANALYTICSService stopLogPageView:nsPageName];
    }
    
    
    
    
    void setExtraEvent(JANALYTICSEventObject *event ,NSString *nsExtMap){
        if ([nsExtMap length]) {
            NSData *dataExtJson = [nsExtMap dataUsingEncoding:NSUTF8StringEncoding];
            NSMutableDictionary *dictExt = APNativeJSONObjectUnityMap(dataExtJson);
            NSArray * arrayKey =  [dictExt allKeys];
            NSMutableDictionary * extra = [[NSMutableDictionary alloc]init];
            for (NSString * key in arrayKey) {
                if ([dictExt[key] isKindOfClass:[NSNull class]]) {
                    [extra setValue:nil forKey:key];
                }else{
                    [extra setValue:dictExt[key] forKey:key];
                }
            }
            event.extra = extra;
        }
    }
    
    /// <summary>
    /// 购买事件上报
    /// </summary>
    /// <param name="purchaseGoodsid">商品id.</param>
    /// <param name="purchaseGoodsName">商品名称.</param>
    /// <param name="purchasePrice">购买价格(非空)e.</param>
    /// <param name="purchaseSuccess">购买是否成功(非空).</param>
    /// <param name="purchaseCurrencyUnity">货币类型，Currency.CNY 或 Currency.USD.</param>
    /// <param name="purchaseGoodsType">商品类型.</param>
    /// <param name="purchaseGoodsCount">商品数量.</param>
    /// <param name="extDic">扩展参数.</param>
    void _onPurchaseEvent(const char *purchaseGoodsid, const char * purchaseGoodsName, double purchasePrice,
                          bool purchaseSuccess, int purchaseCurrencyUnity, const char * purchaseGoodsType, int purchaseGoodsCount,
                          const char * extMap){
        NSString *nsPurchaseGoodsid = CreateNSString(purchaseGoodsid);
        NSString *nsPurchaseGoodsName = CreateNSString(purchaseGoodsName);
        NSString *nsPurchaseGoodsType = CreateNSString(purchaseGoodsType);
        NSString *nsExtMap = CreateNSString(extMap);
        
        
        
        
        JANALYTICSPurchaseEvent * event = [[JANALYTICSPurchaseEvent alloc] init];
        
        event.goodsID =nsPurchaseGoodsid;
        event.goodsName = nsPurchaseGoodsName;
        event.price = purchasePrice;
        event.success = purchaseSuccess;
        
        JANALYTICSPurchaseCurrency currency =JANALYTICSCurrencyCNY;
        if(1 == purchaseCurrencyUnity){
            currency = JANALYTICSCurrencyUSD;
        }
        event.currency = currency;
        event.goodsType = nsPurchaseGoodsType;
        event.quantity = purchaseGoodsCount;
        setExtraEvent(event,nsExtMap);
        
        [JANALYTICSService eventRecord:event];
    }
    
    /// <summary>
    /// 浏览事件上报.
    /// </summary>
    /// <param name="browseId">浏览内容id.</param>
    /// <param name="browseName">内容名称(非空).</param>
    /// <param name="browseType">内容类型.</param>
    /// <param name="browseDuration">浏览时长，单位秒.</param>
    /// <param name="extDic">扩展参数.</param>
    void _onBrowseEvent(const char *  browseId, const char *  browseName, const char *  browseType, long browseDuration,
                        const char *  extMap){
        NSString *nsBrowseId = CreateNSString(browseId);
        NSString *nsBrowseName = CreateNSString(browseName);
        NSString *nsBrowseType = CreateNSString(browseType);
        NSString *nsExtMap = CreateNSString(extMap);
        
        
        JANALYTICSBrowseEvent * event = [[JANALYTICSBrowseEvent alloc] init];
        
        event.contentID =nsBrowseId;
        event.name = nsBrowseName;
        event.type = nsBrowseType;
        event.duration = browseDuration;
        
        setExtraEvent(event,nsExtMap);
        
        [JANALYTICSService eventRecord:event];
        
        //        NSLog(@"_onBrowseEvent %@,%@,%@,%ld,%@",nsBrowseId,nsBrowseName,nsBrowseType,browseDuration,nsExtMap);
    }
    
    /// <summary>
    /// 注册事件上报.
    /// </summary>
    /// <param name="registerMethod">注册方式(非空).</param>
    /// <param name="registerSuccess">注册是否成功(非空).</param>
    /// <param name="extDic">扩展参数.</param>
    void _onRegisterEvent(const char *   registerMethod, bool registerSuccess,
                          const char *   extMap){
        
        NSString *nsRegisterMethod = CreateNSString(registerMethod);
        NSString *nsExtMap = CreateNSString(extMap);
        
        JANALYTICSRegisterEvent * event = [[JANALYTICSRegisterEvent alloc] init];
        
        event.success = registerSuccess;
        
        event.method = nsRegisterMethod;
        
        setExtraEvent(event,nsExtMap);
        
        [JANALYTICSService eventRecord:event];
        //        NSLog(@"_onRegisterEvent %@,%d,%@",nsRegisterMethod,registerSuccess,nsExtMap);
    }
    
    /// <summary>
    /// 登录事件上报.
    /// </summary>
    /// <param name="loginMethod">登录方式(非空).</param>
    /// <param name="loginSuccess">登录是否成功(非空).</param>
    /// <param name="extDic">扩展参数.</param>
    void _onLoginEvent(const char *    loginMethod, bool loginSuccess,
                       const char *    extMap){
        
        NSString *nsLoginMethod = CreateNSString(loginMethod);
        NSString *nsExtMap = CreateNSString(extMap);
        
        JANALYTICSLoginEvent * event = [[JANALYTICSLoginEvent alloc] init];
        
        event.success = loginSuccess;
        
        event.method = nsLoginMethod;
        
        setExtraEvent(event,nsExtMap);
        
        [JANALYTICSService eventRecord:event];
        
        //        NSLog(@"_onLoginEvent %@,%d,%@",nsLoginMethod,loginSuccess,nsExtMap);
    }
    
    /// <summary>
    /// 计算事件上报.
    /// </summary>
    /// <param name="eventId">事件Id(非空).</param>
    /// <param name="eventValues">事件的值(非空).</param>
    /// <param name="extDic">扩展参数.</param>
    void _onCalculateEvent(const char *     eventId, double eventValues,
                           const char *     extMap){
        
        NSString *nsEventId = CreateNSString(eventId);
        NSString *nsExtMap = CreateNSString(extMap);
        
        
        JANALYTICSCalculateEvent * event = [[JANALYTICSCalculateEvent alloc] init];
        
        event.eventID = nsEventId;
        
        event.value = eventValues;
        
        setExtraEvent(event,nsExtMap);
        
        [JANALYTICSService eventRecord:event];
        
        //        NSLog(@"_onCalculateEvent %@,%f,%@",nsEventId,eventValues,nsExtMap);
    }
    
    /// <summary>
    /// 计数事件上报.
    /// </summary>
    /// <param name="eventId">事件Id(非空).</param>
    /// <param name="extDic">扩展参数.</param>
    void _onCountEvent(const char *      eventId,const char *      extMap){
        NSString *nsEventId = CreateNSString(eventId);
        NSString *nsExtMap = CreateNSString(extMap);
        
        JANALYTICSCountEvent * event = [[JANALYTICSCountEvent alloc] init];
        
        event.eventID = nsEventId;
        
        setExtraEvent(event,nsExtMap);
        
        [JANALYTICSService eventRecord:event];
        
        //        NSLog(@"_onCountEvent %@,%@",nsEventId,nsExtMap);
    }
    
    
    
    static NSString * accountID_account_key = @"accountID";
    static NSString * creationTime_account_key = @"creationTime";
    static NSString * name_account_key = @"name";
    static NSString * sex_account_key = @"sex";
    static NSString * paid_account_key = @"paid";
    static NSString * birthdate_account_key = @"birthdate";
    static NSString * phone_account_key = @"phone";
    static NSString * email_account_key = @"email";
    static NSString * weiboID_account_key = @"weiboID";
    static NSString * wechatID_account_key = @"wechatID";
    static NSString * qqID_account_key = @"qqID";
    /// <summary>
    /// 开发者可以为用户增加账户信息，使统计数据可以以账户维度做统计分析.
    /// </summary>
    /// <param name="account">账号信息.</param>
    /// <param name="sequence">唯一请求识别ID，和结果一起反回.</param>
    /// <param name="gameObjectNameBack">游戏对象名 用于反回结果.</param>
    /// <param name="gameObjectMethodBack">游戏对象中的方法名，此方法只能有一个string 参数， 用于反回结果.</param>
    void _identifyAccount(const char * accountMapJson, const char * extMapJson,
                          int sequence, const char * gameObjectNameBack, const char * gameObjectMethodBack){
        
        NSString *nsAccountMapJson = CreateNSString(accountMapJson);
        if (![nsAccountMapJson length]) {
            return;
        }
        
        NSString *nsGameObjectNameBack = CreateNSString(gameObjectNameBack);
        NSString *nsGameObjectMethodBack = CreateNSString(gameObjectMethodBack);
        NSString *nsExtMapJson = CreateNSString(extMapJson);
        
        NSData *dataAccountJson = [nsAccountMapJson dataUsingEncoding:NSUTF8StringEncoding];
        NSMutableDictionary *dictAccount = APNativeJSONObjectUnityMap(dataAccountJson);
        
        JANALYTICSUserInfo * userinfo = [[JANALYTICSUserInfo alloc] init];
        
        NSString *  accountID =  dictAccount[accountID_account_key];
        NSString *  creationTime =  dictAccount[creationTime_account_key];
        NSString *  name =  dictAccount[name_account_key];
        NSString *  sex =  dictAccount[sex_account_key];
        NSString *  paid =  dictAccount[paid_account_key];
        NSString *  birthdate =  dictAccount[birthdate_account_key];
        NSString *  phone =  dictAccount[phone_account_key];
        NSString *  email =  dictAccount[email_account_key];
        NSString *  weiboID =  dictAccount[weiboID_account_key];
        NSString *  wechatID =  dictAccount[wechatID_account_key];
        NSString *  qqID =  dictAccount[qqID_account_key];
        
        if(nil != accountID){
            userinfo.accountID = accountID;
        }
        
        if(nil != creationTime){
            userinfo.creationTime = [creationTime longLongValue];
        }
        
        if(nil != name){
            userinfo.name = name;
        }
        
        if(nil != sex){
            JANALYTICSSex sexJa = JANALYTICSPaidUnknown;
            int sexInt = [sex intValue];
            if (1 == sexInt) {
                sexJa = JANALYTICSSexMale;
            }else if (2==sexInt){
                sexJa = JANALYTICSSexFemale;
            }
            userinfo.sex = sexJa;
        }
        
        if(nil != paid){
            JANALYTICSPaid paidJa =JANALYTICSPaidUnknown;
            int paidInt = [paid intValue];
            if (1 == paidInt) {
                paidJa =JANALYTICSPaidPaid;
            }else if(2 == paidInt){
                paidJa = JANALYTICSPaidUnpaid;
            }
            userinfo.paid = paidJa;
        }
        
        if(nil != birthdate){
            userinfo.birthdate = birthdate;
        }
        
        if(nil != phone){
            userinfo.phone = phone;
        }
        
        if(nil != email){
            userinfo.email = email;
        }
        
        if(nil != weiboID){
            userinfo.weiboID = weiboID;
        }
        
        if(nil != wechatID){
            userinfo.wechatID = wechatID;
        }
        
        if(nil != qqID){
            userinfo.qqID = qqID;
        }
        
        
        if ([nsExtMapJson length]) {
            NSData *dataExtJson = [nsExtMapJson dataUsingEncoding:NSUTF8StringEncoding];
            NSMutableDictionary *dictExt = APNativeJSONObjectUnityMap(dataExtJson);
            NSArray * arrayKey =  [dictExt allKeys];
            for (NSString * key in arrayKey) {
                if ([dictExt[key] isKindOfClass:[NSNull class]]) {
                    [userinfo setExtraObject:nil forKey:key];
                }else{
                    [userinfo setExtraObject:dictExt[key] forKey:key];
                }
            }
        }
        
        [JANALYTICSService identifyAccount:userinfo with:^(NSInteger err, NSString *msg) {
            sendAccountBack(err,msg,sequence,nsGameObjectNameBack,nsGameObjectMethodBack);
        }];
    }
    
    /// <summary>
    /// 解绑当前用账户信息
    /// </summary>
    /// <param name="sequence">唯一请求识别ID，和结果一起反回.</param>
    /// <param name="gameObjectNameBack">游戏对象名 用于反回结果.</param>
    /// <param name="gameObjectMethodBack">游戏对象中的方法名，此方法只能有一个string 参数， 用于反回结果.</param>
    void _detachAccount(int sequence, const char *gameObjectNameBack, const char *gameObjectMethodBack){
        NSString *nsGameObjectNameBack = CreateNSString(gameObjectNameBack);
        NSString *nsGameObjectMethodBack = CreateNSString(gameObjectMethodBack);
        
        [JANALYTICSService detachAccount:^(NSInteger err, NSString *msg) {
            sendAccountBack(err,msg,sequence,nsGameObjectNameBack,nsGameObjectMethodBack);
        }];
    }
    
    
    
    /// <summary>
    /// 设置统计上报的自动周期，未调用前默认即时上报.
    /// </summary>
    /// <param name="period">周期，单位秒，最小10秒，最大1天，超出范围会打印调用失败日志。传0表示统计数据即时上报.</param>
    void _setFrequency(int period){
        [JANALYTICSService setFrequency:period];
    }
    
    
    
#if defined(__cplusplus)
}
#endif






@implementation JAnalyticsUnityManager

@end
