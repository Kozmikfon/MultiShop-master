export function OfferDiscount({ items = [] }) {
  if (!items.length) return null

  return (
    <div className="container-fluid pt-5 pb-3">
      <div className="row px-xl-5">
        {items.map((item) => (
          <div key={item.offerDiscountId} className="col-md-6">
            <div className="product-offer mb-30" style={{ height: 300 }}>
              <img className="img-fluid" src={item.imageUrl} alt={item.title} />
              <div className="offer-text">
                <h6 className="text-white text-uppercase">{item.title}</h6>
                <h3 className="text-white mb-3">{item.subTitle}</h3>
                <a href="#products" className="btn btn-primary">
                  {item.buttonTitle}
                </a>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
