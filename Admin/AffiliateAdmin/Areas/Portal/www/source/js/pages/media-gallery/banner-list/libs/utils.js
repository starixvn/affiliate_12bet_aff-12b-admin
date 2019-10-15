import { Casino, Sportsbook, maxLenght } from './constants';

export const validateFileExtension = (fileName) => {
    const result = {
        isOK: false,
        message: "Sorry! We just support these file types : '.jpg', '.gif' , '.png','.jpeg'",
    }
    if (fileName != "" && fileName.length <= maxLenght) {
        const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length).toLowerCase();

        const isOK = (fileExtension === "jpg" || fileExtension === "jpeg" || fileExtension === "png" || fileExtension === "gif");

        if (isOK) {
            result.isOK = isOK
            result.message = '';
        }
    } else {
        result.message = 'The file name must not exceed 60 characters';
    }

    return result;

}

export const getSubCategory = (category, subCategoryCasinoList, subCategorySportsbookList) => {
    var result = {
        category: category,
        subCategory: ''
    }

    subCategoryCasinoList.map(item => {
        if (item.CategoryName === category) {
            result.category = Casino;
            result.subCategory = category
        }
    })

    subCategorySportsbookList.map(item => {
        if (item.CategoryName === category) {
            result.category = Sportsbook;
            result.subCategory = category
        }
    })

    return result;
}

export const resizeWidthHeight = (realWidth, realHeight, size = 500) => {
    let width = Number(realWidth);
    let height = Number(realHeight);

    if (width > height) {
        height = size * height / width;
        width = size;
    }
    else {
        width = size * width / height;
        height = size;
    }

    return { width, height }
}