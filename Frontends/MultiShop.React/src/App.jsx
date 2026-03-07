import { useEffect, useState } from 'react'
import { fetchHomepageData } from './api/homepage'
import { Banner, Topbar, Navbar, Stories, Footer } from './components/layout'
import { HomePage } from './pages/HomePage/HomePage'
import './App.css'

function App() {
  const [data, setData] = useState(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    fetchHomepageData()
      .then(setData)
      .catch((e) => setError(e instanceof Error ? e.message : 'Bir hata oluştu'))
      .finally(() => setLoading(false))
  }, [])

  return (
    <div className="App">
      <Banner />
      <Topbar />
      <Navbar categories={data?.categories ?? []} />
      <Stories />
      <main>
        {loading && (
          <div className="container py-5 text-center">
            <div className="spinner-border text-primary" role="status" aria-label="Yükleniyor">
            </div>
            <p className="mt-2">Anasayfa yükleniyor...</p>
          </div>
        )}
        {error && (
          <div className="container py-5 text-center">
            <div className="alert alert-danger" role="alert">{error}</div>
            <p className="text-muted">API bağlantısını kontrol edin.</p>
          </div>
        )}
        {!loading && !error && data && <HomePage data={data} />}
      </main>
      <Footer about={data?.about ?? []} />
    </div>
  )
}

export default App
