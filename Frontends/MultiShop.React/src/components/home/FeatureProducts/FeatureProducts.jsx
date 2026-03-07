import { useRef, useEffect, useState } from 'react'
import './FeatureProducts.css'

function formatPrice(price) {
  return new Intl.NumberFormat('tr-TR').format(price) + ' ₺'
}

function ProductStatLine({ item, index }) {
  const numClass = 'product-card-stat-num'
  if (index === 0) return (<> <span className={numClass}>{item.favoriteCount}</span> kişi favoriledi </>)
  if (index === 1) return (<> <span className={numClass}>{item.cartCount}</span> kişinin sepetinde </>)
  return (<> <span className={numClass}>{item.viewCount}</span> kişi inceledi </>)
}

function StarRating({ value }) {
  const v = Math.min(5, Math.max(0, Number(value)))
  const full = Math.round(v)
  return (
    <span className="product-rating-stars">
      {[1, 2, 3, 4, 5].map((i) => (
        <i key={i} className={`fa fa-star ${i <= full ? 'text-primary' : 'product-rating-star-empty'}`} />
      ))}
      <span className="product-rating-value">{value}</span>
    </span>
  )
}

const PRODUCT_CARD_WIDTH = 185
const PRODUCT_GAP = 20
const PRODUCT_SCROLL_AMOUNT = PRODUCT_CARD_WIDTH + PRODUCT_GAP
const AUTO_SCROLL_INTERVAL_MS = 4000
const STAT_ROTATE_MS = 2000

// Ürün isimleriyle uyumlu placeholder görseller (Lorem Flickr - etiket bazlı)
const productImage = (tags, id) => `https://loremflickr.com/400/400/${tags}?lock=${id}`

const FAKE_PRODUCTS = [
  { productId: 1, productName: 'Kablosuz Kulaklık', productPrice: 299, productImageUrl: productImage('wireless,headphones', 1), favoriteCount: 124, cartCount: 38, viewCount: 892, rating: 4.2 },
  { productId: 2, productName: 'Akıllı Saat', productPrice: 899, productImageUrl: productImage('smartwatch', 2), favoriteCount: 256, cartCount: 71, viewCount: 1204, rating: 4.5 },
  { productId: 3, productName: 'Bluetooth Hoparlör', productPrice: 449, productImageUrl: productImage('bluetooth,speaker', 3), favoriteCount: 89, cartCount: 22, viewCount: 567, rating: 4.0 },
  { productId: 4, productName: 'Mekanik Klavye', productPrice: 599, productImageUrl: productImage('mechanical,keyboard', 4), favoriteCount: 312, cartCount: 95, viewCount: 2103, rating: 4.7 },
  { productId: 5, productName: 'Kablosuz Mouse', productPrice: 199, productImageUrl: productImage('wireless,mouse', 5), favoriteCount: 67, cartCount: 41, viewCount: 445, rating: 4.3 },
  { productId: 6, productName: 'Powerbank 20000mAh', productPrice: 349, productImageUrl: productImage('powerbank', 6), favoriteCount: 178, cartCount: 53, viewCount: 734, rating: 4.4 },
  { productId: 7, productName: 'Telefon Kılıfı', productPrice: 89, productImageUrl: productImage('phone,case', 7), favoriteCount: 45, cartCount: 128, viewCount: 623, rating: 3.8 },
  { productId: 8, productName: 'USB-C Hub', productPrice: 279, productImageUrl: productImage('usb,hub', 8), favoriteCount: 92, cartCount: 29, viewCount: 412, rating: 4.1 },
  { productId: 9, productName: 'Ekran Koruyucu', productPrice: 59, productImageUrl: productImage('screen,protector', 9), favoriteCount: 34, cartCount: 86, viewCount: 289, rating: 3.9 },
  { productId: 10, productName: 'Laptop Stand', productPrice: 189, productImageUrl: productImage('laptop,stand', 10), favoriteCount: 156, cartCount: 44, viewCount: 801, rating: 4.6 },
  { productId: 11, productName: 'Webcam Full HD', productPrice: 429, productImageUrl: productImage('webcam', 11), favoriteCount: 203, cartCount: 62, viewCount: 956, rating: 4.5 },
  { productId: 12, productName: 'Mikrofon Seti', productPrice: 549, productImageUrl: productImage('microphone', 12), favoriteCount: 134, cartCount: 37, viewCount: 678, rating: 4.3 },
  { productId: 13, productName: 'Tablet Kılıfı', productPrice: 149, productImageUrl: productImage('tablet,case', 13), favoriteCount: 78, cartCount: 51, viewCount: 534, rating: 4.0 },
  { productId: 14, productName: 'Şarj Aleti Hızlı', productPrice: 129, productImageUrl: productImage('phone,charger', 14), favoriteCount: 112, cartCount: 73, viewCount: 445, rating: 4.2 },
  { productId: 15, productName: 'Kablo Yönetim Seti', productPrice: 79, productImageUrl: productImage('cable,organizer', 15), favoriteCount: 41, cartCount: 19, viewCount: 198, rating: 3.7 },
  { productId: 16, productName: 'Oyuncu Koltuğu', productPrice: 2499, productImageUrl: productImage('gaming,chair', 16), favoriteCount: 445, cartCount: 88, viewCount: 1876, rating: 4.8 },
  { productId: 17, productName: 'LED Masa Lambası', productPrice: 179, productImageUrl: productImage('desk,lamp', 17), favoriteCount: 98, cartCount: 34, viewCount: 512, rating: 4.4 },
  { productId: 18, productName: 'Dokunmatik Kalem', productPrice: 399, productImageUrl: productImage('stylus,pen', 18), favoriteCount: 167, cartCount: 48, viewCount: 723, rating: 4.5 },
  { productId: 19, productName: 'Taşınabilir SSD 1TB', productPrice: 899, productImageUrl: productImage('portable,ssd', 19), favoriteCount: 289, cartCount: 76, viewCount: 1345, rating: 4.6 },
  { productId: 20, productName: 'Kamera Tripod', productPrice: 249, productImageUrl: productImage('camera,tripod', 20), favoriteCount: 73, cartCount: 25, viewCount: 367, rating: 4.1 },
  { productId: 21, productName: 'Gaming Headset', productPrice: 679, productImageUrl: productImage('gaming,headset', 21), favoriteCount: 334, cartCount: 102, viewCount: 1654, rating: 4.7 },
  { productId: 22, productName: 'Akıllı Bileklik', productPrice: 329, productImageUrl: productImage('fitness,band', 22), favoriteCount: 198, cartCount: 59, viewCount: 889, rating: 4.3 },
  { productId: 23, productName: 'E-Reader Kılıfı', productPrice: 119, productImageUrl: productImage('ereader,case', 23), favoriteCount: 56, cartCount: 31, viewCount: 276, rating: 3.9 },
  { productId: 24, productName: 'Kablosuz Şarj Pedi', productPrice: 269, productImageUrl: productImage('wireless,charger', 24), favoriteCount: 145, cartCount: 42, viewCount: 612, rating: 4.2 },
  { productId: 25, productName: 'Laptop Çantası', productPrice: 389, productImageUrl: productImage('laptop,bag', 25), favoriteCount: 122, cartCount: 36, viewCount: 548, rating: 4.4 },
  { productId: 26, productName: 'HDMI Kablosu', productPrice: 69, productImageUrl: productImage('hdmi,cable', 26), favoriteCount: 28, cartCount: 94, viewCount: 234, rating: 3.8 },
  { productId: 27, productName: 'Temizlik Seti', productPrice: 49, productImageUrl: productImage('cleaning,electronics', 27), favoriteCount: 39, cartCount: 67, viewCount: 189, rating: 3.6 },
  { productId: 28, productName: 'Adaptör Çoklu', productPrice: 159, productImageUrl: productImage('power,adapter', 28), favoriteCount: 84, cartCount: 27, viewCount: 398, rating: 4.0 },
  { productId: 29, productName: 'Kulak İçi Kulaklık', productPrice: 229, productImageUrl: productImage('earbuds', 29), favoriteCount: 211, cartCount: 64, viewCount: 1023, rating: 4.5 },
  { productId: 30, productName: 'Ekran Temizleyici', productPrice: 39, productImageUrl: productImage('screen,cleaner', 30), favoriteCount: 22, cartCount: 58, viewCount: 156, rating: 3.5 },
]

export function FeatureProducts() {
  const scrollRef = useRef(null)
  const directionRef = useRef(1)
  const [statIndex, setStatIndex] = useState(0)
  const list = FAKE_PRODUCTS

  useEffect(() => {
    const id = setInterval(() => setStatIndex((i) => (i + 1) % 3), STAT_ROTATE_MS)
    return () => clearInterval(id)
  }, [])

  const scrollLeft = () => {
    if (scrollRef.current) scrollRef.current.scrollBy({ left: -PRODUCT_SCROLL_AMOUNT, behavior: 'smooth' })
  }
  const scrollRight = () => {
    if (scrollRef.current) scrollRef.current.scrollBy({ left: PRODUCT_SCROLL_AMOUNT, behavior: 'smooth' })
  }

  useEffect(() => {
    const el = scrollRef.current
    if (!el) return
    const tick = () => {
      const { scrollLeft: left, scrollWidth, clientWidth } = el
      const maxScroll = scrollWidth - clientWidth
      if (maxScroll <= 0) return
      if (directionRef.current === 1) {
        if (left >= maxScroll - 2) {
          directionRef.current = -1
          el.scrollBy({ left: -PRODUCT_SCROLL_AMOUNT, behavior: 'smooth' })
        } else {
          el.scrollBy({ left: PRODUCT_SCROLL_AMOUNT, behavior: 'smooth' })
        }
      } else {
        if (left <= 2) {
          directionRef.current = 1
          el.scrollBy({ left: PRODUCT_SCROLL_AMOUNT, behavior: 'smooth' })
        } else {
          el.scrollBy({ left: -PRODUCT_SCROLL_AMOUNT, behavior: 'smooth' })
        }
      }
    }
    const id = setInterval(tick, AUTO_SCROLL_INTERVAL_MS)
    return () => clearInterval(id)
  }, [])

  return (
    <div id="products" className="container-fluid pt-5 pb-3">
      <h2 className="section-title position-relative text-uppercase mx-xl-5 mb-4">
        <span className="bg-secondary pr-3">Öne Çıkan Ürünler</span>
      </h2>
      <div className="products-slider-wrap">
        <button type="button" className="products-slider-btn" onClick={scrollLeft} aria-label="Sola kaydır">
          <i className="fas fa-chevron-left" />
        </button>
        <div className="products-scroll-wrap" ref={scrollRef}>
          <div className="products-slider-row">
            {list.map((item) => {
              const oldPrice = item.productPrice * 1.15
              return (
                <div key={item.productId} className="products-card-wrap">
                  <div className="product-item bg-light mb-4">
                    <div className="product-img position-relative overflow-hidden">
                      <img className="img-fluid w-100" src={item.productImageUrl} alt={item.productName} />
                      <a className="product-favorite" href="" aria-label="Favorilere ekle" title="Favorilere ekle">
                        <i className="far fa-heart" />
                      </a>
                    </div>
                    <div className="product-card-info">
                      <a className="product-card-name text-decoration-none text-truncate d-block" href={`/ProductList/ProductDetail/${item.productId}`}>
                        {item.productName}
                      </a>
                      <div className="product-card-stat" key={statIndex}>
                        <ProductStatLine item={item} index={statIndex} />
                      </div>
                      <div className="product-card-rating">
                        <StarRating value={item.rating} />
                      </div>
                      <div className="product-card-price">
                        <span className="product-price-current">{formatPrice(item.productPrice)}</span>
                        <span className="product-price-old"><del>{formatPrice(oldPrice)}</del></span>
                      </div>
                    </div>
                  </div>
                </div>
              )
            })}
          </div>
        </div>
        <button type="button" className="products-slider-btn" onClick={scrollRight} aria-label="Sağa kaydır">
          <i className="fas fa-chevron-right" />
        </button>
      </div>
    </div>
  )
}
