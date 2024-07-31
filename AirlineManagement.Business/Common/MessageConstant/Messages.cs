using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Common.MessageConstant
{
    public static class Messages
    {
        public const string CustomerNotFound = "Müşteri bulunamadı.";
        public const string CustomerDeletionFailed = "Müşteri silme işlemi başarısız oldu.";
        public const string CustomerUpdateFailed = "Müşteri güncelleme işlemi başarısız oldu.";
        public const string CustomerSearchFailed = "Müşteri arama işlemi başarısız oldu.";
        public const string CustomerAlreadyDeleted = "Müşteri zaten silinmiş veya bulunamadı.";
        public const string NoCustomersFound = "Arama kriterlerine uygun müşteri bulunamadı.";
        public const string CustomerDeletionSuccessful = "Müşteri silme işlemi başarıyla gerçekleştirildi.";
        public const string CustomerUpdateSuccessful = "Müşteri güncelleme işlemi başarıyla gerçekleştirildi.";

        
        /// <summary>
        ///  Airline Const Message
        /// </summary>
        public const string AirlineNotFound = "Havayolu bulunamadı.";
        public const string AirlineCreationFailed = "Havayolu oluşturulurken bir hata oluştu.";
        public const string AirlineUpdateFailed = "Havayolu güncellenirken bir hata oluştu.";
        public const string AirlineDeletionFailed = "Havayolu silinirken bir hata oluştu.";
        public const string AirlinesRetrievedSuccessfully = "Havayolları başarıyla alındı.";
        public const string AirlineDetailsRetrievedSuccessfully = "Havayolu detayları başarıyla alındı.";
        public const string AirlineCreatedSuccessfully = "Havayolu başarıyla oluşturuldu.";
        public const string AirlineUpdatedSuccessfully = "Havayolu başarıyla güncellendi.";
        public const string AirlineDeletedSuccessfully = "Havayolu başarıyla silindi.";

        /// <summary>
        ///  Flight Const Message
        /// </summary>
        public const string FlightNotFound = "Uçuş bulunamadı.";
        public const string FlightDeletionFailed = "Uçuş silme işlemi başarısız oldu.";
        public const string FlightUpdateFailed = "Uçuş güncelleme işlemi başarısız oldu.";
        public const string FlightSearchFailed = "Uçuş arama işlemi başarısız oldu.";
        public const string FlightAlreadyDeleted = "Uçuş zaten silinmiş veya bulunamadı.";
        public const string NoFlightsFound = "Arama kriterlerine uygun uçuş bulunamadı.";
        public const string FlightDeletionSuccessful = "Uçuş silme işlemi başarıyla gerçekleştirildi.";
        public const string FlightUpdateSuccessful = "Uçuş güncelleme işlemi başarıyla gerçekleştirildi.";
        public const string FlightSearchSuccessful = "Arama sonuçları başarıyla alındı.";

        /// <summary>
        ///  Reservation Const Message
        /// </summary>
        public const string ReservationNotFound = "Rezervasyon bulunamadı.";
        public const string ReservationDeletionFailed = "Rezervasyon silme işlemi başarısız oldu.";
        public const string ReservationUpdateFailed = "Rezervasyon güncelleme işlemi başarısız oldu.";
        public const string ReservationSearchFailed = "Rezervasyon arama işlemi başarısız oldu.";
        public const string ReservationAlreadyDeleted = "Rezervasyon zaten silinmiş veya bulunamadı.";
        public const string NoReservationsFound = "Arama kriterlerine uygun rezervasyon bulunamadı.";
        public const string ReservationDeletionSuccessful = "Rezervasyon silme işlemi başarıyla gerçekleştirildi.";
        public const string ReservationUpdateSuccessful = "Rezervasyon güncelleme işlemi başarıyla gerçekleştirildi.";
        public const string ReservationSearchSuccessful = "Rezervasyonlar başarıyla alındı.";

    }
}
