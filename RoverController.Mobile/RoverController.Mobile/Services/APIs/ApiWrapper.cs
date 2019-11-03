using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoverController.Lib;
using RoverController.Mobile.DTOs;
using RoverController.Mobile.Misc;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;

namespace RoverController.Mobile.Services.APIs
{
    public static class ApiWrapper<T> where T : class
    {
        private static HttpClient WebApiClient = null;
        private static readonly object threadlock = new object();
        private static string ApiBaseUrl = @"https://rovercontroller.levitica.ca";

        public static string Headers { get; set; }

        public static async Task<Tuple<T, string>> Delete(string url, bool requiresToken = true)
        {
            try
            {
                lock (threadlock)
                {
                    if (WebApiClient == null)
                    {
                        WebApiClient = new HttpClient(new NativeMessageHandler(false, new TLSConfig()
                        {
                            DangerousAcceptAnyServerCertificateValidator = true,
                        })
                        {
                            Timeout = new TimeSpan(0, 0, 30),
                            DisableCaching = true,
                        })
                        {
                            BaseAddress = new Uri(ApiBaseUrl)
                        };
                    }
                }

                //WebApiClient.BaseAddress = new Uri(ApiUrlsBase.BaseUrl);
                url = $"{WebApiClient.BaseAddress}{url}";

                if (requiresToken)
                {
                    WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync(SecureStorageProperties.AccessToken));
                }

                var response = await WebApiClient.DeleteAsync(url);
                var responseJson = await response.Content.ReadAsStringAsync();

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        if (responseJson.IsEmpty())
                        {
                            ////return default(Tuple<T, string>);
                        }

                        var apiResult = JsonConvert.DeserializeObject<T>(responseJson);

                        return Tuple.Create<T, string>(apiResult, null);
                    }
                    else
                    {
                        try
                        {
                            var apiError = JsonConvert.DeserializeObject<string>(responseJson);
                            return Tuple.Create<T, string>(null, apiError);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseDTO>(responseJson);
                                var errorMessageResponse = JsonConvert.DeserializeObject<ErrorMessageResponseDTO>(responseJson);
                                var errorMessage = string.Empty;

                                if (!errorResponse.Error.IsEmpty())
                                {
                                    errorMessage = errorResponse.Error;
                                    if (!errorResponse.ExceptionMessage.IsEmpty())
                                        errorMessage = errorResponse.ExceptionMessage;
                                }
                                else if (!errorMessageResponse.Message.IsEmpty())
                                {
                                    errorMessage = errorMessageResponse.Message;
                                    if (!errorMessageResponse.ExceptionMessage.IsEmpty())
                                        errorMessage = errorMessageResponse.ExceptionMessage;
                                }
                                else
                                {
                                    errorMessage = "Oops! An error has occurred.";
                                }

                                return Tuple.Create<T, string>(null, errorMessage);
                            }
                            catch (Exception)
                            {
                                return Tuple.Create<T, string>(null, "Oops! An error has occurred.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                    //return Tuple.Create<T, string>(null, ex.Message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Tuple<T, string>> Get(string url, bool requiresToken = true)
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                }

                lock (threadlock)
                {
                    if (WebApiClient == null)
                    {
                        WebApiClient = new HttpClient(new NativeMessageHandler(false, new TLSConfig()
                        {
                            DangerousAcceptAnyServerCertificateValidator = true,
                        })
                        {
                            Timeout = new TimeSpan(0, 0, 30),
                            DisableCaching = true,
                        })
                        {
                            BaseAddress = new Uri(ApiBaseUrl)
                        };
                    }
                }

                if (!url.StartsWith("http"))
                {
                    //WebApiClient.BaseAddress = new Uri(ApiUrlsBase.BaseUrl);
                    url = $"{WebApiClient.BaseAddress}{url}";
                }

                if (requiresToken)
                {
                    var token = await SecureStorage.GetAsync(SecureStorageProperties.AccessToken);
                    WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await WebApiClient.GetAsync(url);
                var responseJson = await response.Content.ReadAsStringAsync();

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        if (responseJson.IsEmpty())
                        {
                            return default(Tuple<T, string>);
                        }

                        var apiResult = JsonConvert.DeserializeObject<T>(responseJson);
                        var retval = Tuple.Create<T, string>(apiResult, null);

                        return retval;
                    }
                    else
                    {
                        try
                        {
                            var apiError = JsonConvert.DeserializeObject<string>(responseJson);
                            return Tuple.Create<T, string>(null, apiError);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseDTO>(responseJson);
                                var errorMessageResponse = JsonConvert.DeserializeObject<ErrorMessageResponseDTO>(responseJson);
                                var errorMessage = string.Empty;

                                if (!errorResponse.Error.IsEmpty())
                                {
                                    errorMessage = errorResponse.Error;
                                    if (!errorResponse.ExceptionMessage.IsEmpty())
                                        errorMessage = errorResponse.ExceptionMessage;
                                }
                                else if (!errorMessageResponse.Message.IsEmpty())
                                {
                                    errorMessage = errorMessageResponse.Message;
                                    if (!errorMessageResponse.ExceptionMessage.IsEmpty())
                                        errorMessage = errorMessageResponse.ExceptionMessage;
                                }
                                else
                                {
                                    errorMessage = "Oops! An error has occurred.";
                                }

                                return Tuple.Create<T, string>(null, errorMessage);
                            }
                            catch (Exception)
                            {
                                return Tuple.Create<T, string>(null, "Oops! An error has occurred.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                    //return Tuple.Create<T, string>(null, ex.Message);
                }

                //return default(Tuple<T, string>);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<Tuple<T, string>> Post(string url, object postData, bool requiresToken = true, bool encode = true)
        {
            var query = string.Empty;
            // convert the post data to a url query string
            // example: param1=value1&param2=value2&parame3=value3

            if (postData != null)
            {
                var postString = JsonConvert.SerializeObject(postData, Formatting.Indented);

                if (encode)
                {
                    var jObj = (JObject)JsonConvert.DeserializeObject(postString);
                    query = String.Join("&", jObj.Children().Cast<JProperty>().Select(jp => jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));
                }
                else
                {
                    query = postString;
                }
            }

            return await PostOrPut(url, query, HttpMethod.Post, requiresToken, encode);
        }

        private static async Task<Tuple<T, string>> PostOrPut(string url, string postString, HttpMethod method, bool requiresToken, bool encode = true)
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                }

                lock (threadlock)
                {
                    if (WebApiClient == null)
                    {
                        WebApiClient = new HttpClient(new NativeMessageHandler(false, new TLSConfig()
                        {
                            DangerousAcceptAnyServerCertificateValidator = true,
                        })
                        {
                            Timeout = new TimeSpan(0, 0, 30),
                            DisableCaching = true,
                        })
                        {
                            BaseAddress = new Uri(ApiBaseUrl)
                        };
                    }
                }

                //var cancellationTokenSrc = new CancellationTokenSource(15000);
                //var cancellationToken = cancellationTokenSrc.Token;

                if (!url.StartsWith("http"))
                {
                    //WebApiClient.BaseAddress = new Uri(ApiUrlsBase.BaseUrl);
                    url = $"{WebApiClient.BaseAddress}{url}";
                }

                StringContent postContent = null;

                if (encode)
                {
                    postContent = new StringContent(postString, Encoding.UTF8, "application/x-www-form-urlencoded");
                }
                else
                {
                    postContent = new StringContent(postString, Encoding.UTF8, "application/json");
                }
                //postContent.Headers.ContentType = new MediaTypeHeaderValue("text/json");
                //postContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                //postContent.Headers.ContentEncoding.Add(Encoding.UTF8.ToString());

                if (requiresToken)
                {
                    var token = await SecureStorage.GetAsync(SecureStorageProperties.AccessToken);
                    WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                //WebApiClient.DefaultRequestHeaders.Accept.Clear();
                //WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = new HttpResponseMessage();

                if (method == HttpMethod.Post)
                {
                    response = await WebApiClient.PostAsync(url, postContent);
                }
                else
                {
                    response = await WebApiClient.PutAsync(url, postContent);
                }

                var responseJson = await response.Content.ReadAsStringAsync();

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        if (responseJson.IsEmpty())
                        {
                            return default(Tuple<T, string>);
                        }

                        var apiResult = JsonConvert.DeserializeObject<T>(responseJson);
                        var retval = Tuple.Create<T, string>(apiResult, null);

                        return retval;
                    }
                    else
                    {
                        try
                        {
                            var apiError = JsonConvert.DeserializeObject<string>(responseJson);
                            return Tuple.Create<T, string>(null, apiError);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseDTO>(responseJson);
                                var errorMessageResponse = JsonConvert.DeserializeObject<ErrorMessageResponseDTO>(responseJson);
                                var errorMessage = string.Empty;

                                if (!errorResponse.Error.IsEmpty())
                                {
                                    errorMessage = errorResponse.Error;
                                    if (!errorResponse.ExceptionMessage.IsEmpty())
                                        errorMessage = errorResponse.ExceptionMessage;
                                }
                                else if (!errorMessageResponse.Message.IsEmpty())
                                {
                                    errorMessage = errorMessageResponse.Message;
                                    if (!errorMessageResponse.ExceptionMessage.IsEmpty())
                                        errorMessage = errorMessageResponse.ExceptionMessage;
                                }
                                else
                                {
                                    errorMessage = "Oops! An error has occurred.";
                                }

                                return Tuple.Create<T, string>(null, errorMessage);
                            }
                            catch (Exception)
                            {
                                return Tuple.Create<T, string>(null, "Oops! An error has occurred.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                    //return Tuple.Create<T, string>(null, ex.Message);
                }

                //return default(Tuple<T, string>);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw;
                //return null;
            }
        }

        public static async Task<Tuple<T, string>> Put(string url, object postData, bool requiresToken = true, bool encode = true)
        {
            var query = string.Empty;
            // convert the post data to a url query string
            // example: param1=value1&param2=value2&parame3=value3
            var postString = JsonConvert.SerializeObject(postData, Formatting.Indented);

            if (encode)
            {
                var jObj = (JObject)JsonConvert.DeserializeObject(postString);
                query = String.Join("&", jObj.Children().Cast<JProperty>().Select(jp => jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));
            }
            else
            {
                query = postString;
            }

            return await PostOrPut(url, query, HttpMethod.Put, requiresToken, encode);
        }
    }
}