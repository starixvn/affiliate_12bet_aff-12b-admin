import { resolveClientUrl } from "@/modules/utils";

export const menuKey = {
    allCampaigns: 'allCampaigns',
    componentManagement: 'componentManagement',
};

export const relativePath = {
    allCampaigns: resolveClientUrl('Portal/CampaignManagement'),
    componentManagement: resolveClientUrl('Portal/ComponentManagement'),
};