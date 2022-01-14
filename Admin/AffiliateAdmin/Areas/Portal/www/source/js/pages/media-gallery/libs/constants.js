import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    bannerList: 'bannerList',
    textList: 'textList',
    landingPageList: 'landingPageList',
    newsLetterList: 'newsLetterList',
    offerPage: 'offerPage',
    offerAff: 'offerAff',
}

export const relativePath = {
    bannerList: getVirtualUrl() + 'Portal/MediaGallery',
    textList: getVirtualUrl() + 'Portal/TextList',
    landingPageList: getVirtualUrl() + 'Portal/LandingPageList',
    newsLetterList: getVirtualUrl() + 'Portal/NewsLetter',
    offerPage: getVirtualUrl() + 'Portal/OfferPage',
    offerAff: getVirtualUrl() + 'Portal/OfferAff',
}