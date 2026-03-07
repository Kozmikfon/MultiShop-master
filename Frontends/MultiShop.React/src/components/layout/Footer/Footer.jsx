import './Footer.css'

export function Footer({ about = [] }) {
  const firstAbout = about && about.length > 0 ? about[0] : null

  return (
    <div className="container-fluid bg-dark text-secondary mt-5 pt-5 footer-wrapper">
      <div className="row px-xl-5 pt-5">
        {firstAbout && (
          <div className="col-lg-4 col-md-12 mb-5 pr-3 pr-xl-5">
            <h5 className="text-secondary text-uppercase mb-4">Hakkımızda</h5>
            <p className="mb-4">{firstAbout.description}</p>
            <p className="mb-2">
              <i className="fa fa-map-marker-alt text-primary mr-3"></i>
              {firstAbout.address}
            </p>
            <p className="mb-2">
              <i className="fa fa-envelope text-primary mr-3"></i>
              {firstAbout.email}
            </p>
            <p className="mb-0">
              <i className="fa fa-phone-alt text-primary mr-3"></i>
              {firstAbout.phone}
            </p>
          </div>
        )}
        <div className={firstAbout ? 'col-lg-8 col-md-12' : 'col-12'}>
          <div className="row">
            <div className="col-md-4 mb-5">
              <h5 className="text-secondary text-uppercase mb-4">Hızlı Alışveriş</h5>
              <div className="d-flex flex-column justify-content-start">
                <a className="text-secondary mb-2" href="/"><i className="fa fa-angle-right mr-2"></i>Ana Sayfa</a>
                <a className="text-secondary mb-2" href="#products"><i className="fa fa-angle-right mr-2"></i>Ürünler</a>
                <a className="text-secondary mb-2" href="#products"><i className="fa fa-angle-right mr-2"></i>Günün Fırsatı</a>
                <a className="text-secondary mb-2" href="/cart"><i className="fa fa-angle-right mr-2"></i>Sepetim</a>
                <a className="text-secondary" href="/Contact/Index"><i className="fa fa-angle-right mr-2"></i>Bize Yazın</a>
              </div>
            </div>
            <div className="col-md-4 mb-5">
              <h5 className="text-secondary text-uppercase mb-4">Hesabım</h5>
              <div className="d-flex flex-column justify-content-start">
                <a className="text-secondary mb-2" href="/"><i className="fa fa-angle-right mr-2"></i>Profilim</a>
                <a className="text-secondary mb-2" href="/"><i className="fa fa-angle-right mr-2"></i>Alışverişlerim</a>
                <a className="text-secondary mb-2" href="/"><i className="fa fa-angle-right mr-2"></i>Geçmiş Siparişlerim</a>
                <a className="text-secondary" href="/Contact/Index"><i className="fa fa-angle-right mr-2"></i>İletişim</a>
              </div>
            </div>
            <div className="col-md-4 mb-5">
              <h5 className="text-secondary text-uppercase mb-4">Mail Bülteni</h5>
              <p>En son indirimler ve kampanyalar için hemen abone olun!</p>
              <form action="#" onSubmit={(e) => e.preventDefault()}>
                <div className="input-group">
                  <input type="email" className="form-control" placeholder="Email Adresiniz" />
                  <div className="input-group-append">
                    <button type="submit" className="btn btn-primary">Abone Ol</button>
                  </div>
                </div>
              </form>
              <h6 className="text-secondary text-uppercase mt-4 mb-3">Bizi Takip Edin</h6>
              <div className="d-flex">
                <a className="btn btn-primary btn-square" href="https://www.instagram.com/firsatkapp?igsh=MWQwMjRxemt3cXExOA==" target="_blank" rel="noopener noreferrer" aria-label="Instagram"><i className="fab fa-instagram"></i></a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="row border-top mx-xl-5 py-4 footer-bottom" style={{ borderColor: 'rgba(256, 256, 256, .1)' }}>
        <div className="col-md-6 px-xl-0">
          <p className="mb-md-0 text-center text-md-left text-secondary">
            &copy; <a className="text-primary" href="/">FırsatKap</a>. Tüm hakları saklıdır.
          </p>
        </div>
        <div className="col-md-6 px-xl-0 text-center text-md-right">
          <img className="img-fluid" src="/images/logos/odeme_logo.png" alt="Ödeme yöntemleri" style={{ maxHeight: '28px' }} />
        </div>
      </div>
    </div>
  )
}
