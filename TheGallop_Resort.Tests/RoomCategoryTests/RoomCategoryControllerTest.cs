using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace TheGallop_Resort.Tests.RoomCategoryTests
{
    public class RoomCategoryControllerTests
    {
        [Fact]
        public async Task AddRoomCategory_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var dto = new RoomCategoryDTO
            {
                Type = RoomType.DoubleBed,
                CategoryPrice = 1800,
                RoomDetailId = 1
            };

            var createdRoomCategory = new RoomCategory
            { 
                Id = 1,
                Type = RoomType.DoubleBed,
                CategoryPrice = 1800,
                RoomDetailId = 1
            };

            var serviceMock = new Mock<IRoomCategoryService>();

            serviceMock.Setup(s => s.AddRoomCategoryAsync(dto))
                .ReturnsAsync(ServiceResult<RoomCategory>.Ok(createdRoomCategory));

            var controller = new RoomCategoryController(serviceMock.Object);

            // Act
            var result = await controller.AddRoomCategory(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRoomCategory = Assert.IsType<RoomCategory>(okResult.Value);

            Assert.Equal(1, returnedRoomCategory.Id);
            Assert.Equal(RoomType.DoubleBed, returnedRoomCategory.Type);
            Assert.Equal(1800, returnedRoomCategory.CategoryPrice);
            Assert.Equal(1, returnedRoomCategory.RoomDetailId);
            

        }
    }
}
