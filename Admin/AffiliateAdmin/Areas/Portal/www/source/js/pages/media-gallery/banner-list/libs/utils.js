import { maxLength, validExtensions } from './constants';
import { validateFileExtension } from '../../../../modules/utils';

export const validateFileExtensionCustom = (fileName) => validateFileExtension(fileName, validExtensions);

export const validateInputFile = (inputFileName) => {
    const result = validateFileExtensionCustom(inputFileName);

    if (!!inputFileName && inputFileName.length > maxLength) {
        result.isOK = false;
        result.message = 'The maximum length of file name is 50. Please choose another name.';
    }

    return result;
}

export const resizeWidthHeight = (realWidth, realHeight, size = 500) => {
    let width = Number(realWidth);
    let height = Number(realHeight);

    if(width < size && height < size) {
        return { width, height }
    }

    if (width > height) {
        height = (size * height) / width;
        width = size;
    } else {
        width = (size * width) / height;
        height = size;
    }

    return { width, height }
}