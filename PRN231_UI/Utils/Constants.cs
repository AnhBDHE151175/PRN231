namespace PRN231_UI.Utils
{
    public static class Constants
    {
        public const string API_URL = "https://localhost:5000/api";
        //Job
        public const string JOB_GETALL = "/Job/GetFilter";
        public const string JOB_INSERT = "/Job/Insert";
        public const string JOB_UPDATE = "/Job/Update";
        public const string JOB_DELETE = "/Job/Delete";
        public const string JOB_GETBYID = "/Job/GetByID";
        public const string JOB_ANALY = "/Job/Analysis";
        public const string LOGIN = "/Job/Login";
        public const string SEND = "/Job/SendMail";
        //Skill
        public const string SKILL_GETALL = "/Skill/GetFilter";
        public const string SKILL_INSERT = "/Skill/Insert";
        public const string SKILL_UPDATE = "/Skill/Update";
        public const string SKILL_DELETE = "/Skill/Delete";
        public const string SKILL_GETBYID = "/Skill/GetByID";
        //Stage
        public const string STAGE_GETALL = "/Stage/GetFilter";
        public const string STAGE_INSERT = "/Stage/Insert";
        public const string STAGE_UPDATE = "/Stage/Update";
        public const string STAGE_DELETE = "/Stage/Delete";
        public const string STAGE_GETBYID = "/Stage/GetByID";



        public const string COUNTRY_API = "/Countries";

        public const string DEPARTMENT_API = "/Departments";

        public const string INTERVIEWER_API = "/Interviewers";

        public const string LOCATION_API = "/Locations";

        public const string REGION_API = "/Regions";

        public const string CANDIDATE_API = "/Candidates";


        public const int PAGE_SIZE = 10;
    }
}
