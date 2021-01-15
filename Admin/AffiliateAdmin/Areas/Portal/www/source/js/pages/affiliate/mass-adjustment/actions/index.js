import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { globalResource } from '../../../../modules/globalResource';
import { SET_FILE, SET_IS_CLICK_UPLOAD } from './types';

export { toggleLoader };

export const setFile = (data) => {
    return {
        type: SET_FILE,
        data
    }
}

export const setIsClickUpload = (data) => {
    return {
        type: SET_IS_CLICK_UPLOAD,
        data
    }
}

export const handleUploadFile = (file) => async () => {
    const url = 'Portal/MassAdjustment/UploadFile';
    const formData = new FormData();
    formData.append('file', file);

    const response = await post(url, formData);

    return response && response.status === 200 ? response.data : {
        Result: {
            IsSuccessful: false,
            Message: globalResource.AnError,
        },
        ErroredAdjustmentInfoList: null
    };

    //return new Promise((resolve, reject) => {
    //    const result = {
    //        isOk: false,
    //        message: 'Upload failed!',
    //        data: "Json"
    //    };
    //    resolve(result);
    //});
}