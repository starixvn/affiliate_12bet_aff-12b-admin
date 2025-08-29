import { resolveClientUrl } from "../../../modules/utils";
import { globalResource } from "../../../modules/globalResource";
import { defaultInsertId } from "../../../modules/constants";

export { globalResource, defaultInsertId };

export const menuKey = {
    pageManagement: 'pageManagement',
    pageBuilder: 'pageBuilder',
    assetUploader: 'assetUploader',
    formManagement: 'formManagement',
}

export const relativePath = {
    pageManagement: resolveClientUrl('Portal/LandingPage'),
    pageBuilder: resolveClientUrl('Portal/PageBuilder'),
    assetUploader: resolveClientUrl('Portal/AssetUploader'),
    formManagement: resolveClientUrl('Portal/FormManagement'),
}