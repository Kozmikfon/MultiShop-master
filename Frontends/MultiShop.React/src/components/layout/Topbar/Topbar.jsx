import './Topbar.css'

export function Topbar() {
  return (
    <div className="container-fluid topbar-wrapper">
      <div className="row align-items-center bg-light py-3 px-xl-5 d-none d-lg-flex">
        <div className="col-lg-3">
          <a href="/" className="text-decoration-none d-flex align-items-center firsatkap-logo-link">
            <span className="firsatkap-logo-text">
              <span className="firsatkap-logo-firsat">Fırsat</span>
              <span className="firsatkap-logo-kap">Kap</span>
            </span>
          </a>
        </div>
        <div className="col-lg-5 col-6 text-left topbar-search-col">
          <form action="#" onSubmit={(e) => e.preventDefault()}>
            <div className="input-group topbar-input-group">
              <input type="text" className="form-control" placeholder="Aranacak Ürünü Giriniz" />
              <div className="input-group-append">
                <span className="input-group-text bg-transparent text-primary">
                  <i className="fa fa-search"></i>
                </span>
              </div>
            </div>
          </form>
        </div>
        <div className="col-lg-4 col-6 text-right d-flex align-items-center justify-content-end topbar-actions">
          <a href="/" className="topbar-icon-btn" title="Favoriler">
            <i className="fas fa-heart"></i>
            <span className="topbar-icon-badge">0</span>
          </a>
          <a href="/" className="topbar-icon-btn" title="Sepet">
            <i className="fas fa-shopping-cart"></i>
            <span className="topbar-icon-badge">0</span>
          </a>
          <a href="/Login/Index" className="topbar-login-btn">
            <i className="fa fa-sign-in-alt"></i>
            <span>Giriş Yap</span>
          </a>
        </div>
      </div>
    </div>
  )
}
