import moment from 'moment';
import { get, post } from '../../../../modules/ajaxRequest';
import { formatDecimal } from '../../../../modules/numberFormat';
import { DATE_FORMAT, dateFields, decimalFields, TotalText } from './constants';

export const getPlayerCpaData = async (params, isExportExcel = false) => {
    const url = 'Portal/PlayerCpaReport/GetData';
    
    if(isExportExcel) {
        params = {...params, isExportExcel: true};
    }

    const response = await post(url, params);

    return response && response.status === 200 ? transformData(response.data) : null;
}

/**
 * Transforms raw data by formatting dates and decimal values
 * @param {Object} data - The data object containing mainData array
 * @returns {Object} Transformed data object
 */
const transformData = (data) => {

    const transformDate = (value) => 
        value == null || value == undefined ? "-" : moment(value).format(DATE_FORMAT);

    const transformDecimal = (value) =>
        value == null || value == undefined ? "-" : formatDecimal(value);

    const transformField = (value, key) => {
        if (dateFields.includes(key)) return transformDate(value);
        if (decimalFields.includes(key)) return transformDecimal(value);
        return value ?? "-";
    };

    data.mainData = data.mainData.map(item => {
        return Object.fromEntries(
            Object.entries(item).map(([key, value]) => [key, transformField(value, key)])
        );
    });

    data.total = { ...data.total, affUserName: TotalText };

    return data;
};

export const getTurnoverDetail = async (memberCpaId) => {
    const url = `Portal/PlayerCpaReport/GetTurnoverDetail?memberCpaId=${memberCpaId}`;
    const response = await get(url);

    return response && response.status === 200 ? response.data : null;
}