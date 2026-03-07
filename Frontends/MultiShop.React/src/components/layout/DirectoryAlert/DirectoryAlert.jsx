import './DirectoryAlert.css'

export function DirectoryAlert({ directory1 = 'MultiShop', directory2 = 'Ana Sayfa', directory3 = 'Ürün Listesi' }) {
  return (
    <div className="container-fluid directory-alert-wrapper">
      <div className="row px-xl-5">
        <div className="col-12">
          <nav className="breadcrumb bg-light mb-30">
            <a className="breadcrumb-item text-dark" href="/">{directory1}</a>
            <a className="breadcrumb-item text-dark" href="/">{directory2}</a>
            <span className="breadcrumb-item active">{directory3}</span>
          </nav>
        </div>
      </div>
    </div>
  )
}
