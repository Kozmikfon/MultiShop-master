import './Banner.css'

/* Arka planda kullanılan, tema renkli büyük grafik: etiket (sol) */
function BannerTagGraphic() {
  return (
    <div className="layout-banner__graphic layout-banner__graphic--tag" aria-hidden="true">
      <svg viewBox="0 0 140 160" fill="none" xmlns="http://www.w3.org/2000/svg">
        <defs>
          <linearGradient id="banner-tag-gradient" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#1a7f37" stopOpacity="0.35" />
            <stop offset="50%" stopColor="#2e7d32" stopOpacity="0.2" />
            <stop offset="100%" stopColor="#0d2137" stopOpacity="0.15" />
          </linearGradient>
        </defs>
        <path
          d="M70 8 L128 70 L70 152 L12 70 Z"
          fill="url(#banner-tag-gradient)"
          stroke="rgba(26,127,55,0.25)"
          strokeWidth="1.5"
        />
        <circle cx="70" cy="55" r="18" fill="rgba(13,33,55,0.4)" stroke="rgba(255,255,255,0.08)" strokeWidth="1" />
      </svg>
    </div>
  )
}

/* Arka planda kullanılan, tema renkli grafik: yüzde % (sağ) - net okunur */
function BannerPercentGraphic() {
  return (
    <div className="layout-banner__graphic layout-banner__graphic--percent" aria-hidden="true">
      <svg viewBox="0 0 100 100" fill="none" xmlns="http://www.w3.org/2000/svg">
        <defs>
          <linearGradient id="banner-percent-gradient" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#2e7d32" stopOpacity="0.4" />
            <stop offset="100%" stopColor="#1a7f37" stopOpacity="0.25" />
          </linearGradient>
        </defs>
        <circle cx="28" cy="28" r="14" fill="none" stroke="url(#banner-percent-gradient)" strokeWidth="5" />
        <circle cx="72" cy="72" r="14" fill="none" stroke="url(#banner-percent-gradient)" strokeWidth="5" />
        <path d="M22 78 L78 22" stroke="url(#banner-percent-gradient)" strokeWidth="6" strokeLinecap="round" />
      </svg>
    </div>
  )
}

/* Arka planda kullanılan, tema renkli grafik: hediye kutusu (sağ) */
function BannerGiftGraphic() {
  return (
    <div className="layout-banner__graphic layout-banner__graphic--gift" aria-hidden="true">
      <svg viewBox="0 0 100 100" fill="none" xmlns="http://www.w3.org/2000/svg">
        <defs>
          <linearGradient id="banner-gift-gradient" x1="0%" y1="0%" x2="100%" y2="100%">
            <stop offset="0%" stopColor="#f5d77a" stopOpacity="0.35" />
            <stop offset="100%" stopColor="#2e7d32" stopOpacity="0.2" />
          </linearGradient>
        </defs>
        <rect x="15" y="35" width="70" height="50" rx="4" fill="url(#banner-gift-gradient)" stroke="rgba(245,215,122,0.3)" strokeWidth="1.5" />
        <path d="M50 35 L50 85 M15 55 L85 55" stroke="rgba(255,255,255,0.12)" strokeWidth="2" />
        <rect x="42" y="20" width="16" height="25" fill="url(#banner-gift-gradient)" stroke="rgba(245,215,122,0.3)" strokeWidth="1.5" />
      </svg>
    </div>
  )
}

export function Banner() {
  return (
    <div className="layout-banner">
      <div className="layout-banner__bg-pattern" aria-hidden="true" />
      <div className="layout-banner__bg-dots" aria-hidden="true" />
      <BannerTagGraphic />
      <BannerPercentGraphic />
      <BannerGiftGraphic />
      <div className="layout-banner__bg-blob layout-banner__bg-blob--1" aria-hidden="true" />
      <div className="layout-banner__bg-blob layout-banner__bg-blob--2" aria-hidden="true" />
      <div className="layout-banner__bg-blob layout-banner__bg-blob--3" aria-hidden="true" />
      <div className="layout-banner__shine" aria-hidden="true" />
      <div className="layout-banner__inner marketing-banner-wrapper">
        <a href="#products" className="layout-banner__text marketing-banner" data-testid="marketing-banner">
          <span className="layout-banner__left">
            <span className="layout-banner__text-inner">
              <span className="layout-banner__two-line">
                <span className="layout-banner__two-line-cart" aria-hidden="true"><i className="fas fa-shopping-cart" /></span>
                <span className="layout-banner__text--strong">Büyük Fırsat Günleri!</span>
              </span>
            </span>
          </span>
          <span className="layout-banner__center">
            <img src="/images/logos/logo.png" alt="FırsatKap" className="layout-banner__logo" />
          </span>
          <span className="layout-banner__right" aria-hidden="true" />
        </a>
      </div>
      <div className="layout-banner__border" aria-hidden="true" />
    </div>
  )
}
