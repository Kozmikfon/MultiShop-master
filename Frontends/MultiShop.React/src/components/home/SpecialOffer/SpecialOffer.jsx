export function SpecialOffer({ items = [] }) {
  if (!items.length) return null

  return (
    <div className="col-lg-4">
      {items.map((item) => (
        <div key={item.specialOfferId} className="product-offer mb-30" style={{ height: 200 }}>
          <img className="img-fluid" src={item.imageUrl} alt={item.title} />
          <div className="offer-text">
            <h6 className="text-white text-uppercase">{item.title}</h6>
            <h3 className="text-white mb-3">{item.subTitle}</h3>
            <a href="#products" className="btn btn-primary">
              Alışverişe Başlayın
            </a>
          </div>
        </div>
      ))}
    </div>
  )
}
