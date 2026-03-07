import {
  Carousel,
  Feature,
  Categories,
  FeatureProducts,
  OfferDiscount,
  Vendor,
} from '../../components/home'

export function HomePage({ data }) {
  if (!data) return null

  return (
    <>
      <Carousel items={data.featureSliders} specialOffers={data.specialOffers} />
      <Feature items={data.features} />
      <FeatureProducts items={data.products} />
      <Categories items={data.categories} />
      <OfferDiscount items={data.offerDiscounts} />
      <Vendor items={data.brands} />
    </>
  )
}
