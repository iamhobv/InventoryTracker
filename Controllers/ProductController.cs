using System.Threading.Tasks;
using InventoryTracker.CQRS;
using InventoryTracker.CQRS.Products.Commands;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<GeneralResponse>> AddProduct(AddProductDTO addProductDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddProductCommand addProduct = addProductDTO.Map<AddProductCommand>();
                    var result = await mediator.Send(addProduct);
                    if (result)
                    {

                        await mediator.Send(new SaveChanges());
                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = "Product has been added"
                        };
                    }
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Product already exist"
                    };
                }
                catch (Exception e)
                {

                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = e.Message
                    };
                }



            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }


        [HttpPut("Update")]
        public async Task<ActionResult<GeneralResponse>> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateProductCommand productCommand = updateProductDTO.Map<UpdateProductCommand>();
                    var result = await mediator.Send(productCommand);
                    if (result)
                    {
                        await mediator.Send(new SaveChanges());
                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = "Product is updated"
                        };
                    }
                }
                catch (Exception e)
                {

                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = e.Message
                    };
                }



            }

            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }



        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<GeneralResponse>> deleteProduct(int id)
        {
            var result = await mediator.Send(new DeleteProductCommand() { Id = id });

            if (result)
            {
                await mediator.Send(new SaveChanges());


                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = "Product has been deleted"
                };

            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = "Product is not exist"
            };
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<GeneralResponse>> GetAllProducts()
        {
            try
            {
                IEnumerable<GetProductDTO> products = await mediator.Send(new GetAllProductQuery());

                if (products.Count() == 0)
                {
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "No Products"
                    };
                }

                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = products
                };
            }
            catch (Exception)
            {

                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "There are some error happened"
                };
            }
        }

        [HttpGet("GetByCategory/{CategoryId:int}")]
        public async Task<ActionResult<GeneralResponse>> GetCategoryProducts(int CategoryId)
        {
            try
            {
                var result = await mediator.Send(new GetProductByCategoryIdQuery() { CategoryId = CategoryId });
                if (result.Count() == 0)
                {
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Invalid Category"
                    };

                }
                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = result
                };

            }
            catch (Exception)
            {

                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Error occured"
                };
            }



        }



        [HttpGet("GetById/{Id:int}")]
        public async Task<ActionResult<GeneralResponse>> GetById(int Id)
        {
            var product = await mediator.Send(new GetProductByIdQuery() { Id = Id });

            if (product != null)
            {
                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = product
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = "Product is not exist"
            };
        }
    }
}
