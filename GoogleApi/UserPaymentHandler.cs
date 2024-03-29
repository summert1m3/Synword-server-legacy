﻿using Google.Apis.AndroidPublisher.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;
using SynWord_Server_CSharp.Exceptions;

namespace SynWord_Server_CSharp.GoogleApi {
    public class UserPaymentHandler {
        static string serviceAccountEmail = "server@synword-com.iam.gserviceaccount.com";
        static X509Certificate2 certificate = new X509Certificate2(ContentRootPath.Path + @"/Files/synword-com-5456ebf70718.p12", "notasecret", X509KeyStorageFlags.Exportable);

        public static void PaymentVerify(string inAppItemId, string purchaseToken) {
            ServiceAccountCredential credential = new ServiceAccountCredential(
           new ServiceAccountCredential.Initializer(serviceAccountEmail) {
               Scopes = new[] { "https://www.googleapis.com/auth/androidpublisher" }
           }.FromCertificate(certificate));


            var service = new AndroidPublisherService(
               new BaseClientService.Initializer() {
                   HttpClientInitializer = credential,
                   ApplicationName = "GooglePlay API Sample",
               }
            );

            try {
                if (inAppItemId == "premium") {
                    var request = service.Purchases.Subscriptions.Get("com.patronum.synword", inAppItemId, purchaseToken);
                    var purchaseState = request.Execute();
                } else {
                    var request = service.Purchases.Products.Get("com.patronum.synword", inAppItemId, purchaseToken);
                    var purchaseState = request.Execute();
                }
            } catch (Google.GoogleApiException ex) {
                int statusCode = ex.Error.Code;
                if(statusCode == 400) {
                    throw new InvalidPurchaseTokenException();
                }
                else if(statusCode == 502) {
                    //TODO хранить запрос пользователя, и отправить как только google api будет доступен
                }
            }
        }
    }
}
