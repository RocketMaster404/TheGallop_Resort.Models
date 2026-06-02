using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.DTOs.Validators;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests.RoomReservationTests
{
    [TestClass]
    public class RoomReservationValidatorTest
    {
        [TestMethod]
        public void CreateRoomReservation_CheckOutCanNotBeBeforeCheckIn_ReturnError()
        {
            var validator = new CreateRoomReservationDTOValidator();
            var roomReservation = new CreateRoomReservationDTO
            (
                1,
                CheckIn: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(10),
                CheckOut: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(5),
                2,
                0,
                RoomType.Suite
            );

            var result = validator.TestValidate(roomReservation);
            result.ShouldHaveValidationErrorFor(x => x.CheckOut);
        }

        [TestMethod]
        public void CreateBooking_CheckInDateInPast_RetornError()
        {
            var validator = new CreateRoomReservationDTOValidator();
            var roomReservation = new CreateRoomReservationDTO
            (
                1,
                CheckIn: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-2),
                CheckOut: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(5),
                2,
                0,
                RoomType.Suite
            );

            var result = validator.TestValidate(roomReservation);
            result.ShouldHaveValidationErrorFor(x => x.CheckIn);
        }
    }
}
