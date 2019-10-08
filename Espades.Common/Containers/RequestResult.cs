using Espades.Common.Enumerators;
using Espades.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace Espades.Common.Containers
{
    public class RequestResult
    {
        public RequestResult()
        {
            Status = StatusResult.Success;
            Messages = new List<Message>();
        }

        public RequestResult(StatusResult status, object data)
            : this()
        {
            Status = status;
            Data = data;
        }

        public RequestResult(StatusResult status, params Message[] messages)
            : this()
        {
            Status = status;

            if (messages != null)
            {
                foreach (Message message in messages)
                {
                    Messages.Add(message);
                }
            }
        }

        public RequestResult(StatusResult status, object data, params Message[] messages)
            : this()
        {
            Status = status;
            Data = data;

            if (messages != null)
            {
                foreach (Message message in messages)
                {
                    Messages.Add(message);
                }
            }
        }

        public StatusResult Status { get; set; }

        public object Data { get; set; }

        public List<Message> Messages { get; set; }

        public Exception Exception { get; set; }
    }

    public static class RequestResultExtension
    {
        public static RequestResult ThrowIfUnsuccessfully(this RequestResult value)
        {
            if (value.Status != StatusResult.Success)
            {
                throw new BusinessException(value);
            }

            return value;
        }

        public static RequestResult ThrowIfNoAuth(this RequestResult value)
        {
            if (value.Status == StatusResult.NoAuth)
            {
                throw new BusinessException(value);
            }

            return value;
        }

        public static T ParseTo<T>(this RequestResult value) where T : class, new()
        {
            if (value.Data != null)
            {
                return ((T)value.Data);
            }
            else
            {
                return new T();
            }
        }

        public static RequestResult ThrowIfUnsuccessfully(this RequestResult value, string defaultMessage)
        {
            ValidateRequestMessages(value, defaultMessage);
            return value.ThrowIfUnsuccessfully();
        }

        private static void ValidateRequestMessages(RequestResult value, string defaultMessage)
        {
            value.Messages.RemoveAll(x => string.IsNullOrEmpty(x.Text));

            if (value.Messages.Count == 0 && value.Status != StatusResult.Success)
            {
                value.Messages.Add(new Message(defaultMessage));
            }
        }
    }
}
