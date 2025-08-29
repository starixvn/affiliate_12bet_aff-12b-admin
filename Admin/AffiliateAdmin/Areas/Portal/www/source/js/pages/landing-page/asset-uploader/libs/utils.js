import { maxLength } from './constants';

export const validateFileExtension = (fileName, allowedExtensions = [".jpg", ".gif", ".png", ".jpeg", ".webp"]) => {
    const result = {
        isOK: false,
        message: `Sorry! We just support these file types: ${allowedExtensions.join(', ')}`,
    }
    
    if (!!fileName && allowedExtensions && allowedExtensions.length > 0) {
        const fileExtension = '.' + fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length).toLowerCase();
        const isOK = allowedExtensions.some(ext => ext.toLowerCase() === fileExtension);

        if (isOK) {
            result.isOK = true;
            result.message = '';
        }
    }

    return result;
}

export const validateInputFile = (inputFileName, allowedExtensions = [".jpg", ".gif", ".png", ".jpeg", ".webp"]) => {
    const result = validateFileExtension(inputFileName, allowedExtensions);

    if (!!inputFileName && inputFileName.length > maxLength) {
        result.isOK = false;
        result.message = 'The maximum length of file name is 50. Please choose another name.';
    }

    return result;
}

export const validateFileSize = (file, maxSizeMB = 3) => {
    const result = {
        isOK: true,
        message: ''
    };

    if (file && file.size) {
        const fileSizeMB = file.size / (1024 * 1024); // Convert bytes to MB
        
        if (fileSizeMB > maxSizeMB) {
            result.isOK = false;
            result.message = `File "${file.name}" is ${fileSizeMB.toFixed(2)}MB. Maximum file size allowed is ${maxSizeMB}MB.`;
        }
    }

    return result;
}