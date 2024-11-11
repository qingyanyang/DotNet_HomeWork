using Microsoft.AspNetCore.Mvc.Filters;

namespace NET3Assignment.Common.Exceptions
{
    public class A4NotFoundException: Exception
    {
        public A4NotFoundException(string message):base(message)
        {

        }
    }
}
