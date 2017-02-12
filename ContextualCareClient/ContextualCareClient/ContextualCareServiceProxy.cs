using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ContextualCareClient
{
   
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class ContextualCareServiceProxy
    {
        const string apiKey = "nXJPX9TL4sU+17qp3oWFov+ScT2Q1hFb44uFJxhmEB3fvZH2QFpNjZBx2eMAH6mV7nesCAZoyVqHp9iz8De9pQ==";
        public static async Task<string> InvokeRequestResponseService(InputValues values)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Age", "HcG Level", "Multiple Doppler HeartBeat Count", "Weight Gain >10lbs", "Uterine Fundus Height Beyond Range (binary)", "Frequent Fetal Movements", "Ultrasound Confirmation", "Diabetic", "Sugar Level in urine", "Feel Thirsty", "Frequent Urination", "Fatigue Level", "Blurred vision", "Past Hypertension History", "Hypertension Type", "Workflow"},
                                Values = new string[,] {  { values.Age.ToString(), values.HcGLevel, values.DopplerHBeatCount.ToString(),
                                        values.WeightGain.ToString(), values.FundusHeightBeyondRange.ToString(), values.FrequentFetalMovements.ToString(), values.UltrasoundConfirmation, values.Diabetic.ToString(), values.SugarLevelInUrine.ToString(), values.FeelThirsty.ToString(), values.FrequentUrination.ToString(), values.FatigueLevel, values.BlurredVision.ToString(), values.PastHypertensionHistory.ToString(),
                                        values.Hypertension.ToString(), "0" }, { values.Age.ToString(), values.HcGLevel, values.DopplerHBeatCount.ToString(),
                                        values.WeightGain.ToString(), values.FundusHeightBeyondRange.ToString(), values.FrequentFetalMovements.ToString(), values.UltrasoundConfirmation, values.Diabetic.ToString(), values.SugarLevelInUrine.ToString(), values.FeelThirsty.ToString(), values.FrequentUrination.ToString(), values.FatigueLevel, values.BlurredVision.ToString(), values.PastHypertensionHistory.ToString(),
                                        values.Hypertension.ToString(), "0" } }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/cfcd10704c2444678bd6f16fbf0bae20/services/021fa227891a42d0b1fa56e919ffd609/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)
                string jsonString = JsonConvert.SerializeObject(scoreRequest);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                
                //HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);
                HttpResponseMessage response = await client.PostAsync("", jsonContent);
               


                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    throw new Exception("Failed to get data from service with code " + response.StatusCode);
                }
            }
        }
    }
}
