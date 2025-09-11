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

export const maxWidthHeight = 501;
export const maxLength = 50;
export const regSpecialCharacters = '[\\\\\/\:\*\?\"\<\>\|\#\%\&\+]';

export const fileUpload = {
    AllowedImageExtensions: [".jpg", ".jpeg", ".png", ".gif"],
    MaximumFiles: 10,
    MaxFileSize: 3
}

export const assetStatus = {
    all: -1,
    active: 1,
    inactive: 2
};

export const defaultFormManagementModuleId = 9;