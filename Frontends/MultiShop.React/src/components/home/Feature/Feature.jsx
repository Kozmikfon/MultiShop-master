export function Feature({ items = [] }) {
  if (!items.length) return null

  return (
    <div className="container-fluid pt-5">
      <div className="row px-xl-5 pb-3">
        {items.map((item) => (
          <div key={item.featureId} className="col-lg-3 col-md-6 col-sm-12 pb-1">
            <div className="d-flex align-items-center bg-light mb-4" style={{ padding: 30 }}>
              <h1 className={item.icon} style={{ marginRight: 12 }} />
              <h5 className="font-weight-semi-bold m-0">{item.title}</h5>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
