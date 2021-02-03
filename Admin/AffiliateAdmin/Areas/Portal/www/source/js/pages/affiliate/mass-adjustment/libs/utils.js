import { incorrectFormatMessage } from './constants';

export const validateFileExtension = (fileName) => {
    const result = {
        isOK: false,
        message: incorrectFormatMessage
    }
    if (!!fileName) {
        const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length).toLowerCase();

        const isOK = (fileExtension === "xlsx" || fileExtension === "xls");

        if (isOK) {
            result.isOK = isOK
            result.message = '';
        }
    }

    return result;
}