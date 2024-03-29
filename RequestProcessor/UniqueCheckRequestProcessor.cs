﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SynWord_Server_CSharp.RequestProcessor.RequestValidators;
using SynWord_Server_CSharp.Logging;
using SynWord_Server_CSharp.Model.Log;
using SynWord_Server_CSharp.Model.Request;
using SynWord_Server_CSharp.Constants;
using SynWord_Server_CSharp.RequestProcessor.RequestHandlers;

namespace SynWord_Server_CSharp.RequestProcessor {
    public class UniqueCheckRequestProcessor {
        private IValidationControl _validationControl;
        private UniqueCheckRequestHandler _uniqueCheck = new UniqueCheckRequestHandler();
        private int _requestPrice = RequestPrices.UniqueCheckPrice;

        public async Task<IActionResult> UnauthUserRequestExecution(IUserLogDataModel user) {
            try {
                RequestLogger.Add(new RequestStatusLog(RequestTypes.UniqueCheck, user.ToDictionary(), RequestStatuses.Start));

                int textLength = user.UserModel.Text.Length;

                _validationControl = new UnauthValidationControl(user.UserModel.Uid);

                _validationControl.MinSymbolLimitVerification(textLength);
                _validationControl.UniqueCheckMaxSymbolLimitVerification(textLength);
                _validationControl.IsUserHaveEnoughCoins(_requestPrice);

                IActionResult result = await _uniqueCheck.HandleRequest(user.UserModel.Text);

                _validationControl.SpendCoins(_requestPrice);

                RequestLogger.Add(new RequestStatusLog(RequestTypes.UniqueCheck, user.ToDictionary(), RequestStatuses.Completed));

                return result;
            } catch (Exception exception) {
                RequestLogger.Add(new RequestExceptionLog(RequestTypes.UniqueCheck, user.ToDictionary(), exception.Message));
                return RequestExceptionHandler.Handle(exception);
            }
        }

        public async Task<IActionResult> AuthUserRequestExecution(IUserLogDataModel user) {
            try {
                RequestLogger.Add(new RequestStatusLog(RequestTypes.UniqueCheck, user.ToDictionary(), RequestStatuses.Start));

                int textLength = user.UserModel.Text.Length;

                _validationControl = new AuthValidationControl(user.UserModel.Uid);

                _validationControl.MinSymbolLimitVerification(textLength);
                _validationControl.UniqueCheckMaxSymbolLimitVerification(textLength);
                _validationControl.IsUserHaveEnoughCoins(_requestPrice);

                IActionResult result = await _uniqueCheck.HandleRequest(user.UserModel.Text);

                _validationControl.SpendCoins(_requestPrice);

                RequestLogger.Add(new RequestStatusLog(RequestTypes.UniqueCheck, user.ToDictionary(), RequestStatuses.Completed));

                return result;
            } catch (Exception exception) {
                RequestLogger.Add(new RequestExceptionLog(RequestTypes.UniqueCheck, user.ToDictionary(), exception.Message));
                return RequestExceptionHandler.Handle(exception);
            }
        }
    }
}
