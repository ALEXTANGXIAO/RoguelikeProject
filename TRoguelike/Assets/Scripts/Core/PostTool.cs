using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace TGame
{
    public class PostTool : MonoBehaviour
    {
        public static string url = "http://openapi.tuling123.com/openapi/api/v2";
        public static string apiKey = "e0e98d1c14614a1fb61eafb4c41d588e";

        private void Start()
        {
            StartCoroutine(Post());
        }

        IEnumerator Post()
        {
            //HEADER DATA
            Dictionary<string, string> header = new Dictionary<string, string>();
            header["Content-Type"] = "application/json";
            header["charset"] = "utf-8";
            header.Add("sign", "test sign");
            //POST DATA
            JsonData data = new JsonData();
            data["reqType"] = "0";
            data["perception"] = new JsonData();
            data["perception"]["inputText"] = new JsonData();
            data["perception"]["inputText"]["text"] = "你好啊";
            data["inputImage"] = new JsonData();
            data["inputImage"]["url"] = "imageUrl";
            data["selfInfo"] = new JsonData();
            data["selfInfo"]["location"] = new JsonData();
            data["selfInfo"]["location"]["city"] = "深圳";
            data["selfInfo"]["location"]["province"] = "深圳";
            data["selfInfo"]["location"]["street"] = "宝安区";
            data["userInfo"] = new JsonData();
            data["userInfo"]["apiKey"] = apiKey;
            data["userInfo"]["userId"] = "Tang";


            byte[] postdata = Encoding.UTF8.GetBytes(data.ToJson());
            WWW _w = new WWW(url, postdata, header);
            yield return _w;
            //Debug.Log(_w.text);   //拿到JSON


            string jsonstr = _w.text;
            JsonData jsonData = JsonMapper.ToObject(jsonstr);
            if (jsonData != null)
            {
                JsonData results = jsonData["results"];
                foreach(JsonData temp in results)
                {
                    JsonData text = temp["values"]["text"];
                    Debug.Log(text.ToString());
                }
            }
        }
    }
}