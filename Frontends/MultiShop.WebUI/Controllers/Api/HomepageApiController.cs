using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.WebUI.Controllers.Api
{
    /// <summary>
    /// Anasayfa verilerini React ve diğer SPA istemcileri için JSON olarak döndürür.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HomepageApiController : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService;
        private readonly IFeatureService _featureService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOfferDiscountService _offerDiscountService;
        private readonly IBrandService _brandService;
        private readonly ISpecialOfferService _specialOfferService;
        private readonly IAboutService _aboutService;

        public HomepageApiController(
            IFeatureSliderService featureSliderService,
            IFeatureService featureService,
            ICategoryService categoryService,
            IProductService productService,
            IOfferDiscountService offerDiscountService,
            IBrandService brandService,
            ISpecialOfferService specialOfferService,
            IAboutService aboutService)
        {
            _featureSliderService = featureSliderService;
            _featureService = featureService;
            _categoryService = categoryService;
            _productService = productService;
            _offerDiscountService = offerDiscountService;
            _brandService = brandService;
            _specialOfferService = specialOfferService;
            _aboutService = aboutService;
        }

        /// <summary>
        /// Tüm anasayfa verilerini tek istekte döndürür (slider, özellikler, kategoriler, ürünler, kampanyalar, markalar, özel teklifler).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var slidersTask = _featureSliderService.GetAllFeatureSliderAsync();
            var featuresTask = _featureService.GetAllFeatureAsync();
            var categoriesTask = _categoryService.GetAllCategoryAsync();
            var productsTask = _productService.GetAllProductAsync();
            var offerDiscountsTask = _offerDiscountService.GetAllOfferDiscountAsync();
            var brandsTask = _brandService.GetAllBrandAsync();
            var specialOffersTask = _specialOfferService.GetAllSpecialOfferAsync();
            var aboutTask = _aboutService.GetAllAboutAsync();

            await Task.WhenAll(slidersTask, featuresTask, categoriesTask, productsTask, offerDiscountsTask, brandsTask, specialOffersTask, aboutTask);

            var response = new
            {
                featureSliders = await slidersTask,
                features = await featuresTask,
                categories = await categoriesTask,
                products = await productsTask,
                offerDiscounts = await offerDiscountsTask,
                brands = await brandsTask,
                specialOffers = await specialOffersTask,
                about = await aboutTask
            };

            return Ok(response);
        }
    }
}
