import { Casino, Sportsbook } from './constants';

export const validateFileExtension = (fileName) => {
    const result = {
        isOK: false,
        message: "Sorry! We just support these file types : '.jpg', '.gif' , '.png','.swf','.jpeg'",
        imageType: ''
    }
    if (fileName != "") {
        const fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length).toLowerCase();

        const isOK = (fileExtension === "jpg" || fileExtension === "jpeg" || fileExtension === "png" || fileExtension === "gif" || fileExtension === "swf");

        if (isOK) {
            result.isOK = isOK
            result.imageType = fileExtension === "swf" ? "Flash" : "Image";
            result.message = '';
        }
    }

    return result;

}

export const getSubCategory = (category, subCategoryCasinoList, subCategorySportsbookList) => {
    var result = {
        category: category,
        subCategory: ''
    }

    subCategoryCasinoList.map(item => {
        if (item.Name === category) {
            result.category = Casino;
            result.subCategory = category
        }
    })

    subCategorySportsbookList.map(item => {
        if (item.Name === category) {
            result.category = Sportsbook;
            result.subCategory = category
        }
    })

    return result;
}