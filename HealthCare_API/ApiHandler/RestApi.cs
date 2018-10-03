using HealthCare_API.Model;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ApiHandler
{
    public class RestApi
    {
        public WebRequest webRequest = null;
        private HttpClient client;
        //private int _statusCode;
        //private string _response;
        private string _col = ":";

        public RestApi()

        {
            client = new HttpClient();
        }
        //Get Api for List Data
        public async Task<T> GetAsyncData_GetApi<T>(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, T Tobject) where T : new()
        {
            // var _storedToken=Settings;
            try
            {
                client.MaxResponseContentBufferSize = 256000;
                if (IsHeaderRequired)
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    //client.DefaultRequestHeaders.Add("Authorization", "application/json");
                    //client.DefaultRequestHeaders.Add("Content-Length", "84");
                    //client.DefaultRequestHeaders.Add("User-Agent", "Fiddler");
                    //client.DefaultRequestHeaders.Add("Host", "localhost:49165");
                    // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                // var request = new HttpRequestMessage(HttpMethod.Get, uri);

                // request.Content = new FormUrlEncodedContent(keyValues);

                //  HttpResponseMessage response = await client.SendAsync(request);

                HttpResponseMessage response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {

                    var responseContent = response.Content;
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    //  SucessResponse = SucessResponse.Insert(1, "\"Status\"" + _col + "\"Success\",");
                    Tobject = JsonConvert.DeserializeObject<T>(SucessResponse);
                    return Tobject;

                }
                else
                {

                    //long ResonseStatus = Convert.ToInt64(response.StatusCode);
                    //switch (ResonseStatus)
                    //{
                    //    case 302:
                    //        _response = "{\"Status\"" + _col + "\"Invalid User Name and password..\"}";
                    //        break;
                    //    case 400:
                    //        _response = "{\"Status\"" + _col + "\"Bad Request\"}";
                    //        break;
                    //    case 401:
                    //        _response = "{\"Status\"" + _col + "\"Invalid User Name and password..\"}";
                    //        break;
                    //    case 404:
                    //        _response = "{\"Status\"" + _col + "\"Not Found\"}";
                    //        break;

                    //    default:
                    //        _response = "{\"Status\"" + _col + "\"Internal Server errror\"}";
                    //        break;

                    var responseContent = response.Content;
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    ErrorResponse = ErrorResponse.Insert(1, "\"Status\"" + _col + "\"fail\",");
                    Tobject = JsonConvert.DeserializeObject<T>(ErrorResponse);
                    return Tobject;

                    // }
                }

                //Tobject = JsonConvert.DeserializeObject<T>(_response);
                //return Tobject;
            }
            catch (Exception ex)
            {

                throw;
               
            }
        }       
        public async Task<ObservableCollection<T>> GetAsyncData_GetApiList<T>(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ObservableCollection<T> Tobject) where T : new()
        {
            // var _storedToken=Settings;
            try
            {
                HttpResponseMessage response = null;
                //  client.MaxResponseContentBufferSize = 256000;
                if (IsHeaderRequired)
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);

                }


                response = await client.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {

                    var responseContent = response.Content;
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    //  SucessResponse = SucessResponse.Insert(1, "\"Status\"" + _col + "\"Success\",");
                    Tobject = JsonConvert.DeserializeObject<ObservableCollection<T>>(SucessResponse);
                    return Tobject;

                }
                else
                {





                    var responseContent = response.Content;
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    //  ErrorResponse = ErrorResponse.Insert(1, "\"Status\"" + _col + "\"fail\",");
                    Tobject = JsonConvert.DeserializeObject<ObservableCollection<T>>(ErrorResponse);
                    return Tobject;


                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //POST APIS..........................
        public async Task<OTPResponseModel> LoginAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, OTPRequestModel _objOTPRequestModel)
        {

            client.MaxResponseContentBufferSize = 256000;
            OTPResponseModel _objOTPResponseModel =new OTPResponseModel();
           
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("PhoneNo",_objOTPRequestModel.PhoneNo),
                new KeyValuePair<string, string>("DeviceId",_objOTPRequestModel.DeviceId),
                new KeyValuePair<string, string>("DeviceType",_objOTPRequestModel.DeviceType)
            };
            if (IsHeaderRequired)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Length", "69");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Fiddler");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Host", "localhost:49165");
            }
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await client.SendAsync(request);
            var statuscode = Convert.ToInt64(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                var SucessResponse = await response.Content.ReadAsStringAsync();               
                _objOTPResponseModel = JsonConvert.DeserializeObject<OTPResponseModel>(SucessResponse);               
                return _objOTPResponseModel;
            }
            else
            {
                var ErrorResponse = await response.Content.ReadAsStringAsync();               
                _objOTPResponseModel = JsonConvert.DeserializeObject<OTPResponseModel>(ErrorResponse);
                return _objOTPResponseModel;
            }

        }
        public async Task<OTPVerificationResponseModel> OTPAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, OTPResponse _objOTPResponseModel)
        {

           // client.MaxResponseContentBufferSize = 256000;
            OTPVerificationResponseModel _objOTPVerificationResponseModel = new OTPVerificationResponseModel();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("OTP",_objOTPResponseModel.OTP.ToString()),
                new KeyValuePair<string, string>("UserId",_objOTPResponseModel.UserId.ToString())
               
            };
            if (IsHeaderRequired)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Length", "69");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Fiddler");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Host", "localhost:49165");
            }
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var SucessResponse = await response.Content.ReadAsStringAsync();              
                _objOTPVerificationResponseModel = JsonConvert.DeserializeObject<OTPVerificationResponseModel>(SucessResponse);              
                return _objOTPVerificationResponseModel;
            }
            else
            {


                var ErrorResponse = await response.Content.ReadAsStringAsync();
              //  ErrorResponse = ErrorResponse.Insert(1, "\"Status\"" + _col + "\"Fail\",");
                _objOTPVerificationResponseModel = JsonConvert.DeserializeObject<OTPVerificationResponseModel>(ErrorResponse);
                return _objOTPVerificationResponseModel;
            }

        }

        public async Task<PINUpdateResponseModel> PinUpdateAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, PINUpdateRequestModel _objPINUpdateRequestModel)
        {

            client.MaxResponseContentBufferSize = 256000;
            PINUpdateResponseModel _objPINUpdateResponseModel = new PINUpdateResponseModel();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("UserId",_objPINUpdateRequestModel.UserId.ToString()),
                new KeyValuePair<string, string>("UserPin",_objPINUpdateRequestModel.UserPin.ToString())

            };
            if (IsHeaderRequired)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
            }
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            request.Content = new FormUrlEncodedContent(keyValues);

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var SucessResponse = await response.Content.ReadAsStringAsync();               
                _objPINUpdateResponseModel = JsonConvert.DeserializeObject<PINUpdateResponseModel>(SucessResponse);         
                return _objPINUpdateResponseModel;
            }
            else
            {
                var ErrorResponse = await response.Content.ReadAsStringAsync();               
                _objPINUpdateResponseModel = JsonConvert.DeserializeObject<PINUpdateResponseModel>(ErrorResponse);
                return _objPINUpdateResponseModel;
            }

        }
        public async Task<AppointmentResponseModel> LoadAppointmentListAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, AppointmentRequestModel _objAppointmentRequestModel)
        {

            AppointmentResponseModel objAppointmentResponseModel;
            string s = JsonConvert.SerializeObject(_objAppointmentRequestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                   
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();                   
                    objAppointmentResponseModel = JsonConvert.DeserializeObject<AppointmentResponseModel>(SucessResponse);
                    return objAppointmentResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();                   
                    objAppointmentResponseModel = JsonConvert.DeserializeObject<AppointmentResponseModel>(ErrorResponse);
                    return objAppointmentResponseModel;
                }
            }
        }

        public async Task<BookAppointmentResponseModel> BookAppointmentAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, BookAppointmentRequestModel _objBookAppointmentRequestModel)
        {

            BookAppointmentResponseModel objBookAppointmentResponseModel;
            string s = JsonConvert.SerializeObject(_objBookAppointmentRequestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                  
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objBookAppointmentResponseModel = JsonConvert.DeserializeObject<BookAppointmentResponseModel>(SucessResponse);
                    return objBookAppointmentResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objBookAppointmentResponseModel = JsonConvert.DeserializeObject<BookAppointmentResponseModel>(ErrorResponse);
                    return objBookAppointmentResponseModel;
                }
            }
        }
        public async Task<DepartmentListResponseModel> DepartmentListAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, DepartmentListRequestModel _objDepartmentListRequestModel)
        {

            DepartmentListResponseModel objDepartmentListResponseModel;
            string s = JsonConvert.SerializeObject(_objDepartmentListRequestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);                    
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objDepartmentListResponseModel = JsonConvert.DeserializeObject<DepartmentListResponseModel>(SucessResponse);
                    return objDepartmentListResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objDepartmentListResponseModel = JsonConvert.DeserializeObject<DepartmentListResponseModel>(ErrorResponse);
                    return objDepartmentListResponseModel;
                }
            }
        }

        public async Task<DoctorListResponseModel> DoctorListByDepartmentAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, DoctorListRequestModel _objDoctorListRequestModel)
        {

            DoctorListResponseModel objDoctorListResponseModel;
            string s = JsonConvert.SerializeObject(_objDoctorListRequestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objDoctorListResponseModel = JsonConvert.DeserializeObject<DoctorListResponseModel>(SucessResponse);
                    return objDoctorListResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objDoctorListResponseModel = JsonConvert.DeserializeObject<DoctorListResponseModel>(ErrorResponse);
                    return objDoctorListResponseModel;
                }
            }
        }
        public async Task<PrescriptionResponseModel> GetPrescriptionListAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, PrescriptionRequestModel _objPrescriptionRequestModel)
        {

            PrescriptionResponseModel objPrescriptionResponseModel;
            string s = JsonConvert.SerializeObject(_objPrescriptionRequestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objPrescriptionResponseModel = JsonConvert.DeserializeObject<PrescriptionResponseModel>(SucessResponse);
                    return objPrescriptionResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objPrescriptionResponseModel = JsonConvert.DeserializeObject<PrescriptionResponseModel>(ErrorResponse);
                    return objPrescriptionResponseModel;
                }
            }
        }

        public async Task<ReportResponseModel> GetReportListAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, ReportRequestModel _objReportRequestModel)
        {

            ReportResponseModel objReportResponseModel;
            string s = JsonConvert.SerializeObject(_objReportRequestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objReportResponseModel = JsonConvert.DeserializeObject<ReportResponseModel>(SucessResponse);
                    return objReportResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objReportResponseModel = JsonConvert.DeserializeObject<ReportResponseModel>(ErrorResponse);
                    return objReportResponseModel;
                }
            }
        }
        public async Task<LogoutResponseModel> LogOutAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, LogoutRequestModel _objLogoutRequestModel)
        {

            LogoutResponseModel objLogoutResponseModel;
            string s = JsonConvert.SerializeObject(_objLogoutRequestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objLogoutResponseModel = JsonConvert.DeserializeObject<LogoutResponseModel>(SucessResponse);
                    return objLogoutResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objLogoutResponseModel = JsonConvert.DeserializeObject<LogoutResponseModel>(ErrorResponse);
                    return objLogoutResponseModel;
                }
            }
        }

        public async Task<Reason4VisitResponseModel> Reason4VisitAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, Reason4VisitResponseModel _objReason4VisitResponseModel)
        {

            Reason4VisitResponseModel objReason4VisitResponseModel;
            string s = JsonConvert.SerializeObject(_objReason4VisitResponseModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objReason4VisitResponseModel = JsonConvert.DeserializeObject<Reason4VisitResponseModel>(SucessResponse);
                    return objReason4VisitResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objReason4VisitResponseModel = JsonConvert.DeserializeObject<Reason4VisitResponseModel>(ErrorResponse);
                    return objReason4VisitResponseModel;
                }
            }
        }
        public async Task<RegisterUserResponse> RegisterAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, RegisterUserRequest _objRegisterUserRequest)
        {

            RegisterUserResponse objRegisterUserResponse;
            string s = JsonConvert.SerializeObject(_objRegisterUserRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objRegisterUserResponse = JsonConvert.DeserializeObject<RegisterUserResponse>(SucessResponse);
                    return objRegisterUserResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objRegisterUserResponse = JsonConvert.DeserializeObject<RegisterUserResponse>(ErrorResponse);
                    return objRegisterUserResponse;
                }
            }
        }
        public async Task<EditUserResponseModel> EditUserAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, EditUserResquestModel _objEditUserResquestModel)
        {

            EditUserResponseModel objEditUserResponseModel;
            string s = JsonConvert.SerializeObject(_objEditUserResquestModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objEditUserResponseModel = JsonConvert.DeserializeObject<EditUserResponseModel>(SucessResponse);
                    return objEditUserResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objEditUserResponseModel = JsonConvert.DeserializeObject<EditUserResponseModel>(ErrorResponse);
                    return objEditUserResponseModel;
                }
            }
        }
        public async Task<RegisterUserResponse> UpdateUserAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, RegisterUserRequest _objRegisterUserRequest)
        {

            RegisterUserResponse objRegisterUserResponse;
            string s = JsonConvert.SerializeObject(_objRegisterUserRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objRegisterUserResponse = JsonConvert.DeserializeObject<RegisterUserResponse>(SucessResponse);
                    return objRegisterUserResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objRegisterUserResponse = JsonConvert.DeserializeObject<RegisterUserResponse>(ErrorResponse);
                    return objRegisterUserResponse;
                }
            }
        }
        //public async Task<UploadProfileResponse> UpdateUserProfileAsync1(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, UploadProfileBase64Req _objUploadProfileBase64Req)
        //{
        //    UploadProfileResponse objUploadProfileResponse;
        //    string s = JsonConvert.SerializeObject(_objUploadProfileBase64Req);
        //    HttpResponseMessage response = null;
        //    using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
        //    {
        //        //  MultipartFormDataContent form = new MultipartFormDataContent();
        //        //form.Add(new StreamContent(_mediafile.GetStream()),
        //        //   "\"file\"",
        //        //   $"\"{_mediafile.Path}\"");
        //        //  form.Add(new (content, 0, content.Count()), "profile_pic", Name);

        //        if (IsHeaderRequired)
        //    {

        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", objHeaderModel.OTPToken);


        //    }
        //    response = await client.PostAsync("http://10.0.1.246:5000/api/User/UploadUserProfile", stringContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var SucessResponse = await response.Content.ReadAsStringAsync();
        //        objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(SucessResponse);
        //        return objUploadProfileResponse;
        //    }
        //    else
        //    {
        //        var ErrorResponse = await response.Content.ReadAsStringAsync();
        //        objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(ErrorResponse);
        //        return objUploadProfileResponse;
        //    }
        //    }
        //}
        public async Task<UploadProfileResponse> UpdateUserProfileAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, MediaFile _mediafile)
        {
            UploadProfileResponse objUploadProfileResponse;
          //  string s = JsonConvert.SerializeObject(_objUploadProfileBase64Req);
            HttpResponseMessage response = null;
            //using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            //{
                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new StreamContent(_mediafile.GetStream()),
                   "\"file\"",
                   $"\"{_mediafile.Path}\"");
                //  form.Add(new (content, 0, content.Count()), "profile_pic", Name);

                if (IsHeaderRequired)
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, form);
                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(SucessResponse);
                    return objUploadProfileResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objUploadProfileResponse = JsonConvert.DeserializeObject<UploadProfileResponse>(ErrorResponse);
                    return objUploadProfileResponse;
                }
           // }
        }


        public async Task<GetProfilePicResponseModel> GetUserprofileAsync(string uri, Boolean IsHeaderRequired, HeaderModel objHeaderModel, GetProfilePicResponseModel _objGetProfilePicResponseModel)
        {

            GetProfilePicResponseModel objGetProfilePicResponseModel;
            string s = JsonConvert.SerializeObject(_objGetProfilePicResponseModel);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {

                if (IsHeaderRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objHeaderModel.OTPToken);
                }
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objGetProfilePicResponseModel = JsonConvert.DeserializeObject<GetProfilePicResponseModel>(SucessResponse);
                    return objGetProfilePicResponseModel;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objGetProfilePicResponseModel = JsonConvert.DeserializeObject<GetProfilePicResponseModel>(ErrorResponse);
                    return objGetProfilePicResponseModel;
                }
            }
        }
        public async Task<string> PostAsyncData(string uri)
        {
            try
            {

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                return await response.Content.ReadAsStringAsync(); ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void FetchData(string url)
        {

            // Create an HTTP web request using the URL:
            webRequest = WebRequest.Create(new Uri(url));
            webRequest.ContentType = "application/json";
            webRequest.Method = "GET";
            webRequest.BeginGetRequestStream(new AsyncCallback(RequestStreamCallback), webRequest);
        }

        private void RequestStreamCallback(IAsyncResult asynchronousResult)
        {
            webRequest = (HttpWebRequest)asynchronousResult.AsyncState;

            using (var postStream = webRequest.EndGetRequestStream(asynchronousResult))
            {
                //send yoour data here
            }
            webRequest.BeginGetResponse(new AsyncCallback(ResponseCallback), webRequest);
        }


        void ResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest myrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            using (HttpWebResponse response = (HttpWebResponse)myrequest.EndGetResponse(asynchronousResult))
            {
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        var data = reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
