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