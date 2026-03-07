import { useState, useEffect } from 'react'
import { SpecialOffer } from '../SpecialOffer/SpecialOffer'

export function Carousel({ items = [], specialOffers = [] }) {
  const [activeIndex, setActiveIndex] = useState(0)

  useEffect(() => {
    if (items.length <= 1) return
    const id = setInterval(() => {
      setActiveIndex((i) => (i + 1) % items.length)
    }, 5000)
    return () => clearInterval(id)
  }, [items.length])

  if (!items.length) return null

  return (
    <div className="home-carousel container-fluid mb-3">
      <div className="row px-xl-5">
        <div className="col-lg-8">
          <div className="carousel slide carousel-fade mb-30 mb-lg-0" style={{ minHeight: 430 }}>
            <div className="carousel-indicators">
              {items.map((_, i) => (
                <button
                  key={i}
                  type="button"
                  className={i === activeIndex ? 'active' : ''}
                  aria-current={i === activeIndex}
                  onClick={() => setActiveIndex(i)}
                />
              ))}
            </div>
            <div className="carousel-inner">
              {items.map((item, i) => (
                <div
                  key={item.featureSliderId}
                  className={`carousel-item position-relative ${i === activeIndex ? 'active' : ''}`}
                  style={{ height: 430 }}
                >
                  <img
                    className="position-absolute w-100 h-100"
                    src={item.imageUrl}
                    alt={item.title}
                    style={{ objectFit: 'cover' }}
                  />
                  <div className="carousel-caption d-flex flex-column align-items-center justify-content-center">
                    <div className="p-3" style={{ maxWidth: 700 }}>
                      <h1 className="display-4 text-white mb-3">{item.title}</h1>
                      <p className="mx-md-5 px-5">{item.description}</p>
                      <a className="btn btn-outline-light py-2 px-4 mt-3" href="#categories">
                        Alışverişe Başla
                      </a>
                    </div>
                  </div>
                </div>
              ))}
            </div>
            {items.length > 1 && (
              <>
                <button
                  className="carousel-control-prev"
                  type="button"
                  onClick={() => setActiveIndex((i) => (i - 1 + items.length) % items.length)}
                >
                  <span className="carousel-control-prev-icon" />
                </button>
                <button
                  className="carousel-control-next"
                  type="button"
                  onClick={() => setActiveIndex((i) => (i + 1) % items.length)}
                >
                  <span className="carousel-control-next-icon" />
                </button>
              </>
            )}
          </div>
        </div>
        <SpecialOffer items={specialOffers} />
      </div>
    </div>
  )
}
