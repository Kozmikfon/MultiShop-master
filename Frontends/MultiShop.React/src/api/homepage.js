const API_BASE = import.meta.env.VITE_API_URL ?? '/api'

/**
 * Anasayfa verilerini API'den çeker.
 * @returns {Promise<Object>} { featureSliders, features, categories, products, offerDiscounts, brands, specialOffers }
 */
export async function fetchHomepageData() {
  const res = await fetch(`${API_BASE}/HomepageApi`)
  if (!res.ok) throw new Error('Anasayfa verileri yüklenemedi')
  return res.json()
}
