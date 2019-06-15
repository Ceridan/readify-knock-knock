namespace Readify.KnockKnock.Api.Services
{
    public class TriangleService : ITriangleService
    {
        public string DetermineTriangleType(int sideA, int sideB, int sideC)
        {
            if ((long)sideA + sideB <= sideC
                || (long)sideA + sideC <= sideB
                || (long)sideB + sideC <= sideA)
            {
                return "Error";
            }

            if (sideA == sideB && sideA == sideC)
            {
                return "Equilateral";
            }

            if (sideA == sideB || sideA == sideC || sideB == sideC)
            {
                return "Isosceles";
            }

            return "Scalene";
        }
    }
}
