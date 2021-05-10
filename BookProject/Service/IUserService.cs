namespace BookProject.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}