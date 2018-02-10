using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.DAL.Interfaces;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.BLL.Infrastructure;
using AutoMapper;
using HotelBookingApp.BLL.BusinessModels;

namespace HotelBookingApp.BLL.Services
{
    /// <summary>
    /// Class that implements funcionality of IApartmentService interface. It has functions for creating, getting, updating data about apartments.
    /// </summary>
    public class ApartmentService : IApartmentService
    {
        IUnitOfWork DataBase { get; set; }

        /// <summary>
        /// Constructor that getting IUnitOfWork of work parameter and initializing DataBase class property by that parametr.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ApartmentService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public void AddApartament(ApartmentDTO apartment)
        {
            if (apartment == null)
            {
                throw new ValidationException("Empty arpartment instance argument", "");
            }

            var appsWithExistingNumber = FindApartment(a => a.Number == apartment.Number);

            if (appsWithExistingNumber != null)
            {
                throw new ValidationException("Apartment with " + apartment.Number.ToString() + " number is already existing", "");
            }

            Apartment newApartment = new Apartment
            {
                ApartmentClassId = apartment.ApartmentClassId,
                Floor = apartment.Floor,
                Number = apartment.Number,
                NumberOfPlaces = apartment.NumberOfPlaces,
                NumberOfRooms = apartment.NumberOfRooms,
                PricePerDay = apartment.PricePerDay
            };

            try
            {
                DataBase.Apartments.Create(newApartment);
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in AddApartment method"
                throw ex;
            }
        }

        public void EditApartament(ApartmentDTO apartment)
        {
            if (apartment == null)
            {
                throw new ValidationException("Empty apartment instance argument", "");
            }

            try
            {
                //Mapper.Initialize(cfg => cfg.CreateMap<ApartmentDTO, Apartment>());
                DataBase.Apartments.Update(Mapper.Map<ApartmentDTO, Apartment>(apartment));
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in EditApartament method"
                throw ex;
            }
        }

        public void DeleteApartament(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty apartment id argument", "");
            }

            try
            {
                DataBase.Apartments.Delete(id.Value);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in DeleteApartament method"
                throw ex;
            }
        }

        public IEnumerable<ApartmentDTO> FindApartment(Func<Apartment, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ValidationException("Empty predicate argument", "");
            }
            try
            {
                var apartment = DataBase.Apartments.Find(predicate);

                if (apartment.Count() == 0)
                {
                    //throw new ValidationException("Apartment is not found", "");
                    return null;
                }

                //Mapper.Initialize(cfg => cfg.CreateMap<Apartment, ApartmentDTO>());

                return Mapper.Map<IEnumerable<Apartment>, IEnumerable<ApartmentDTO>>(apartment);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in FindApartment method"
                throw ex;
            }
        }

        public ApartmentDTO GetApartment(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty apartment id argument", "");
            }

            try
            {
                var apartment = DataBase.Apartments.Get(id.Value);

                if (apartment == null)
                {
                    throw new ValidationException("Apartment is not found", "");
                }

                //Mapper.Initialize(cfg => cfg.CreateMap<Apartment, ApartmentDTO>());

                return Mapper.Map<Apartment, ApartmentDTO>(apartment);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in GetApartment method"
                throw ex;
            }
        }

        public IEnumerable<ApartmentDTO> GetApartments()
        {
            try
            {
                return Mapper.Map<IEnumerable<Apartment>, List<ApartmentDTO>>(DataBase.Apartments.GetAll().Where(a => a.IsClosed == false));
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in GetApartments method"
                throw ex;
            }
        }

        public IEnumerable<ApartmentDTO> GetAllApartments()
        {
            try
            {
                return Mapper.Map<IEnumerable<Apartment>, List<ApartmentDTO>>(DataBase.Apartments.GetAll());
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in GetApartments method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that allows to close apartment by some reason. For maintanence for example.
        /// </summary>
        /// <param name="id"></param>
        public void CloseApartment(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty apartment id argument", "");
            }

            try
            {
                var apartment = Mapper.Map<Apartment, ApartmentDTO>(DataBase.Apartments.Get(id.Value));

                if (!IsApartmentHasBookingsInFuture(apartment.Id))
                {
                    apartment.IsClosed = true;
                    DataBase.Apartments.Update(Mapper.Map<ApartmentDTO,Apartment>(apartment));
                    DataBase.Save();
                }
                else
                {
                    string message = "Impossible to close apartment number " + apartment.Number + " because of active bookings for it.";

                    throw new ValidationException(message, "");
                }
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in CloseApartment method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that allows to open previously closed apartments.
        /// </summary>
        /// <param name="id"></param>
        public void OpenApartment(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty apartment id argument", "");
            }

            try
            {
                var apartment = DataBase.Apartments.Get(id.Value);
                apartment.IsClosed = false;
                DataBase.Apartments.Update(apartment);
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in OpenApartment method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that returns collection of valid apartments for user request.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ApartmentDTO> GetApartmentsForPick(int? id)
        {
            try
            {
                var request = Mapper.Map<Request, RequestDTO>(DataBase.Requests.Get(id.Value));
                var aps = Mapper.Map<List<Apartment>, List<ApartmentDTO>>(DataBase.Apartments.Find(a => a.ApartmentClassId == request.ApartmentClassId).Where(a => a.NumberOfPlaces >= request.NumberOfPlaces).Where(a => a.IsClosed == false).ToList());
                var bookings = Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(DataBase.Bookings.GetAll());

                return DateValidation.GetValidApartments(aps, bookings, request.DateOfArrival, request.DaysOfStay);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in GetApartmentsForPick method"
                throw ex;
            }
        }

        public bool IsCorrectApartmentForPickedDate(int? id, DateTime date, int days)
        {
            try
            {
                var bookings = Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(DataBase.Bookings.GetAll());

                return DateValidation.IsValidApartment(id.Value, bookings, date, days);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in IsCorrectApartmentForPickedDate in GetApartmentsForPick method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that returns collection of apartments for user, that wants to pick apartment by self.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public IEnumerable<ApartmentDTO> GetApartmentsForPickForBooking(BookingDTO booking)
        {
            try
            {
                var aps = Mapper.Map<List<Apartment>, List<ApartmentDTO>>(DataBase.Apartments.Find(a => a.ApartmentClassId == booking.ApartmentClassId).Where(a => a.NumberOfPlaces >= booking.NumberOfPlaces).Where(a => a.IsClosed == false).ToList());
                var bookings = Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(DataBase.Bookings.GetAll());

                return DateValidation.GetValidApartments(aps, bookings, booking.DateOfArrival, booking.DaysOfArrival);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in GetApartmentsForPick method"
                throw ex;
            }
        }

        public bool IsApartmentHasBookingsInFuture(int? id)
        {
            try
            {
                var apartment = Mapper.Map<Apartment, ApartmentDTO>(DataBase.Apartments.Get(id.Value));
                var bookings = Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(DataBase.Bookings.Find(b => b.ApartmentId == apartment.Id)).ToList();

                if (ApartmentCloset.IsCloseableApartment(apartment, bookings))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in AppartmentService in IsApartmentHasBookingsInFuture method"
                throw ex;
            }
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
