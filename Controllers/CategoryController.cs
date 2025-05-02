using InventoryTracker.CQRS;
using InventoryTracker.CQRS.Categories.Commands;
using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.Category.Commands;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<GeneralResponse>> AddCategoryAsync(AddCategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                bool result = await mediator.Send(new AddCategoryCommand() { CategoryName = categoryDTO.CategoryName, CategoryDescription = categoryDTO.CategoryDescription });

                if (result)
                {
                    await mediator.Send(new SaveChanges());

                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "Category added successfully!"
                    };
                }

                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Category is already exists"
                };
            }
            {
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = ModelState
                };
            }

        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<GeneralResponse>> GetAllCategoriesAsync()
        {
            var result = await mediator.Send(new GetAllCategoryQuery());

            if (result.Count() == 0)
            {

                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "No categories please add some"
                };
            }
            return new GeneralResponse()
            {
                IsPass = true,
                Data = result
            };
        }

        [HttpPut("Update")]
        public async Task<ActionResult<GeneralResponse>> UpdateCategoryAsync(EditCategoryDTO categoryFromRequest)
        {
            if (ModelState.IsValid)
            {
                UpdateCategoryCommand c = categoryFromRequest.Map<UpdateCategoryCommand>();

                var result = await mediator.Send(c);


                if (result)
                {

                    await mediator.Send(new SaveChanges());
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "Category has been updated"
                    };
                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Category is not exist"
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<GeneralResponse>> DeleteCategoryAsync(int id)
        {
            var result = await mediator.Send(new DeleteCategoryCommand() { CategoryID = id });


            if (result)
            {

                await mediator.Send(new SaveChanges());
                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = "Category has been deleted"
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = "Category is not exist"
            };

        }
    }
}
