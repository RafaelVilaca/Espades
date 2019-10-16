using Espades.Common.Containers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Espades.Common.Helpers
{
    public static class ModelStateHelper
    {
        public static Message[] GetModelStateErrors(ModelStateDictionary modelState, IStringLocalizer _localizer)
        {
            List<Message> messages = new List<Message>();

            foreach (ModelStateEntry model in modelState.Values)
            {
                foreach (ModelError error in model.Errors)
                {
                    LocalizedString msg = _localizer[error.ErrorMessage];
                    if (!string.IsNullOrEmpty(msg))
                    {
                        messages.Add(new Message(msg));
                    }
                    else
                    {
                        messages.Add(new Message(string.Format(_localizer["UnexpectedError"])));
                    }
                }
            }

            return messages.ToArray();
        }
    }
}
