import { useRef, useEffect } from 'react'

export function Vendor({ items = [] }) {
  const scrollRef = useRef(null)

  useEffect(() => {
    if (!scrollRef.current || items.length <= 4) return
    const el = scrollRef.current
    let pos = 0
    const step = 1
    const id = setInterval(() => {
      pos += step
      if (pos >= el.scrollWidth - el.clientWidth) pos = 0
      el.scrollTo({ left: pos, behavior: 'auto' })
    }, 30)
    return () => clearInterval(id)
  }, [items.length])

  if (!items.length) return null

  return (
    <div className="container-fluid py-5">
      <div className="row px-xl-5">
        <div className="col">
          <div
            ref={scrollRef}
            className="vendor-carousel d-flex gap-3 overflow-auto flex-nowrap"
            style={{ scrollbarWidth: 'none', msOverflowStyle: 'none' }}
          >
            {items.map((item) => (
              <div
                key={item.brandId}
                className="bg-light p-4 flex-shrink-0 d-flex align-items-center justify-content-center"
                style={{ height: 120, width: 150 }}
              >
                <img src={item.imageUrl} alt={item.brandName} style={{ maxHeight: '100%', maxWidth: '100%', objectFit: 'contain' }} />
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}
