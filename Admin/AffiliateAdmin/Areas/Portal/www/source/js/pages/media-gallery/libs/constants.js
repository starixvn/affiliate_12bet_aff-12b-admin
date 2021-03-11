import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    bannerList: 'bannerList',
    textList: 'textList',
    landingPageList: 'landingPageList',
    newsLetterList: 'newsLetterList'
}

export const relativePath = {
    bannerList: getVirtualUrl() + 'Portal/MediaGallery',
    textList: getVirtualUrl() + 'Portal/TextList',
    landingPageList: getVirtualUrl() + 'Portal/LandingPageList',
    newsLetterList: getVirtualUrl() + 'Portal/NewsLetter'
}