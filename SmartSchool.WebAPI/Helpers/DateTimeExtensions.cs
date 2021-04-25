using System;

namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtensions
    {
        public static int PegarIdade(this DateTime data)
        {
            var dataAtual = DateTime.UtcNow;
            int age = dataAtual.Year - data.Year;

            if(dataAtual < data.AddYears(age))
                age--;

            return age;
        }
    }
}