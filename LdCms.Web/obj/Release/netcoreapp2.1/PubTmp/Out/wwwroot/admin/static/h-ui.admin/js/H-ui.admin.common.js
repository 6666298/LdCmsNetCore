/* 
 * param 将要转为URL参数字符串的对象 
 * key URL参数字符串的前缀 
 * encode true/false 是否进行URL编码,默认为true 
 *  
 * return URL参数字符串 
 */
function urlEncode(param, key, encode) {
    if (param == null) return '';
    var paramStr = '';
    var t = typeof (param);
    if (t == 'string' || t == 'number' || t == 'boolean') {
        paramStr += '&' + key + '=' + ((encode == null || encode) ? encodeURIComponent(param) : param);
    } else {
        for (var i in param) {
            var k = key == null ? i : key + (param instanceof Array ? '[' + i + ']' : '.' + i);
            paramStr += urlEncode(param[i], k, encode);
        }
    }
    return paramStr;
}

function urlEncodes(param) {
    var result = urlEncode(param, null, true);
    return result.substring(1, result.lngLen);
}


function isNull(obj) {
    var exp = obj;
    if (!exp && typeof (exp) != "undefined" && exp != 0) {
        return true;
    } else {
        return false;
    }
}

/*
 * paraName 等找参数的名称
 * 调用方法：GetUrlParam("id"); 
 * 举例说明：
 * 假如当网页的网址有这样的参数 test.htm ? id = 896 & s=q & p=5，则调用 GetUrlParam("p") ，返回 5。
*/
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return "";
} 