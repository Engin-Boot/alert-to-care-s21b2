using AlertToCareFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AlertToCareFrontend.Utilities
{
    internal static class HttpClientUtility
    {
        static readonly HttpClient Client = new HttpClient();

        static HttpClientUtility()
        {
            // Setting Base address.  
            Client.BaseAddress = new Uri("http://localhost:5000/");

            // Setting content type.  
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Setting timeout.  
            Client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
        }

        private static string ConvertHttpResponseToStringResponse(ref HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                response.Dispose();
                return "";
            }
            else
            { 
                string result = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<string>(result);
                return result;
            }
        }

        internal static async Task<string> PutBedData(BedDataModel requestObj)
        {
            try
            {  
                HttpResponseMessage response = await Client.PutAsJsonAsync("api/BedData/" + requestObj.BedId, requestObj).ConfigureAwait(false);

                return ConvertHttpResponseToStringResponse(ref response);
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }
        internal static async Task<string> PostPatientData(PatientDataModel requestObj)
        {
            try { 
                HttpResponseMessage response = await Client.PostAsJsonAsync("api/PatientData/", requestObj).ConfigureAwait(false);

                return ConvertHttpResponseToStringResponse(ref response);
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }

        internal static async Task<string> PostVitalsData(VitalsDataModel requestObj)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync("api/VitalData/", requestObj).ConfigureAwait(false);

                return ConvertHttpResponseToStringResponse(ref response);
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }

        internal static async Task<string> DeleteData(string uri)
        {
            try
            {
                HttpResponseMessage response = await Client.DeleteAsync(uri).ConfigureAwait(false);

                return ConvertHttpResponseToStringResponse(ref response);
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }

        internal static async Task<string> GetData(string uri)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(uri).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);

                    // Releasing.  
                    response.Dispose();
                    return result;
                }
                else
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);
                    return result;
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        internal static async Task<VitalsDataModel> GetVitalData(string patientId)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync("api/VitalData/" + patientId).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    VitalsDataModel responseObj = JsonConvert.DeserializeObject<VitalsDataModel>(result);

                    // Releasing.  
                    response.Dispose();
                    return responseObj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static async Task<string> PutVitalData(VitalsDataModel requestObj)
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsJsonAsync("api/VitalData/" + requestObj.PatientId, requestObj).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    response.Dispose();
                    return "Vitals update successful!";
                }
                else
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);
                    return result;
                }
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }
        internal static async Task<IEnumerable<BedDataModel>> GetBeds(string icuId)
        {
            // Initialization.  
            IEnumerable<BedDataModel> responseObj = new List<BedDataModel>();

            try
            {
                var url = "api/BedData/GetBedsByIcuId/" + icuId;
                HttpResponseMessage response = await Client.GetAsync(url).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<IEnumerable<BedDataModel>>(result);

                    // Releasing.  
                    response.Dispose();
                }
            }
            catch(Exception)
            {
                return responseObj;
            }
            return responseObj;
        }

        internal static async Task<string> PostBedData(BedDataModel requestObj)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync("api/BedData/", requestObj).ConfigureAwait(false);

                return ConvertHttpResponseToStringResponse(ref response);
                
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        internal static async Task<string> PostIcuData(IcuDataModel requestObj)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync("api/IcuData/", requestObj).ConfigureAwait(false);

                return ConvertHttpResponseToStringResponse(ref response);
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
