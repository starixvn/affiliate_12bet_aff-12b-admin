import React from 'react'
import { Claim, claimResource } from '../../../modules/claim';
import { menuKey, relativePath } from '../libs/constants';

const BannerList = React.lazy(() => import('../banner-list/index'));
const TextList = React.lazy(() => import('../text-list/index'));
const LandingPageList = React.lazy(() => import('../landing-page/index'));
const NewsLetterList = React.lazy(() => import('../newsletter/index'));
const ExclusivePage = React.lazy(() => import('../exclusive-page/index'));
const ExclusiveAff = React.lazy(() => import('../exclusive-aff/index'));

export const menus = [
    { key: menuKey.bannerList, label: 'Banner List', href: relativePath.bannerList, component: BannerList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.textList, label: 'Text List', href: relativePath.textList, component: TextList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.landingPageList, label: 'Landing Page List', href: relativePath.landingPageList, component: LandingPageList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.newsLetterList, label: 'NewsLetter List', href: relativePath.newsLetterList, component: NewsLetterList, renderClaim: new Claim(claimResource.MediaGalleryMenu)},
    { key: menuKey.exclusivePage, label: 'Exclusive Page List', href: relativePath.exclusivePage, component: ExclusivePage, renderClaim: new Claim(claimResource.MediaGalleryMenu)},
    { key: menuKey.exclusiveAff, label: 'Exclusive - Affiliate', href: relativePath.exclusiveAff, component: ExclusiveAff, renderClaim: new Claim(claimResource.MediaGalleryMenu)},
]
