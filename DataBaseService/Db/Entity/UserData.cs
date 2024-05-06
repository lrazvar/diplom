namespace DataBaseService.Db.Entity;

public class UserData
{
    public int Id { get; set; }

    public string Login { get; private set; }
    public string Password { get; private set; }
    public bool IsTeacher { get; private set; }
    public string SchoolClass { get; private set; }
    public string Score { get; private set; }

    public UserData(string requestLogin, string requestPassword, string requestSchoolClass, string requestScore)
    {
        Id = 0;
        Login = requestLogin;
        Password = requestPassword;
        SchoolClass = requestSchoolClass;
        Score = requestScore;
    }

    public UserData()
    {
    }
}