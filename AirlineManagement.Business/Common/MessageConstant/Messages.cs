using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Common.MessageConstant
{
    public static class Messages
    {
        public const string CheckInNotFound = "Check-in bulunamadı.";
        public const string CheckInDeletionFailed = "Check-in silme işlemi başarısız oldu.";
        public const string CheckInHardDeletionFailed = "Check-in kalıcı olarak silme işlemi başarısız oldu.";
        public const string CheckInUpdateFailed = "Check-in güncelleme işlemi başarısız oldu.";
        public const string CheckInFetchFailed = "Check-in getirme işlemi başarısız oldu.";
        public const string CheckInCreationFailed = "Check-in oluşturma işlemi başarısız oldu.";
        public const string CheckInFetchSuccessful = "Check-in başarıyla getirildi.";
        public const string CheckInCreationSuccessful = "Check-in başarıyla oluşturuldu.";
        public const string CheckInUpdateSuccessful = "Check-in başarıyla güncellendi.";
        public const string CheckInDeletionSuccessful = "Check-in başarıyla silindi.";
        public const string CheckInHardDeletionSuccessful = "Check-in kalıcı olarak başarıyla silindi.";


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
        public const string ReservationCreationSuccessful = "Rezervasyon başarıyla oluşturuldu.";
        public const string ReservationCreationFailed = "Rezervasyon oluşturma işlemi başarısız oldu.";
        public const string ReservationHardDeletionSuccessful = "Rezervasyon kalıcı olarak silindi.";


        /// <summary>
        ///  Passenger  Const Message
        /// </summary>
        public const string PassengerNotFound = "Yolcu bulunamadı.";
        public const string PassengerDeletionFailed = "Yolcu silme işlemi başarısız oldu.";
        public const string PassengerUpdateFailed = "Yolcu güncelleme işlemi başarısız oldu.";
        public const string PassengerFetchFailed = "Yolcu arama işlemi başarısız oldu.";
        public const string PassengerAlreadyDeleted = "Yolcu zaten silinmiş veya bulunamadı.";
        public const string NoPassengersFound = "Arama kriterlerine uygun yolcu bulunamadı.";
        public const string PassengerDeletionSuccessful = "Yolcu silme işlemi başarıyla gerçekleştirildi.";
        public const string PassengerUpdateSuccessful = "Yolcu güncelleme işlemi başarıyla gerçekleştirildi.";
        public const string PassengerFetchSuccessful = "Yolcular başarıyla alındı.";
        public const string PassengerCreationSuccessful = "Yolcu başarıyla oluşturuldu.";
        public const string PassengerCreationFailed = "Yolcu oluşturma işlemi başarısız oldu.";
        public const string PassengerHardDeletionSuccessful = "Yolcu kalıcı olarak silindi.";
        public const string PassengerHardDeletionFailed = "Yolcu kalıcı olarak silme işlemi başarısız oldu.";
        public const string PassengerIdMismatch = "Yolcu ID eşleşmesi başarısız oldu.";

    }
}
