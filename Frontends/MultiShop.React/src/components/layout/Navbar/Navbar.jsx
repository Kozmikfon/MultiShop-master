import { useState } from 'react'
import './Navbar.css'

export function Navbar({ categories = [] }) {
  const [collapseOpen, setCollapseOpen] = useState(false)
  const [categoriesOpen, setCategoriesOpen] = useState(false)

  return (
    <div className="container-fluid bg-dark mb-30 navbar-wrapper">
      <div className="row px-xl-5">
        <div className="col-lg-3 d-none d-lg-block navbar-categories-col">
          <button
            type="button"
            className="btn d-flex align-items-center justify-content-between bg-primary w-100 navbar-categories-btn"
            onClick={() => setCategoriesOpen(!categoriesOpen)}
            style={{ height: '65px', padding: '0 30px' }}
          >
            <h6 className="text-dark m-0">
              <i className="fa fa-bars mr-2"></i>Kategoriler
            </h6>
            <i className={`fa fa-angle-down text-dark ${categoriesOpen ? 'rotate-180' : ''}`}></i>
          </button>
          {categoriesOpen && (
            <nav
              className="position-absolute navbar navbar-vertical navbar-light align-items-start p-0 bg-light navbar-vertical-dropdown"
              style={{ width: 'calc(100% - 30px)', zIndex: 999 }}
            >
              <div className="navbar-nav w-100">
                {categories.map((item) => (
                  <a key={item.categoryID} href={`/ProductList/Index/${item.categoryID}`} className="nav-item nav-link">
                    {item.categoryName}
                  </a>
                ))}
              </div>
            </nav>
          )}
        </div>
        <div className="col-lg-9">
          <nav className="navbar navbar-expand-lg bg-dark navbar-dark py-3 py-lg-0 px-0">
            <a href="/" className="text-decoration-none d-flex align-items-center firsatkap-logo-link firsatkap-logo-inverse navbar-brand-link">
              <span className="firsatkap-logo-text">
                <span className="firsatkap-logo-firsat">Fırsat</span>
                <span className="firsatkap-logo-kap">Kap</span>
              </span>
            </a>
            <button
              type="button"
              className="navbar-toggler"
              onClick={() => setCollapseOpen(!collapseOpen)}
              aria-label="Menü"
            >
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className={`collapse navbar-collapse justify-content-between ${collapseOpen ? 'show' : ''}`}>
              <div className="navbar-nav mr-auto py-0">
                <a href="/" className="nav-item nav-link">Ana Sayfa</a>
                <a href="#products" className="nav-item nav-link">Ürünler</a>
                <a href="#products" className="nav-item nav-link">Günün Fırsatı</a>
                <a href="/Contact/Index" className="nav-item nav-link">İletişim</a>
              </div>
            </div>
          </nav>
        </div>
      </div>
    </div>
  )
}
