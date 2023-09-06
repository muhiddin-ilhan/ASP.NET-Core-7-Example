using System;
using System.Linq.Expressions;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;


        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult? errorResult = BusinessRules.Run(
                 CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                 CheckIfProductNameExists(product.ProductName),
                 CheckCategoryLimitCorrect()
                 );

            if (errorResult != null)
            {
                return errorResult;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }


        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }


        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }


        public IDataResult<Product?> GetById(int productId)
        {
            return new SuccessDataResult<Product?>(_productDal.Get(p => p.ProductId == productId));
        }


        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result >= 100)
            {
                return new ErrorResult("Bir kategoride en fazla 100 ürün olabilir");
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult("Bu ürün ismi daha önce kullanılmış");
            }

            return new SuccessResult();
        }

        private IResult CheckCategoryLimitCorrect()
        {
            var result = _categoryService.GetAll().Data;

            if(result != null)
            {
                if(result.Count > 15)
                {
                    return new ErrorResult("Kategori sayısı 15'den büyük olamaz");
                }
            }

            return new SuccessResult();
        }
    }
}

