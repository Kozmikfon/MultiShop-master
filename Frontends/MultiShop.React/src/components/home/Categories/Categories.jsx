import { useRef, useEffect } from 'react'
import './Categories.css'

const FAKE_CATEGORIES = [
  { categoryID: 1, categoryName: 'Elektronik', imageUrl: 'https://picsum.photos/seed/elektronik/100/100' },
  { categoryID: 2, categoryName: 'Giyim', imageUrl: 'https://picsum.photos/seed/giyim/100/100' },
  { categoryID: 3, categoryName: 'Ev & Yaşam', imageUrl: 'https://picsum.photos/seed/ev/100/100' },
  { categoryID: 4, categoryName: 'Kozmetik', imageUrl: 'https://picsum.photos/seed/kozmetik/100/100' },
  { categoryID: 5, categoryName: 'Spor', imageUrl: 'https://picsum.photos/seed/spor/100/100' },
  { categoryID: 6, categoryName: 'Kitap & Hobi', imageUrl: 'https://picsum.photos/seed/kitap/100/100' },
  { categoryID: 7, categoryName: 'Oyuncak', imageUrl: 'https://picsum.photos/seed/oyuncak/100/100' },
  { categoryID: 8, categoryName: 'Süpermarket', imageUrl: 'https://picsum.photos/seed/market/100/100' },
]

/* 1 kart genişliği + aralık = her tıklamada tam 1 kategori kayar */
const CARD_WIDTH = 150
const GAP = 20
const SCROLL_AMOUNT = CARD_WIDTH + GAP
const AUTO_SCROLL_INTERVAL_MS = 4000

export function Categories() {
  const scrollRef = useRef(null)
  const directionRef = useRef(1) // 1 = sağa, -1 = sola

  const scrollLeft = () => {
    if (scrollRef.current) scrollRef.current.scrollBy({ left: -SCROLL_AMOUNT, behavior: 'smooth' })
  }
  const scrollRight = () => {
    if (scrollRef.current) scrollRef.current.scrollBy({ left: SCROLL_AMOUNT, behavior: 'smooth' })
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
          el.scrollBy({ left: -SCROLL_AMOUNT, behavior: 'smooth' })
        } else {
          el.scrollBy({ left: SCROLL_AMOUNT, behavior: 'smooth' })
        }
      } else {
        if (left <= 2) {
          directionRef.current = 1
          el.scrollBy({ left: SCROLL_AMOUNT, behavior: 'smooth' })
        } else {
          el.scrollBy({ left: -SCROLL_AMOUNT, behavior: 'smooth' })
        }
      }
    }

    const id = setInterval(tick, AUTO_SCROLL_INTERVAL_MS)
    return () => clearInterval(id)
  }, [])

  const list = FAKE_CATEGORIES

  return (
    <div id="categories" className="container-fluid pt-5">
      <h2 className="section-title position-relative text-uppercase mx-xl-5 mb-4">
        <span className="bg-secondary pr-3">Öne Çıkan Kategoriler</span>
      </h2>
      <div className="categories-slider-wrap">
        <button type="button" className="categories-slider-btn categories-slider-btn--left" onClick={scrollLeft} aria-label="Sola kaydır">
          <i className="fas fa-chevron-left" />
        </button>
        <div className="categories-scroll-wrap" ref={scrollRef}>
          <div className="categories-row">
            {list.map((item) => (
              <a key={item.categoryID} className="cat-item-link text-decoration-none" href="#products">
                <div className="cat-item cat-item--square">
                  <div className="cat-item__img-wrap">
                    <img className="cat-item__img" src={item.imageUrl} alt={item.categoryName} />
                  </div>
                  <h6 className="cat-item__name">{item.categoryName}</h6>
                </div>
              </a>
            ))}
          </div>
        </div>
        <button type="button" className="categories-slider-btn categories-slider-btn--right" onClick={scrollRight} aria-label="Sağa kaydır">
          <i className="fas fa-chevron-right" />
        </button>
      </div>
    </div>
  )
}
