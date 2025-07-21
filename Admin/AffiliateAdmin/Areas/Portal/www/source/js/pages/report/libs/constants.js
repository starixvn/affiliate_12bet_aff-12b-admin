import { resolveClientUrl } from "../../../modules/utils"

export const menuKey = {
    affiliateReport: 'affiliateReport',
    playerGgrReport: 'playerGgrReport',
    transferReport: 'transferReport',
    playerCPA: 'playerCPA',
}

export const relativePath = {
    affiliateReport: resolveClientUrl('Portal/Report'),
    playerGgrReport: resolveClientUrl('Portal/PlayerGgrReport'),
    transferReport: resolveClientUrl('Portal/TransferReport'),
    playerCPA: resolveClientUrl('Portal/PlayerCpaReport'),
}

export const getMaxHeight = () => {
    const height = window.innerHeight - 350;
    return height > 300 ? height : 300;
}