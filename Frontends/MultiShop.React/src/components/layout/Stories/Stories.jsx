import { useRef } from 'react'
import './Stories.css'

/* 15 hikaye görünsün: item genişliği (72+16) = 88, scroll bir tıklamada 3 hikaye */
const STORY_ITEM_WIDTH = 72
const STORY_GAP = 16
const STORY_SCROLL_AMOUNT = (STORY_ITEM_WIDTH + STORY_GAP) * 3

const FAKE_STORIES = [
  { id: 1, sellerName: 'Tech Mağaza', avatarUrl: 'https://loremflickr.com/200/200/shop,store?lock=1', viewed: false },
  { id: 2, sellerName: 'Moda Evi', avatarUrl: 'https://loremflickr.com/200/200/fashion,clothes?lock=2', viewed: false },
  { id: 3, sellerName: 'Elektronik Dünyası', avatarUrl: 'https://loremflickr.com/200/200/electronics?lock=3', viewed: true },
  { id: 4, sellerName: 'Kitap Köşesi', avatarUrl: 'https://loremflickr.com/200/200/books?lock=4', viewed: false },
  { id: 5, sellerName: 'Spor Ürünleri', avatarUrl: 'https://loremflickr.com/200/200/sports?lock=5', viewed: true },
  { id: 6, sellerName: 'Ev Dekor', avatarUrl: 'https://loremflickr.com/200/200/home,decor?lock=6', viewed: false },
  { id: 7, sellerName: 'Kozmetik Plus', avatarUrl: 'https://loremflickr.com/200/200/cosmetics?lock=7', viewed: false },
  { id: 8, sellerName: 'Oyuncak Dünyası', avatarUrl: 'https://loremflickr.com/200/200/toys?lock=8', viewed: true },
  { id: 9, sellerName: 'Ayakkabı Durağı', avatarUrl: 'https://loremflickr.com/200/200/shoes?lock=9', viewed: false },
  { id: 10, sellerName: 'Mücevherat', avatarUrl: 'https://loremflickr.com/200/200/jewelry?lock=10', viewed: false },
  { id: 11, sellerName: 'Bahçe Market', avatarUrl: 'https://loremflickr.com/200/200/garden?lock=11', viewed: true },
  { id: 12, sellerName: 'Süper Fırsat', avatarUrl: 'https://loremflickr.com/200/200/sale,discount?lock=12', viewed: false },
  { id: 13, sellerName: 'Organik Ürünler', avatarUrl: 'https://loremflickr.com/200/200/organic,food?lock=13', viewed: false },
  { id: 14, sellerName: 'Oyuncu Market', avatarUrl: 'https://loremflickr.com/200/200/gaming?lock=14', viewed: true },
  { id: 15, sellerName: 'Mobilya Ev', avatarUrl: 'https://loremflickr.com/200/200/furniture?lock=15', viewed: false },
  { id: 16, sellerName: 'Anne Bebek', avatarUrl: 'https://loremflickr.com/200/200/baby?lock=16', viewed: false },
  { id: 17, sellerName: 'Pet Shop', avatarUrl: 'https://loremflickr.com/200/200/pet,dog?lock=17', viewed: true },
  { id: 18, sellerName: 'Hobi Market', avatarUrl: 'https://loremflickr.com/200/200/hobby?lock=18', viewed: false },
  { id: 19, sellerName: 'Outdoor Store', avatarUrl: 'https://loremflickr.com/200/200/camping,outdoor?lock=19', viewed: false },
  { id: 20, sellerName: 'El Yapımı', avatarUrl: 'https://loremflickr.com/200/200/handmade?lock=20', viewed: true },
]

export function Stories() {
  const scrollRef = useRef(null)

  const scrollLeft = () => {
    if (scrollRef.current) scrollRef.current.scrollBy({ left: -STORY_SCROLL_AMOUNT, behavior: 'smooth' })
  }
  const scrollRight = () => {
    if (scrollRef.current) scrollRef.current.scrollBy({ left: STORY_SCROLL_AMOUNT, behavior: 'smooth' })
  }

  return (
    <div className="stories-wrapper">
      <div className="stories-slider-wrap">
        <button type="button" className="stories-slider-btn" onClick={scrollLeft} aria-label="Hikayeleri sola kaydır">
          <i className="fas fa-chevron-left" />
        </button>
        <div className="stories-scroll-wrap">
          <div className="stories-scroll" ref={scrollRef}>
            {FAKE_STORIES.map((story) => (
              <button
                key={story.id}
                type="button"
                className={`story-item ${story.viewed ? 'story-viewed' : ''}`}
                aria-label={`${story.sellerName} hikayesini görüntüle`}
              >
                <span className="story-ring">
                  <span className="story-avatar">
                    <img src={story.avatarUrl} alt="" />
                  </span>
                </span>
                <span className="story-name">{story.sellerName}</span>
              </button>
            ))}
          </div>
        </div>
        <button type="button" className="stories-slider-btn" onClick={scrollRight} aria-label="Hikayeleri sağa kaydır">
          <i className="fas fa-chevron-right" />
        </button>
      </div>
    </div>
  )
}
