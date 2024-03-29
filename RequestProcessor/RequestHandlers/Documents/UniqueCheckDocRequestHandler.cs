﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SynWord_Server_CSharp.Model.UniqueCheck;
using Newtonsoft.Json;
using SynWord_Server_CSharp.DocumentHandling.Docx;

namespace SynWord_Server_CSharp.RequestProcessor.RequestHandlers.Documents {
    public class UniqueCheckDocRequestHandler {
        private string _filePath;

        public UniqueCheckDocRequestHandler(string filePath) {
            _filePath = filePath;
        }

        public async Task<IActionResult> HandleRequest() {
            UniqueCheckResponseModel uniqueCheckResponse = await DocxUniqueCheck.UniqueCheck(_filePath);

            string response = JsonConvert.SerializeObject(uniqueCheckResponse);

            return new OkObjectResult(response);
        }
    }
}
