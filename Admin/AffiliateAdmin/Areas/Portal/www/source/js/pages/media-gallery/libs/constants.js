import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    bannerList: 'bannerList',
    textList: 'textList',
    landingPageList: 'landingPageList',
    newsLetterList: 'newsLetterList',
    exclusivePage: 'exclusivePage',
    exclusiveAff: 'exclusiveAff',
}

export const relativePath = {
    bannerList: getVirtualUrl() + 'Portal/MediaGallery',
    textList: getVirtualUrl() + 'Portal/TextList',
    landingPageList: getVirtualUrl() + 'Portal/LandingPageList',
    newsLetterList: getVirtualUrl() + 'Portal/NewsLetter',
    exclusivePage: getVirtualUrl() + 'Portal/ExclusivePage',
    exclusiveAff: getVirtualUrl() + 'Portal/ExclusiveAff',
}