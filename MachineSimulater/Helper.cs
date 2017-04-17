using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 

namespace MachineSimulater
{
    class Helper
    {
        /// <summary>
        /// POST数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="result"></param>
        /// <param name="ContentType"></param>
        public static void HttpPost(string url, string postData, out JObject result, string ContentType = "application/json")
        {
            System.GC.Collect();     //垃圾回收，回收没有正常关闭的http连接
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            byte[] data;
            result = new JObject();
            result.Add("success", false);
            result.Add("resmsg", "");
            try
            {
                //'设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 2000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = ContentType;
                data = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = data.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //'获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //'获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string resultstr = sr.ReadToEnd().Trim();
                sr.Close();
                result = (JObject)JsonConvert.DeserializeObject(resultstr);
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                System.Threading.Thread.ResetAbort();
                result["resmsg"] = ex.StackTrace;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    string err = "StatusCode : " + (e.Response as HttpWebResponse).StatusCode;
                    err += "StatusDescription : " + (e.Response as HttpWebResponse).StatusDescription;
                    result["resmsg"] = err;
                }
                else
                {
                    result["resmsg"] = e.StackTrace;
                }
            }
            catch (Exception e)
            {
                result["resmsg"] = "HttpService" + e.StackTrace;
            }
            finally
            {
                //'关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }

        }

        public static JObject PostMachine(Machine _machine, string NodeUrl)
        {
            Dictionary<string, object> inputs = new Dictionary<string, object>();
            inputs.Add("machine", _machine.Name);
            inputs.Add("paraslist", _machine.ParameterList);
            inputs.Add("currtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            inputs.Add("modeluenum", _machine.Number);
            JObject result;
            HttpPost(NodeUrl + "/opc", JsonConvert.SerializeObject(inputs), out result);
            bool success = (bool)result["success"];
            string errmsg = (string)result["resmsg"];


            return result;
        }
    }
}
