import './Header.css'

export function Header() {
  return (
    <header className="header">
      <div className="container">
        <div className="header__inner">
          <a href="/" className="header__logo">
            MultiShop
          </a>
          <nav className="header__nav">
            <a href="#products" className="header__nav-link">Ürünler</a>
            <a href="#categories" className="header__nav-link">Kategoriler</a>
          </nav>
        </div>
      </div>
    </header>
  )
}
