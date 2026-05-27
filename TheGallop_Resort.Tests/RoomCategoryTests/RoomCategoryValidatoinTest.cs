using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class RoomCategoryValidationTest
{
    [TestMethod]
    public void Validate_NegativeCategoryPrice_ReturnError()
    {
        var dto = new RoomCategoryDTO
        {
            Type = RoomType.DoubleBed,
            CategoryPrice = -100,
            RoomDetailId = 1
        };

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto);

        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        isValid.Should().BeFalse();
        validationResults.Should().Contain(x => x.MemberNames.Contains(nameof(RoomCategoryDTO.CategoryPrice)));
    }
}