using HealthCare_API.RequestModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
  public  class EditUserResponseModel:BaseResponseModel
    {
        public EditUserResponseModel()
        {
            Response = new EditUser();
        }
     //  public EditUser Response { get; set; }
        #region Properties
        private EditUser employeeObject;
        /// <summary>
        /// Gets or sets the employee object.
        /// </summary>
        /// <value>The employee object.</value>
        public EditUser Response
        {
            get
            {
                return employeeObject;
            }
            set
            {
                employeeObject = value; OnPropertyChanged();
            }
        }
        #endregion
    }
    public class EditUser :BaseRequestModel
    {
        public int UserId
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string MiddelName
        {
            get;
            set;
        }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DOB { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

      //  public event PropertyChangedEventHandler PropertyChanged;
    }
}
