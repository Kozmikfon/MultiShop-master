# MultiShop React - Anasayfa (JavaScript)

Anasayfa React (JavaScript) ile yazılmış, modüler bileşen yapısına sahip SPA'dır.

## Dosya yapısı

```
Frontends/MultiShop.React/
├── public/
├── src/
│   ├── api/
│   │   └── homepage.js              # Anasayfa API isteği (fetchHomepageData)
│   │
│   ├── components/
│   │   ├── layout/
│   │   │   ├── Header/
│   │   │   │   └── Header.jsx
│   │   │   ├── Footer/
│   │   │   │   └── Footer.jsx
│   │   │   └── index.js
│   │   │
│   │   └── home/                    # Anasayfa bölümleri (her biri kendi klasöründe)
│   │       ├── Carousel/
│   │       │   └── Carousel.jsx     # Slider + özel teklif paneli
│   │       ├── SpecialOffer/
│   │       │   └── SpecialOffer.jsx
│   │       ├── Feature/
│   │       │   └── Feature.jsx      # Öne çıkan özellikler
│   │       ├── Categories/
│   │       │   └── Categories.jsx   # Öne çıkan kategoriler
│   │       ├── FeatureProducts/
│   │       │   └── FeatureProducts.jsx  # Öne çıkan ürünler
│   │       ├── OfferDiscount/
│   │       │   └── OfferDiscount.jsx    # Kampanya / indirim alanları
│   │       ├── Vendor/
│   │       │   └── Vendor.jsx      # Markalar
│   │       └── index.js            # Tüm home bileşenlerini export eder
│   │
│   ├── pages/
│   │   └── HomePage/
│   │       └── HomePage.jsx         # Anasayfa: veri çekme + bölümleri bir araya getirir
│   │
│   ├── App.jsx
│   ├── App.css
│   ├── main.jsx
│   └── index.css
│
├── index.html
├── vite.config.js
├── jsconfig.json
├── package.json
└── README.md
```

## Gereksinimler

- Node 18+
- Backend: MultiShop.WebUI çalışır durumda (Ocelot + Catalog servisleri)

## Kurulum

```bash
npm install
```

## Geliştirme

```bash
npm run dev
```

Uygulama `http://localhost:5173` adresinde açılır. API istekleri Vite proxy ile `http://localhost:5178/api` adresine gider.

## Build

```bash
npm run build
```

Çıktı `dist/` klasöründedir.
