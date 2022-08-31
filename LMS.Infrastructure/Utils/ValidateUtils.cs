using LMS.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Utils
{
    public class ValidateUtils
    {
        public static void CheckStringNotEmpty(string field, string value)
        {
            if (string.IsNullOrEmpty(value) || value.Trim().Length == 0)
                throw new RequestException(ErrorCodes.DataIsEmpty, $"Field {field} is empty");
        }
        public static void CheckDataNotNull(string name, object value)
        {
            if (value is Task)
            {
                throw new System.InvalidOperationException("Should not pass task here");
            }

            if (value == null)
                throw new RequestException(ErrorCodes.DataIsEmpty, $"{name} must be not null");
        }
        public static void CheckNullOrEmptyList<T>(string name, List<T> list)
        {
            if(list == null)
            {
                throw new RequestException(ErrorCodes.DataListIsNull, $"{name} is null");
            }
            else if (!list.Any())
            {
                throw new RequestException(ErrorCodes.DataListIsEmpty, $"{name} is empty");
            }
        }
        public static Guid CheckGuidFormat(string name, string value)
        {
            try
            {
                return Guid.Parse(value);
            }
            catch (FormatException)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ValueNotValid, $"'{name} '" + ErrorMessages.ValueNotValid);
            }
        }

        public static void TimeLimitValidate(string name, TimeSpan time)
        {
            TimeSpanValidator myTimeSpanValidator = new(TimeSpan.FromMinutes(1), TimeSpan.FromDays(1));
            try
            {
                myTimeSpanValidator.Validate(time);
            } catch (ArgumentException)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TimeSpanNotValid, $"'{name} '" + ErrorMessages.ValueNotValid);
            }
        }

        public static void CheckStartEndTime(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            if (DateTimeOffset.Compare(startTime, endTime) >= 0)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.StartEndTimeNotValid, ErrorMessages.StartEndTimeNotValid);
            }
        }
    }
}