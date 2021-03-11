import React from 'react'
import { Claim, claimResource } from '../../../modules/claim';
import { menuKey, relativePath } from '../libs/constants';

const BannerList = React.lazy(() => import('../banner-list/index'));
const TextList = React.lazy(() => import('../text-list/index'));
const LandingPageList = React.lazy(() => import('../landing-page/index'));
const NewsLetterList = React.lazy(() => import('../newsletter/index'));

export const menus = [
    { key: menuKey.bannerList, label: 'Banner List', href: relativePath.bannerList, component: BannerList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.textList, label: 'Text List', href: relativePath.textList, component: TextList, renderClaim: new Claim(claimResource.MediaGalleryMenu) },
    { key: menuKey.landingPageList, label: 'Landing Page List', href: relativePath.landingPageList, component: LandingPageList },
    { key: menuKey.newsLetterList, label: 'NewsLetter List', href: relativePath.newsLetterList, component: NewsLetterList, renderClaim: new Claim(claimResource.MediaGalleryMenu)}
]
