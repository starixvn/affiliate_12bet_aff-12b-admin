import React from 'react'
import { Claim, claimResource } from '../../../modules/claim';
import { menuKey, relativePath } from '../libs/constants';

const BannerList = React.lazy(() => import('../banner-list/index'));
const TextList = React.lazy(() => import('../text-list/index'));
const LandingPageList = React.lazy(() => import('../landing-page/index'));
const NewsLetterList = React.lazy(() => import('../newsletter/index'));
const OfferPage = React.lazy(() => import('../offer-page/index'));
const OfferAff = React.lazy(() => import('../offer-aff/index'));

export const menus = [
    { key: menuKey.bannerList, label: 'Banner List', href: relativePath.bannerList, component: BannerList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.textList, label: 'Text List', href: relativePath.textList, component: TextList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.landingPageList, label: 'Landing Page List', href: relativePath.landingPageList, component: LandingPageList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.newsLetterList, label: 'NewsLetter List', href: relativePath.newsLetterList, component: NewsLetterList, renderClaim: new Claim(claimResource.MediaGalleryMenu)},
    { key: menuKey.offerPage, label: 'Offer Page List', href: relativePath.offerPage, component: OfferPage, renderClaim: new Claim(claimResource.MediaGalleryMenu)},
    { key: menuKey.offerAff, label: 'Offer - Affiliate', href: relativePath.offerAff, component: OfferAff, renderClaim: new Claim(claimResource.MediaGalleryMenu)},
]
