namespace Readify.KnockKnock.Api.Services
{
    public interface ITriangleService
    {
        string DetermineTriangleType(int sideA, int sideB, int sideC);
    }
}
