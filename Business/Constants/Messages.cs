using System;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Constants
{
	public static class Messages
	{
		public static string ProductAdded = "Ürün Eklendi";
		public static string ProductNameInvalid = "Ürün İsmi Geçersiz";
        public static string MaintenanceTime = "Server Bakımda";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string AuthorizationDenied = "Yetki Reddedildi";
        public static string UserRegistered = "Kullanıcı Kaydedildi";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string SuccessfulLogin = "Başarılı Giriş";
        public static string UserAlreadyExists = "Kullanıcı Zaten Mevcut";
        public static string AccessTokenCreated = "Access Token Oluşturuldu";
    }
}

