import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    affMember: 'affMember',
    activeMember: 'activeMember'
}

export const relativePath = {
    affMember: getVirtualUrl() + 'Portal/Member',
    activeMember: getVirtualUrl() + 'Portal/ActiveMember'
}